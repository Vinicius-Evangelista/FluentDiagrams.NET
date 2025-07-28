namespace FluentDiagrams.NET.Core;

interface IDiagram : IComposable
{
  public IDiagram AddContainer(IContainer container);

  public void Render();
}
