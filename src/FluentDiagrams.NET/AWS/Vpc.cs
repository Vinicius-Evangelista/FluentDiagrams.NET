using FluentDiagrams.NET.Core;
using Microsoft.Msagl.Core.Layout;

namespace FluentDiagrams.NET.AWS;

public class Vpc(string id) : IContainer
{
  public List<IElement> Elements { get; private set; } = [];

  public IComposable AddElement(IElement element)
  {
    Elements.Add(item: element);
    return this;
  }

  public string Id { get; } = id;
  public string ImagePath { get; } = "C:\\Users\\vinie\\RiderProjects\\FluentDiagrams.NET\\src\\FluentDiagrams.NET\\AWS\\Icons\\Architecture-Service-Icons_02072025\\Arch_Networking-Content-Delivery\\48\\Arch_Amazon-Virtual-Private-Cloud_48.png";
  public string ConnectTo { get; set; } = null!;
}
