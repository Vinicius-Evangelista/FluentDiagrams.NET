namespace FluentDiagrams.NET.Core;

public interface IComposable
{
  public IComposable AddElement(IElement element);
}
