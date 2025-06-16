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

    foreach (Node? node in graph.Nodes)
    {
      double x = node.Center.X;
      double y = node.Center.Y;

      if (node.UserData is not IElement element)
        continue;

      using FileStream imgStream = File.OpenRead(path: element.ImagePath);
      using SKBitmap? skBitmap = SKBitmap.Decode(stream: imgStream);

      float imgX = (float)x - skBitmap.Width / 2f;
      float imgY = (float)y - skBitmap.Height / 2f;
      canvas.DrawBitmap(bitmap: skBitmap, x: imgX, y: imgY);

      using SKPaint textPaint = new();
      textPaint.Color = SKColors.Black;
      canvas.DrawText(text: element.Id, x: (float)imgX + 12,
                      y: (float)imgY + 5,
                      font: new SKFont()
                      {
                        Size = 16
                      }, paint: textPaint);
    }

    foreach (Node? node in
             graph.RootCluster.Clusters.SelectMany(selector: x =>
                    x.Nodes))
    {
      double x = node.Center.X;
      double y = node.Center.Y;

      if (node.UserData is IElement element)
      {
        using FileStream imgStream = File.OpenRead(path: element.ImagePath);
        using SKBitmap? skBitmap = SKBitmap.Decode(stream: imgStream);

        float imgX = (float)x - skBitmap.Width / 2f;
        float imgY = (float)y - skBitmap.Height / 2f;
        canvas.DrawBitmap(bitmap: skBitmap, x: imgX, y: imgY);

        using SKPaint textPaint = new();
        textPaint.Color = SKColors.Black;
        canvas.DrawText(text: element.Id, x: (float)x + 12,
                        y: (float)y + 5,
                        font: new SKFont()
                        {
                          Size = 16
                        }, paint: textPaint);
      }
    }

    foreach (Cluster? cluster in graph.RootCluster.Clusters)
    {
      Rectangle box = cluster.BoundingBox;

      using SKPaint clusterPaint = new();
      clusterPaint.Color = SKColors.Gray;
      clusterPaint.StrokeWidth = 2;
      clusterPaint.IsAntialias = true;
      clusterPaint.Style = SKPaintStyle.Stroke;

      clusterPaint.PathEffect = SKPathEffect.CreateDash(intervals: new float[] { 8, 6 }, phase: 0);

      var rect = new SKRect(
                            left: (float)box.Left,
                            top: (float)box.Bottom,
                            right: (float)box.Right,
                            bottom: (float)box.Top
                           );

      canvas.DrawRect(rect: rect, paint: clusterPaint);
    }

    if (graph.RootCluster.BoundingBox != null)
    {
      Rectangle currentBox = graph.RootCluster.BoundingBox;
      double boxWidth = currentBox.Right - currentBox.Left;
      double boxHeight = currentBox.Top - currentBox.Bottom;

      double centerX = settings.Width / 2;
      double centerY = settings.Height / 2;

      var newBoundingBox =  new Rectangle(
                        x0: centerX - boxWidth / 2,      // left
                        y0: centerY - boxHeight / 2,    // bottom
                        x1: centerX + boxWidth / 2,     // right
                        y1: centerY + boxHeight / 2     // top
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

      rootClusterPaint.PathEffect = SKPathEffect.CreateDash(intervals: new float[] { 8, 6 }, phase: 0);

      var rootRect = new SKRect(
        left: (float)newBoundingBox.Left,
        top: (float)newBoundingBox.Bottom,
        right: (float)newBoundingBox.Right,
        bottom: (float)newBoundingBox.Top
      );

      canvas.DrawRect(rect: rootRect, paint: rootClusterPaint);
    }


    // Delimitador do root cluster (também com linha pontilhada)
    if (graph.BoundingBox != null)
    {
      Rectangle rootBox = graph.BoundingBox;

      using SKPaint rootClusterPaint = new();
      rootClusterPaint.Color = SKColors.Red;
      rootClusterPaint.StrokeWidth = 2;
      rootClusterPaint.IsAntialias = true;
      rootClusterPaint.Style = SKPaintStyle.Stroke;
      rootClusterPaint.PathEffect = SKPathEffect.CreateDash(intervals: new float[] { 8, 6 }, phase: 0);

      var rootRect = new SKRect(
                                left: (float)rootBox.Left,
                                top: (float)rootBox.Bottom,
                                right: (float)rootBox.Right,
                                bottom: (float)rootBox.Top
                               );

      canvas.DrawRect(rect: rootRect, paint: rootClusterPaint);
    }

    foreach (Edge? edge in graph.Edges)
    {
      using SKPaint edgePaint = new();
      edgePaint.Color = SKColors.Black;
      edgePaint.StrokeWidth = 2;
      edgePaint.IsAntialias = true;
      edgePaint.Style = SKPaintStyle.Stroke;

      ICurve? edgeCurve = edge.EdgeGeometry?.Curve;

      if (edgeCurve == null)
        continue;

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
}
