using System.Collections.Generic;

namespace HelloMef
{
    public interface INumberProvider
    {
        IEnumerable<GotNumber> GetNumbers();
    }
}
