using FluentDiagrams.NET.AWS;
using FluentDiagrams.NET.Core;

namespace FluentDiagrams.NET;

public class Class1
{
  public void Example()
  {


    var diagram = new Diagram();

    diagram.Ec2(instanceName: "ec2")
           .Vpc(vpcName: "vpc", config: vpc =>
           {


             vpc.Ec2(instanceName: "ec2");
             vpc.Ec2(instanceName: "ec3");
             vpc.Ec2(instanceName: "ec4");
           });

    diagram.Render();
  }
}
