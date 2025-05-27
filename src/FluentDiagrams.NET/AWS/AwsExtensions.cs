using FluentDiagrams.NET.Core;

namespace FluentDiagrams.NET.AWS;

public static class AwsExtensions
{
  private static Diagram Diagram { get; set; } = new();

  public static IDiagram Ec2(this IComposable diagram,
                             string instanceName,
                             string connectTo = null!)
  {
    var element = new Ec2(id: instanceName);
    Diagram.Add(element: element, parentId: connectTo);
    return Diagram;
  }

  public static IDiagram Vpc(this IDiagram diagram,
                             string vpcName,
                             Action<Vpc> config)
  {
    var vpc = new Vpc(id: vpcName);
    Diagram.Add(element: vpc);
    return Diagram;
  }
}
