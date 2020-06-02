using System;

namespace neh
{
    class Program
    {
        static void Main(string[] args)
        {

            var neh = new Neh(3);

            neh.C_max(neh.Tasks, neh.n, neh.m);
            Console.WriteLine(neh.Calculate());
        }
    }
}
