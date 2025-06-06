using FluentDiagrams.NET.Core;

namespace FluentDiagrams.NET.AWS;

public class Ec2(string id, string connectTo) : IElement
{
  public string Id { get; } = id;
  public string ImagePath { get; } = "aws/ec2.png";
  public string ConnectTo { get; set; } = connectTo;
}
