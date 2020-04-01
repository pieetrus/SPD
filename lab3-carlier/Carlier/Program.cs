using System;
using System.Linq;

namespace Carlier
{
    class Program
    {
        static void Main(string[] args)
        {
            var rpq = new RPQ(3);
            var pi = new Task[rpq.n];
            var tasks = rpq.tasks;
            rpq.Schrage(pi);
            
            var b = rpq.GetB(pi);
            var a = rpq.GetA(pi,b);
            var c = rpq.GetC(pi,a, b);
            var k = rpq.GetK(pi, b, c);



            //Console.WriteLine("B:(index):  " + b);
            //Console.WriteLine("B:(wartość d):  " + tasks[b].d);
            //Console.WriteLine("B:(wartość c):  " + tasks[b].c);
            //Console.WriteLine("B:(wartość r):  " + tasks[b].r);
            //Console.WriteLine("B:(wartość p):  " + tasks[b].p);
            //Console.WriteLine("B:(wartość q):  " + tasks[b].q);

            //Console.WriteLine();
            //Console.WriteLine("A:(index):  " + a);
            //Console.WriteLine("A:(wartość d):  " + tasks[a].d);
            //Console.WriteLine("A:(wartość c):  " + tasks[a].c);
            //Console.WriteLine("A:(wartość r):  " + tasks[a].r);
            //Console.WriteLine("A:(wartość p):  " + tasks[a].p);
            //Console.WriteLine("A:(wartość q):  " + tasks[a].q);

            //Console.WriteLine();
            //Console.WriteLine("C:(index):  " + c);
            //Console.WriteLine("C:(wartość d):  " + tasks[c].d);
            //Console.WriteLine("C:(wartość c):  " + tasks[c].c);
            //Console.WriteLine("C:(wartość r):  " + tasks[c].r);
            //Console.WriteLine("C:(wartość p):  " + tasks[c].p);
            //Console.WriteLine("C:(wartość q):  " + tasks[c].q);

            //Console.WriteLine("hehe");
            //Console.WriteLine("hehe");
            //Console.WriteLine("hehe");

            //foreach (var item in k)
            //{
            //    Console.WriteLine(item.d);
            //}

            //var xd = rpq.Carlier();

            //Console.WriteLine(xd.Max(x=>x.d));

            Test.Run();


        }
    }
}
