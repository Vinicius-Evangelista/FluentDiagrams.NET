namespace FluentDiagrams.NET.Core;

public interface IDiagram : IComposable
{
  public IDiagram AddContainer(IContainer container);

  public void Render();
}
