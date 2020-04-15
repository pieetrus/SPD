using System.Linq;

namespace Carlier
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskNumber = 3;

            for (int i = 1; i < 9; i++)
            {
                var rpq = new RPQ(i);
                var pi = new Task[rpq.n];
                var tasks = rpq.tasks;
                rpq.Schrage(pi);

                var b = rpq.GetB(pi);
                var a = rpq.GetA(pi, b);
                var c = rpq.GetC(pi, a, b);
                var k = rpq.GetK(pi, b, c);

                var r = k.Min(x => x.r);
                var q = k.Min(x => x.q);
                var p = k.Sum(task => task.p);
                if (c == -1)
                {
                    System.Console.WriteLine("-------------");
                    System.Console.WriteLine(i);
                    System.Console.WriteLine("-------------");
                    System.Console.WriteLine("b : " + b);
                    System.Console.WriteLine("a : " + a);
                }
                else
                {
                    System.Console.WriteLine("-------------");
                    System.Console.WriteLine(i);
                    System.Console.WriteLine("-------------");
                    System.Console.WriteLine("b : " + b);
                    System.Console.WriteLine("a : " + a);
                    System.Console.WriteLine("c : " + c);
                    System.Console.WriteLine("r : " + r);
                    System.Console.WriteLine("p : " + p);
                    System.Console.WriteLine("q : " + q);
                }
                
            }

            










        }
    }
}
