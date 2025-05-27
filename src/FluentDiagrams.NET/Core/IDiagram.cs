namespace FluentDiagrams.NET.Core;

public interface IDiagram : IComposable
{
  public IDiagram Add(IContainer container);

  public void Render();
}
