using FluentDiagrams.NET.Core;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Miscellaneous;

namespace FluentDiagrams.NET.LayoutEngine;

public class MsaglLayoutEngine
{
  public static GeometryGraph Graph { get; private set; } = new();

  static MsaglLayoutEngine() =>
    Graph.RootCluster.UserData = 1;

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
            width: 68,
            height: 68,
            center: new Point(xCoordinate: 0,
                              yCoordinate: 0)), userData: element);


    Graph.Nodes.Add(item: node);

    return node;
  }

  public void AddCluster(IContainer container)
  {
    if (container is null)
      throw new ArgumentNullException(paramName: nameof(container));

    if (string.IsNullOrEmpty(value: container.Id))
    {
      throw new
        ArgumentNullException(paramName: nameof(container.Id));
    }

    var cluster = new Cluster();

    foreach (Node? node in container.Elements.Select(selector:
               element => new
                 Node(curve: CurveFactory.CreateRectangle(
                       width: 68,
                       height: 68,
                       center: new Point(xCoordinate: 0,
                                         yCoordinate: 0)),
                      userData: element)))

      cluster.AddChild(child: node);

    cluster.UserData = container;

    Graph.RootCluster.AddChild(child: cluster);

    foreach (IElement element in container.Elements)
    {
      if (string.IsNullOrEmpty(value: element.ConnectTo))
        continue;

      Node? source = cluster.Nodes.FirstOrDefault(predicate: x =>
        ((IElement)x.UserData).Id ==
        element.Id);

      Node? target = Graph.Nodes.FirstOrDefault(predicate: x =>
                       ((IElement)x.UserData).Id ==
                       element.ConnectTo) ??
                     cluster.Nodes.FirstOrDefault(predicate: x =>
                       ((IElement)x.UserData).Id ==
                       element.ConnectTo);

      var edge = new Edge(source: source,
                          target: target);

      Graph.Edges.Add(item: edge);
    }
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

    sourceNode ??= AddNode(element: source);
    targetNode ??= AddNode(element: target);


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

  public static void Run(DiagramSettings diagramSettings)
  {
    var settings = new SugiyamaLayoutSettings()
    {
      EdgeRoutingSettings = new EdgeRoutingSettings
      {
        EdgeSeparationRectilinear = 10.0,
        EdgeRoutingMode = EdgeRoutingMode.RectilinearToCenter,
        RouteMultiEdgesAsBundles = true,
      },
      ClusterSettings =
        new Dictionary<object, LayoutAlgorithmSettings>()
        {
          {
            1, new SugiyamaLayoutSettings()
            {
              Transformation =
                PlaneTransformation.Rotation(angle: Math.PI / 2),
              ClusterMargin = 30,
              NodeSeparation = 40,
              LayerSeparation = 40,
              EdgeRoutingSettings =
                new EdgeRoutingSettings
                {
                  EdgeRoutingMode = EdgeRoutingMode.RectilinearToCenter,
                  RouteMultiEdgesAsBundles = true,

                },
            }
          }
        },
    };

    foreach (IElement? containers in Graph.RootCluster.Clusters.Select(selector: x => x.UserData as IElement))
    {
      if (!settings.ClusterSettings
                   .ContainsKey(key: containers.Id))
      {
        settings.ClusterSettings.Add(
                                     key: containers.Id,
                                     value: new
                                       SugiyamaLayoutSettings()
                                       {
                                         Transformation =
                                           PlaneTransformation
                                             .Rotation(angle:
                                               - Math.PI / 2),
                                         NodeSeparation = 25,
                                         ClusterMargin = 35,
                                         LayerSeparation = 45,
                                         EdgeRoutingSettings =
                                           new EdgeRoutingSettings
                                           {
                                             EdgeRoutingMode =
                                               EdgeRoutingMode
                                                 .RectilinearToCenter,
                                             RouteMultiEdgesAsBundles = false,
                                           }
                                       });
      }
    }

    Graph.MinimalHeight = diagramSettings.Height;
    Graph.MinimalWidth = diagramSettings.Width;

    LayoutHelpers
      .CalculateLayout(geometryGraph: Graph,
                       settings: settings, cancelToken: null);
  }
}
