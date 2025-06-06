using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;

namespace FluentDiagrams.NET.Core;

public interface IElement
{
  public string Id { get; }
  public string ImagePath { get; }
  public string ConnectTo { get; set; }
}
