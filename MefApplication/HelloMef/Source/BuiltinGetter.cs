using System.Composition;

using Common;

namespace HelloMef
{
    [Export(typeof(IGetNumber))]
    public sealed class BuiltinGetter
        : IGetNumber
    {
        public int GetNumber() => 0;
    }
}
