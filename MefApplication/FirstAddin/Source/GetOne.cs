using System.Composition;

using Common;

namespace FirstAddin
{
    [Export(typeof(IGetNumber))]
    public sealed class GetOne
         : IGetNumber
    {
        public int GetNumber() => 1;
    }
}
