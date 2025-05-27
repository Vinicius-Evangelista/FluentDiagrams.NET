using FluentDiagrams.NET.Core;

namespace FluentDiagrams.NET.AWS;

public class Vpc(string id) : IContainer
{
  public IReadOnlyCollection<IElement> Elements { get; } = [];

public IComposable Add(IElement element, string? parentId = null) =>
    throw new NotImplementedException();
  public string Id { get; } = id;

  public string ImagePath { get; } = "aws/Vpc.png";
}
