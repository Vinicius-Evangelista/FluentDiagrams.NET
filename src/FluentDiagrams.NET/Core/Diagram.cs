using FluentDiagrams.NET.LayoutEngine;
using FluentDiagrams.NET.RenderingEngine;

namespace FluentDiagrams.NET.Core;

public class Diagram : IDiagram
{
  private MsaglLayoutEngine Engine { get; set; } = new();
  private static DiagramSettings Settings { get; set; } = new();

  public IDiagram SetSettings(DiagramSettings settings)
  {
    Settings = settings ??
               throw new
                 ArgumentNullException(paramName: nameof(settings));

    return this;
  }

  public IComposable AddElement(IElement element)
  {
    if (element is null)
      throw new ArgumentNullException(paramName: nameof(element));

    if (!string.IsNullOrEmpty(value: element.ConnectTo))
    {
      // TODO: Add personalized exception for not found parent
      IElement parent = GetElementById(id: element.ConnectTo) ??
                        throw new InvalidOperationException();

      Engine.AddEdge(source: element, target: parent);
    }

    if (string.IsNullOrEmpty(value: element.ConnectTo))
      Engine.AddNode(element: element);

    return this;
  }

  private IElement? GetElementById(string id)
  {
    if (string.IsNullOrEmpty(value: id))
      throw new ArgumentNullException(paramName: nameof(id));

    return Engine.GetElementById(id: id);
  }

  public IDiagram AddContainer(IContainer container)
  {
    if (container is null)
      throw new ArgumentNullException(paramName: nameof(container));

    Engine.AddCluster(container: container);

    return this;
  }

  public void Render()
  {
    MsaglLayoutEngine.Run(diagramSettings: Settings);
    DiagramRender.Render(graph: MsaglLayoutEngine.Graph,
                         settings: Settings);
  }
}
