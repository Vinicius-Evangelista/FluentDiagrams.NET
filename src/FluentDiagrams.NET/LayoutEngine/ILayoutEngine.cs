using FluentDiagrams.NET.Core;
using Microsoft.Msagl.Core.Layout;

public interface ILayoutEngine
{
    public Node AddNode(string nodeId);

    public void AddEdge(string source, string target);

    void Run();
}
