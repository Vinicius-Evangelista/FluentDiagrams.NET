using FluentDiagrams.NET.AWS;
using FluentDiagrams.NET.LayoutEngine;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using SkiaSharp;

namespace FluentDiagrams.NET.Core;

public class Diagram : IDiagram
{
  private MsaglLayoutEngine Engine { get; set; } = new();
  private Cluster Cluster { get; set; } = new();

  public IComposable AddElement(IElement element,
                                string? parentId = null!)
  {
    if (element is null)
      throw new ArgumentNullException(paramName: nameof(element));

    if (parentId is not null)
    {
      // TODO: Add personalized exception for not found parent

      IElement parent = GetElementById(id: parentId) ??
                        throw new InvalidOperationException();

      Engine.AddEdge(source: element, target: parent);
    }

    if (parentId is null)
      Engine.AddNode(element: element);

    return this;
  }

  private IElement? GetElementById(string id)
  {
    if (string.IsNullOrEmpty(value: id))
      throw new ArgumentNullException(paramName: nameof(id));

    return Engine.GetElementById(id: id);
  }

  public IDiagram AddContainer(IContainer container)
  {
    if (container is null)
      throw new ArgumentNullException(paramName: nameof(container));

    AddElement(element: container);

    foreach (IElement element in container.Elements)
      AddElement(element: element, parentId: container.Id);

    return this;
  }

  public void Render()
  {
    using var bitmap = new SKBitmap(width: 2000, height: 1000);
    using var canvas = new SKCanvas(bitmap: bitmap);
    canvas.Clear(color: SKColors.White);

    foreach (Node graphNode in
             Engine.Graph.Nodes.Take(count: Engine.Graph.Nodes.Count /
                                            2))
    {
      Cluster.AddChild(child: new
                         Node(curve: CurveFactory.CreateRectangle(width: 100, height: 60, center: new Point(xCoordinate: 0, yCoordinate: 0)),
                              userData: new Ec2(id: "instance-x")));
    }


    Cluster.UserData = 1;
    Engine.Graph.RootCluster.AddChild(child: Cluster);

    Engine.Graph.RootCluster.UserData = 1;

    Engine.Run();

    Rectangle bbox = Engine.Graph.BoundingBox;

    foreach (Node? node in
             Engine.Graph.RootCluster.Clusters
                   .Select(selector: x => x.Nodes).SelectMany(selector: x => x))
    {
      double x = node.Center.X;
      double y = node.Center.Y;

      var paint = new SkiaSharp.SKPaint
      {
        Color = SkiaSharp.SKColors.Blue,
        IsAntialias = true
      };

      canvas.DrawCircle(cx: (float)x, cy: (float)y, radius: 10,
                        paint: paint);

      var textPaint = new SkiaSharp.SKPaint
      {
        Color = SkiaSharp.SKColors.Black,
        TextSize = 16
      };

      canvas.DrawText(
                      text: ((IElement)node.UserData).Id ?? "",
                      x: (float)x + 12,
                      y: (float)y + 5,
                      paint: textPaint
                     );
    }

    foreach (Node node in Engine.Graph.Nodes)
    {
      double x = node.Center.X;
      double y = node.Center.Y;

      var paint = new SkiaSharp.SKPaint
      {
        Color = SkiaSharp.SKColors.Blue,
        IsAntialias = true
      };

      canvas.DrawCircle(cx: (float)x, cy: (float)y, radius: 10,
                        paint: paint);

      var textPaint = new SkiaSharp.SKPaint
      {
        Color = SkiaSharp.SKColors.Black,
        TextSize = 16
      };

      canvas.DrawText(
                      text: ((IElement)node.UserData).Id ?? "",
                      x: (float)x + 12,
                      y: (float)y + 5,
                      paint: textPaint
                     );
    }

    foreach (Edge? edge in Engine.Graph.Edges)
    {
      var edgePaint = new SKPaint
      {
        Color = SKColors.Black,
        StrokeWidth = 2,
        IsAntialias = true,
        Style = SKPaintStyle.Stroke
      };

      ICurve? edgeCurve = edge.EdgeGeometry.Curve;

      if (edgeCurve != null)
      {
        var path = new SKPath();

        var steps = 1000;
        double tStep = 1.0 / steps;
        Point start = edgeCurve.Start;
        path.MoveTo(x: (float)start.X, y: (float)start.Y);

        for (var i = 1; i <= steps; i++)
        {
          double t = i * tStep;
          Point point =
            edgeCurve
              [t: edgeCurve.ParStart + t * (edgeCurve.ParEnd - edgeCurve.ParStart)];
          path.LineTo(x: (float)point.X, y: (float)point.Y);
        }

        canvas.DrawPath(path: path, paint: edgePaint);
      }
    }

    using SKImage? image =
      SkiaSharp.SKImage.FromBitmap(bitmap: bitmap);
    using SKData? data =
      image.Encode(format: SkiaSharp.SKEncodedImageFormat.Png,
                   quality: 100);
    using FileStream stream =
      File.OpenWrite(path: "C:\\Users\\vinie\\diagramaa.png");
    data.SaveTo(target: stream);
  }
}
