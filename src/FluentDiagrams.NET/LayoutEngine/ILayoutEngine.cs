using FluentDiagrams.NET.Core;
using Microsoft.Msagl.Core.Layout;

public interface ILayoutEngine
{
    public Node AddNode(IElement element);

    public void AddEdge(IElement source, IElement target);

    void Run();
}
