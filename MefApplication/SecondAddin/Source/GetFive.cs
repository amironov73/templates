using System.Composition;

using Common;

namespace SecondAddin
{
    [Export(typeof(IGetNumber))]
    public sealed class GetFive
        : IGetNumber
    {
        public int GetNumber() => 5;
    }
}
