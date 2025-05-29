using FluentDiagrams.NET.LayoutEngine;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using SkiaSharp;

namespace FluentDiagrams.NET.Core;

public class Diagram : IDiagram
{
  private MsaglLayoutEngine Engine { get; set; } = new();

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

    Engine.Run();

    Rectangle bbox = Engine.Graph.BoundingBox;

    float canvasWidth = bitmap.Width;
    float canvasHeight = bitmap.Height;

    var graphWidth = (float)bbox.Width;
    var graphHeight = (float)bbox.Height;

    float translateX =
      (canvasWidth - graphWidth) / 2f - (float)bbox.Left;
    float translateY =
      (canvasHeight - graphHeight) / 2f - (float)bbox.Top;

    canvas.Translate(dx: translateX, dy: translateY);

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
      ICurve? edgeCurve = edge.EdgeGeometry.Curve;
      if (edgeCurve == null)
        continue;


      var path = new SKPath();
      path.MoveTo(x: (float)edgeCurve.Start.X, y: (float)edgeCurve.Start.Y);


        switch (edgeCurve)
        {
          case Microsoft.Msagl.Core.Geometry.Curves.LineSegment line:
            path.LineTo(x: (float)line.End.X, y: (float)line.End.Y);
            break;

          case Microsoft.Msagl.Core.Geometry.Curves.CubicBezierSegment bezier:
            path.CubicTo(
                         x0: (float)bezier[t: 1].X, y0: (float)bezier[t: 1].Y,  // First control point
                         x1: (float)bezier[t: 2].X, y1: (float)bezier[t: 2].Y,  // Second control point
                         x2: (float)bezier[t: 3].X, y2: (float)bezier[t: 3].Y
                        );
            break;

          case Microsoft.Msagl.Core.Geometry.Curves.Polyline polyline:
            for (var i = 1; i < polyline.Count(); i++)
              path.LineTo(x: (float)polyline[t: i].X, y: (float)polyline[t: i].Y);
            break;
        }

      var edgePaint = new SKPaint
      {
        Color = SKColors.Black,
        StrokeWidth = 2,
        IsAntialias = true,
        Style = SKPaintStyle.Stroke
      };

      canvas.DrawPath(path: path, paint: edgePaint);
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
