using System;

using Interfaces;

namespace ImplOne
{
    public class FullSizeHello : IHello
    {
        public void SayHello(string name)
        {
            Console.WriteLine($"Good afternoon, dear {name}");
        }
    }
}
