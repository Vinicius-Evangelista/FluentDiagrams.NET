namespace FluentDiagrams.NET.Core;

public interface IContainer : IElement, IComposable
{
   public IReadOnlyCollection<IElement> Elements { get; }
}
