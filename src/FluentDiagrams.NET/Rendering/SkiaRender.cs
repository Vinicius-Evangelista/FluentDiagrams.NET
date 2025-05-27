using SkiaSharp;

namespace FluentDiagrams.NET.Rendering;

public static class SkiaRenderer
{
  public static void RenderToPng(Stream outputStream, Action<SKCanvas> drawAction, int width = 512, int height = 512)
  {
    using var bitmap = new SKBitmap(width: width, height: height);
    using var canvas = new SKCanvas(bitmap: bitmap);

    canvas.Clear(color: SKColors.White); // fundo branco
    drawAction(obj: canvas); // seu desenho aqui

    using SKImage? image = SKImage.FromBitmap(bitmap: bitmap);
    using SKData? data = image.Encode(format: SKEncodedImageFormat.Png, quality: 100);
    data.SaveTo(target: outputStream);
  }
}

