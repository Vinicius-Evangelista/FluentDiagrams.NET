using FluentDiagrams.NET.Core;

namespace FluentDiagrams.NET.AWS;

public static class AwsExtensions
{
  private static Diagram Diagram { get; set; } = new();

  public static IComposable Ec2(this IComposable composable,
                             string instanceName,
                             string connectTo = null!)
  {
    var element = new Ec2(id: instanceName);
    composable.AddElement(element: element, parentId: connectTo);
    return composable;
  }

  public static IDiagram Vpc(this IComposable composable,
                             string vpcName,
                             Action<Vpc> config)
  {
    var vpc = new Vpc(id: vpcName);
    config(obj: vpc);
    Diagram.AddContainer(container: vpc);
    return Diagram;
  }

  public static IDiagram Render(this IComposable diagram) =>
    Diagram;
}
