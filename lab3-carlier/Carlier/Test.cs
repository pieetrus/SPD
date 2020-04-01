using System;
using System.Diagnostics;
using System.Linq;

namespace Carlier
{
    public static class Test
    {
        private static readonly Stopwatch timer = new Stopwatch();
        private static int _amount = 10;

        public static int TestCarlier()
        {
            timer.Reset();

            Console.WriteLine("Carlier");
            Console.WriteLine();

            Console.WriteLine("{0,3} {1,8} {2,8} {3,8} ", "Nr", "Cmax", "Odp", "Diff");

            timer.Start();
            for (int i = 1; i <= _amount; i++)
            {
                RPQ rpq = new RPQ(i);
                var myAnwser = rpq.Carlier(new Task[rpq.n]).Max(x=>x.d);
                var fileAnwser = rpq.anwser;
                var diff = fileAnwser - myAnwser;

                Console.WriteLine("{0,3} {1,8} {2,8} {3,8}", i, myAnwser, fileAnwser, diff);
            }
            timer.Stop();
            Console.WriteLine("Czas wykonywania: " + timer.Elapsed.Milliseconds + "ms");
            Console.WriteLine();
            return timer.Elapsed.Milliseconds;
        }

        public static void Run()
        {
            var test3 = TestCarlier();


            Console.WriteLine("Podsumowanie: ");
            Console.WriteLine("Carlier: " + test3 + "ms");


        }
    }
}
