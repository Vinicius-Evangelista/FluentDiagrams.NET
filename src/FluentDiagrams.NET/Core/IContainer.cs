namespace FluentDiagrams.NET.Core;

public interface IContainer : IElement, IComposable
{
   public List<IElement> Elements { get; }
}
