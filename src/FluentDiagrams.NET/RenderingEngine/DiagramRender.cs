using FluentDiagrams.NET.Core;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using SkiaSharp;

namespace FluentDiagrams.NET.RenderingEngine;

public static class DiagramRender
{
  public static void Render(GeometryGraph graph,
                            DiagramSettings settings)
  {
    using SKBitmap bitmap = new(width: (int)settings.Width,
                                height: (int)settings.Height);
    using SKCanvas canvas = new(bitmap: bitmap);
    canvas.Clear(color: SKColors.White);

    foreach (Edge? edge in graph.Edges)
      DrawEdge(canvas: canvas, edge: edge);

    foreach (Node? node in
             graph.RootCluster.Clusters.SelectMany(selector: x =>
               x.Nodes))
    {
      DrawNodeWithLabel(canvas: canvas, node: node,
                        labelBackgroundColor: SKColors.White,
                        labelYOffset: 13);
    }

    foreach (Cluster? cluster in graph.RootCluster.Clusters)
      DrawCluster(canvas: canvas, cluster: cluster);

    if (graph.RootCluster.BoundingBox != null)
    {
      DrawRootClusterBoundingBox(canvas: canvas, graph: graph,
                                 settings: settings);
    }

    if (graph.BoundingBox != null)
      DrawGraphBoundingBox(canvas: canvas, graph: graph);

    foreach (Node? node in graph.Nodes)
    {
      DrawNodeWithLabel(canvas: canvas, node: node,
                        labelBackgroundColor: SKColors.White,
                        labelYOffset: 13);
    }

    using SKImage image = SKImage.FromBitmap(bitmap: bitmap);
    using SKData data =
      image.Encode(format: SKEncodedImageFormat.Jpeg, quality: 100);
    using FileStream stream =
      File.OpenWrite(path: string.IsNullOrWhiteSpace(value: settings
                             .OutputPath)
                             ? settings.FileName
                             : settings.OutputPath);

    data.SaveTo(target: stream);
  }

  private static void DrawEdge(SKCanvas canvas, Edge? edge)
  {
    using SKPaint edgePaint = new();
    edgePaint.Color = SKColors.Black;
    edgePaint.StrokeWidth = 2;
    edgePaint.IsAntialias = true;
    edgePaint.Style = SKPaintStyle.Stroke;

    ICurve? edgeCurve = edge?.EdgeGeometry?.Curve;
    if (edgeCurve == null)
      return;

    SKPath path = new();

    const int steps = 999;
    const double tStep = 1.0 / steps;

    Point start = edgeCurve.Start;

    path.MoveTo(x: (float)start.X, y: (float)start.Y);

    for (var i = 0; i <= steps; i++)
    {
      double t = i * tStep;
      Point point =
        edgeCurve
          [t: edgeCurve.ParStart + t * (edgeCurve.ParEnd - edgeCurve.ParStart)];
      path.LineTo(x: (float)point.X, y: (float)point.Y);
    }

    canvas.DrawPath(path: path, paint: edgePaint);
  }

  private static void DrawNodeWithLabel(SKCanvas canvas,
                                        Node? node,
                                        SKColor labelBackgroundColor,
                                        float labelYOffset)
  {
    if (node?.UserData is not IElement element)
      return;
    double x = node.Center.X;
    double y = node.Center.Y;

    using FileStream imgStream =
      File.OpenRead(path: element.ImagePath);

    using SKBitmap? skBitmap = SKBitmap.Decode(stream: imgStream);

    float imgX = (float)x - skBitmap.Width / 2f;
    float imgY = (float)y - skBitmap.Height / 2f;

    canvas.DrawBitmap(bitmap: skBitmap, x: imgX, y: imgY);

    using SKPaint textPaint = new();

    textPaint.Color = SKColors.Black;
    textPaint.TextSize = 16;
    string label = element.Id;

    SKRect textBounds = new();

    textPaint.MeasureText(text: label, bounds: ref textBounds);

    const float padding = 6f;

    float labelX = imgX + (skBitmap.Width - textBounds.Width) / 2f -
                   textBounds.Left;

    float labelY = imgY + skBitmap.Height + padding + labelYOffset;

    SKRect backgroundRect = new(
                                left: labelX - padding,
                                top: labelY + textBounds.Top -
                                     padding,
                                right: labelX + textBounds.Width +
                                       padding,
                                bottom: labelY + textBounds.Bottom +
                                        padding
                               );

    using SKPaint backgroundPaint = new();

    backgroundPaint.Color = labelBackgroundColor;
    backgroundPaint.Style = SKPaintStyle.Fill;

    canvas.DrawRect(rect: backgroundRect, paint: backgroundPaint);

    canvas.DrawText(text: label, x: labelX, y: labelY,
                    paint: textPaint);
  }

  private static void DrawCluster(SKCanvas canvas, Cluster? cluster)
  {
    if (cluster == null) return;
    Rectangle box = cluster.BoundingBox;

    using SKPaint clusterPaint = new();

    clusterPaint.Color = SKColors.Gray;
    clusterPaint.StrokeWidth = 2;
    clusterPaint.IsAntialias = true;
    clusterPaint.Style = SKPaintStyle.Stroke;

    clusterPaint.PathEffect =
      SKPathEffect.CreateDash(intervals: new float[] { 8, 6 },
                              phase: 0);

    var rect = new SKRect(
                          left: (float)box.Left,
                          top: (float)box.Bottom,
                          right: (float)box.Right,
                          bottom: (float)box.Top
                         );

    canvas.DrawRect(rect: rect, paint: clusterPaint);
  }

  private static void DrawRootClusterBoundingBox(
    SKCanvas canvas,
    GeometryGraph graph,
    DiagramSettings settings)
  {
    Rectangle currentBox = graph.RootCluster.BoundingBox;

    double boxWidth = currentBox.Right - currentBox.Left;
    double boxHeight = currentBox.Top - currentBox.Bottom;

    double centerX = settings.Width / 2;
    double centerY = settings.Height / 2;


    var newBoundingBox = new Rectangle(
                                       x0: centerX - boxWidth / 2,
                                       y0: centerY - boxHeight / 2,
                                       x1: centerX + boxWidth / 2,
                                       y1: centerY + boxHeight / 2
                                      );
    graph.RootCluster.BoundingBox = new Rectangle
    {
      Left = newBoundingBox.Left,
      Bottom = newBoundingBox.Bottom,
      Right = newBoundingBox.Right,
      Top = newBoundingBox.Top
    };

    using SKPaint rootClusterPaint = new();

    rootClusterPaint.Color = SKColors.Gray;
    rootClusterPaint.StrokeWidth = 2;
    rootClusterPaint.IsAntialias = true;
    rootClusterPaint.Style = SKPaintStyle.Stroke;
    rootClusterPaint.PathEffect =
      SKPathEffect.CreateDash(intervals: new float[] { 8, 6 },
                              phase: 0);

    var rootRect = new SKRect(
                              left: (float)newBoundingBox.Left,
                              top: (float)newBoundingBox.Bottom,
                              right: (float)newBoundingBox.Right,
                              bottom: (float)newBoundingBox.Top
                             );


    canvas.DrawRect(rect: rootRect, paint: rootClusterPaint);
  }

  private static void DrawGraphBoundingBox(
    SKCanvas canvas,
    GeometryGraph graph)
  {
    Rectangle rootBox = graph.BoundingBox;

    using SKPaint rootClusterPaint = new();

    rootClusterPaint.Color = SKColors.Red;
    rootClusterPaint.StrokeWidth = 2;
    rootClusterPaint.IsAntialias = true;
    rootClusterPaint.Style = SKPaintStyle.Stroke;
    rootClusterPaint.PathEffect =
      SKPathEffect.CreateDash(intervals: new float[] { 8, 6 },
                              phase: 0);

    var rootRect = new SKRect(
                              left: (float)rootBox.Left,
                              top: (float)rootBox.Bottom,
                              right: (float)rootBox.Right,
                              bottom: (float)rootBox.Top
                             );

    canvas.DrawRect(rect: rootRect, paint: rootClusterPaint);
  }
}
