namespace FluentDiagrams.NET.Core;

public interface IComposable
{
  public IComposable Add(IElement element, string? parentId = null);
}
