using FluentDiagrams.NET.Core;
using Microsoft.Msagl.Core.Layout;

namespace FluentDiagrams.NET.AWS;

public class Vpc(string id) : IContainer
{
  public List<IElement> Elements { get; private set; } = [];

  public IComposable AddElement(IElement element, string? parentId = null!)
  {
    Elements.Add(item: element);
    return this;
  }

  public string Id { get; } = id;
  public string ImagePath { get; } = "aws/Vpc.png";
}
