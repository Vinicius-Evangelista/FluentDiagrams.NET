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
  private DiagramSettings Settings { get; set; } = new();

  public IDiagram SetSettings(DiagramSettings settings)
  {
    if (settings is null)
      throw new ArgumentNullException(paramName: nameof(settings));

    Settings = settings;

    return this;
  }

  public IComposable AddElement(IElement element)
  {
    if (element is null)
      throw new ArgumentNullException(paramName: nameof(element));

    if (!string.IsNullOrEmpty(value: element.ConnectTo) )
    {
      // TODO: Add personalized exception for not found parent
      IElement parent = GetElementById(id: element.ConnectTo) ??
                        throw new InvalidOperationException();

      Engine.AddEdge(source: element, target: parent);
    }

    if (string.IsNullOrEmpty(value: element.ConnectTo))
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

    Engine.AddCluster(container: container);

    return this;
  }

  public void Render()
  {
    using var bitmap = new SKBitmap(width: 2000, height: 1000);
    using var canvas = new SKCanvas(bitmap: bitmap);
    canvas.Clear(color: SKColors.White);

    MsaglLayoutEngine.Graph.RootCluster.UserData = 1;

    Engine.Run();

    foreach (Node? node in
             MsaglLayoutEngine.Graph.RootCluster.Clusters
               .Select(selector: x => x.Nodes)
               .SelectMany(selector: x => x))
    {
      double x = node.Center.X;
      double y = node.Center.Y;

      var paint = new SKPaint
      {
        Color = SKColors.Blue,
        IsAntialias = true
      };

      canvas.DrawCircle(cx: (float)x, cy: (float)y, radius: 10,
                        paint: paint);

      var textPaint = new SKPaint
      {
        Color = SKColors.Black,
        TextSize = 16
      };

      canvas.DrawText(
                      text: ((IElement)node.UserData).Id ?? "",
                      x: (float)x - 20,
                      y: (float)y + 30,
                      paint: textPaint
                     );
    }

    foreach (Node node in MsaglLayoutEngine.Graph.Nodes)
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

    foreach (Edge? edge in MsaglLayoutEngine.Graph.Edges)
    {
      var edgePaint = new SKPaint
      {
        Color = SKColors.Black,
        StrokeWidth = 1,
        IsAntialias = true,
        Style = SKPaintStyle.Stroke
      };

      ICurve? edgeCurve = edge.EdgeGeometry.Curve;

      if (edgeCurve != null)
      {
        var path = new SKPath();

        var steps = 999;
        double tStep = 1.0 / steps;
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
    }

    using SKImage? image =
      SkiaSharp.SKImage.FromBitmap(bitmap: bitmap);
    using SKData? data =
      image.Encode(format: SkiaSharp.SKEncodedImageFormat.Jpeg,
                   quality: 1000);
    using FileStream stream =
      File.OpenWrite(path: "C:\\Users\\vinie\\diagramaa.png");
    data.SaveTo(target: stream);
  }
}
