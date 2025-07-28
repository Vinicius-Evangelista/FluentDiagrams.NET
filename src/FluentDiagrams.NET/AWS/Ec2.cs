using FluentDiagrams.NET.Core;

namespace FluentDiagrams.NET.AWS;

public class Ec2(string id, string connectTo) : IElement
{
  public string Id { get; } = id;
  public string ImagePath { get; } = @".\Icons\Architecture-Service-Icons_02072025\Arch_Compute\48\Arch_Amazon-EC2_48.png";
  public string ConnectTo { get; set; } = connectTo;
}
