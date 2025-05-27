using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl;
using SkiaSharp;


namespace FluentDiagrams.NET.Core;

public class Diagram : IDiagram
{
  private MsaglLayoutEngine Engine { get; set; } = new();


  public IComposable Add(IElement element, string? parentId = null)
  {
    if (element is null)
      throw new ArgumentNullException(paramName: nameof(element));

    if (parentId is not null)
      Engine.AddEdge(source: parentId, target: element.Id);

    Engine.AddNode(nodeId: element.Id);

    return this;
  }

  public IDiagram Add(IContainer container)
  {
    if (container is null)
      throw new ArgumentNullException(paramName: nameof(container));

    foreach (IElement element in container.Elements)
      Add(element: element, parentId: container.Id);

    return this;
  }


  public void Render()
  {
    using var bitmap =
      new SkiaSharp.SKBitmap(width: 1000, height: 1000);
    using var canvas = new SkiaSharp.SKCanvas(bitmap: bitmap);
    canvas.Clear(color: SkiaSharp.SKColors.White);

    Engine.Run();


    foreach (Node? node in Engine.Graph.Nodes)
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

      // Desenha o texto do Id do nó
      var textPaint = new SkiaSharp.SKPaint
      {
        Color = SkiaSharp.SKColors.Black,
        TextSize = 16
      };

      canvas.DrawText(text: node.UserData.ToString() ?? "", x: (float)(x + 12),
                      y: (float)(y + 5), paint: textPaint);
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
