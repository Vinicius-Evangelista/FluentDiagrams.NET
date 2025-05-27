using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;

public class MsaglLayoutEngine : ILayoutEngine
{
  public GeometryGraph Graph { get; private set; } = new();

  public Node AddNode(string nodeId)
  {
    if (Graph.Nodes.FirstOrDefault(predicate: x => x.UserData.ToString() == nodeId) is not null)
      return Graph.Nodes.FirstOrDefault(predicate: x => x.UserData.ToString() == nodeId)!;

    if (string.IsNullOrEmpty(value: nodeId))
      throw new ArgumentNullException(paramName: nameof(nodeId));

    var node = new
      Node(curve: CurveFactory.CreateRectangle(
              width: 30,
              height: 20,
              center: new Point(xCoordinate: 0,
                                yCoordinate: 0)), userData: nodeId);

    Graph.Nodes.Add(item: node);

    return node;
  }

  public void AddEdge(string source, string target)
  {
    if (string.IsNullOrEmpty(value: source) ||
        string.IsNullOrEmpty(value: target))
      throw new ArgumentNullException(paramName: "source or target");


    Node? sourceNode =
      Graph.Nodes.FirstOrDefault(predicate: x =>
                                   x.UserData.ToString() == source);

    Node? targetNode =
      Graph.Nodes.FirstOrDefault(predicate: x =>
                                   x.UserData.ToString() == source);

    if (sourceNode is null)
      sourceNode = AddNode(nodeId: source);

    if (targetNode is null)
      targetNode = AddNode(nodeId: target);


    Graph.Edges.Add(item: new Edge(source: sourceNode, target: targetNode));
  }

  public void Run()
  {
    var layoutSettings =
      new Microsoft.Msagl.Layout.Layered.SugiyamaLayoutSettings()
      {
        LayerSeparation = 50,
        NodeSeparation = 30,
      };

    var layoutAlgorithm =
      new Microsoft.Msagl.Layout.Layered.
        LayeredLayout(geometryGraph: Graph,
                      settings: layoutSettings);

    layoutAlgorithm.Run();
  }
}
