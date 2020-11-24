using System;

using Interfaces;

namespace ImplTwo
{
    public class ConciseHello : IHello
    {
        public void SayHello(string name)
        {
            Console.WriteLine($"Hi, {name}");
        }
    }
}
