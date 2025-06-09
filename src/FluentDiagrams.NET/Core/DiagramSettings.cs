namespace FluentDiagrams.NET.Core;

public class DiagramSettings
{
    public string? BackgroundColor { get; set; } = "#FFFFFF";
    public double Width { get; set; } = 2000;
    public double Height { get; set; } = 1000;
    public double ElementMargin { get; set; } = 20;
    public double ContainersMargin { get; set; } = 50;
    public double ElementWidth { get; set; } = 200;
    public double ElementHeight { get; set; } = 100;
    public string OutputPath { get; set; } = "";
    public string FileName { get; set; } = "diagram.png";
    public string FileFormat { get; set; } = "png";
    public string TextColor { get; set; } = "#000000";
    public string FontFamily { get; set; } = "Arial";
}
