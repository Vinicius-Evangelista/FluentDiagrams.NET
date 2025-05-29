using FluentDiagrams.NET.Core;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Layout.MDS;
using Microsoft.Msagl.Miscellaneous;
using Microsoft.Msagl.Prototype.Ranking;

namespace FluentDiagrams.NET.LayoutEngine;

public class MsaglLayoutEngine : ILayoutEngine
{
  public GeometryGraph Graph { get; private set; } = new();

  public Node AddNode(IElement element)
  {
    Node? existingNode =
      Graph.Nodes.FirstOrDefault(predicate: x =>
                                   ((IElement)x.UserData).Id ==
                                   element.Id);

    if (existingNode is not null)
      return existingNode;

    if (string.IsNullOrEmpty(value: element.Id))
      throw new ArgumentNullException(paramName: nameof(element.Id));

    var node = new
      Node(curve: CurveFactory.CreateRectangle(
            width: 30,
            height: 20,
            center: new Point(xCoordinate: 0,
                              yCoordinate: 0)), userData: element);

    Graph.Nodes.Add(item: node);

    return node;
  }

  public void AddEdge(IElement source, IElement target)
  {
    if (string.IsNullOrEmpty(value: source.ImagePath) ||
        string.IsNullOrEmpty(value: target.ImagePath))
      throw new ArgumentNullException(paramName: nameof(source));

    Node? sourceNode =
      Graph.Nodes.FirstOrDefault(predicate: x =>
                                   ((IElement)x.UserData).Id ==
                                   source.Id);

    Node? targetNode =
      Graph.Nodes.FirstOrDefault(predicate: x =>
                                   ((IElement)x.UserData).Id ==
                                   target.Id);

    if (sourceNode is null)
      sourceNode = AddNode(element: source);


    if (targetNode is null)
      targetNode = AddNode(element: target);

    Graph.Edges.Add(item: new Edge(source: targetNode,
                                   target: sourceNode));
  }

  public IElement? GetElementById(string id)
  {
    Node? node =
      Graph.Nodes.FirstOrDefault(predicate: x =>
                                   ((IElement)x.UserData).Id == id);

    return node?.UserData as IElement;
  }

  public void Run()
  {
    var settings = new SugiyamaLayoutSettings()
    {
      NodeSeparation = 20,
      ClusterMargin = 10,
      ClusterSettings =
        new Dictionary<object, LayoutAlgorithmSettings>()
        {
          {
            2, new RankingLayoutSettings()
            {
              NodeSeparation = 15, EdgeRoutingSettings =
                new EdgeRoutingSettings
                {
                  EdgeSeparationRectilinear = 4.0,

                  EdgeRoutingMode = EdgeRoutingMode.Rectilinear,
                },
                LiftCrossEdges = true,
            }
          },
          {
            1, new SugiyamaLayoutSettings()
            {
              NodeSeparation = 15,
              EdgeRoutingSettings =
                new EdgeRoutingSettings
                {
                  EdgeSeparationRectilinear = 4.0,

                  EdgeRoutingMode = EdgeRoutingMode.Rectilinear,
                },
                LiftCrossEdges = true,
            }
          }
        },

      EdgeRoutingSettings = new EdgeRoutingSettings
      {
        EdgeSeparationRectilinear = 4.0,

        EdgeRoutingMode = EdgeRoutingMode.Rectilinear,
      },
      LiftCrossEdges = true,
    };

    Graph.RootCluster.SetInitialLayoutState( padding: 20);

    Microsoft.Msagl.Miscellaneous.LayoutHelpers
             .CalculateLayout(geometryGraph: Graph,
                              settings: settings, cancelToken: null);
  }
}
