using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace lab2_Schrage
{
    public static class Test
    {
        private static readonly Stopwatch timer = new Stopwatch();

        public static int TestSchrageWithoutQueue()
        {
            timer.Reset();

            Console.WriteLine("Schrage bez podziału (Bez kolejki)");
            Console.WriteLine();

            Console.WriteLine("{0,3} {1,8} {2,8}", "Nr", "Cmax", "Odp");

            timer.Start();
            for (int i = 1; i < 10; i++)
            {
                RPQ rpq = new RPQ(i);
                var myAnwser = rpq.SchrageWithoutQueue();
                var fileAnwser = rpq.anwser;

                Console.WriteLine("{0,3} {1,8} {2,8}", i, myAnwser, fileAnwser);
            }
            timer.Stop();
            Console.WriteLine("Czas wykonywania: " + timer.Elapsed.Milliseconds + "ms");
            Console.WriteLine();
            return timer.Elapsed.Milliseconds;
        }

        public static int TestSchrageWithoutQueueWithDivision()
        {
            timer.Reset();

            Console.WriteLine("Schrage z podziałem (Bez kolejki)");
            Console.WriteLine();

            Console.WriteLine("{0,3} {1,8} {2,8}", "Nr", "Cmax", "Odp");

            timer.Start();
            for (int i = 1; i < 10; i++)
            {
                RPQ rpq = new RPQ(i);
                var myAnwser = rpq.SchrageWithoutQueueWithDivision();
                var fileAnwser = rpq.anwser;

                Console.WriteLine("{0,3} {1,8} {2,8}", i, myAnwser, fileAnwser);
            }
            timer.Stop();
            Console.WriteLine("Czas wykonywania: " + timer.Elapsed.Milliseconds + "ms");
            Console.WriteLine();
            return timer.Elapsed.Milliseconds;
        }

        public static int TestSchrage()
        {
            timer.Reset();

            Console.WriteLine("Schrage bez podziału (Z kolejką)");
            Console.WriteLine();

            Console.WriteLine("{0,3} {1,8} {2,8}", "Nr", "Cmax", "Odp");

            timer.Start();
            for (int i = 1; i < 10; i++)
            {
                RPQ rpq = new RPQ(i);
                var myAnwser = rpq.Schrage();
                var fileAnwser = rpq.anwser;

                Console.WriteLine("{0,3} {1,8} {2,8}", i, myAnwser, fileAnwser);
            }
            timer.Stop();
            Console.WriteLine("Czas wykonywania: " + timer.Elapsed.Milliseconds + "ms");
            Console.WriteLine();
            return timer.Elapsed.Milliseconds;
        }

        public static int TestSchrageWithDivision()
        {
            timer.Reset();

            Console.WriteLine("Schrage z podziałem (Z kolejką)");
            Console.WriteLine();

            Console.WriteLine("{0,3} {1,8} {2,8}", "Nr", "Cmax", "Odp");

            timer.Start();
            for (int i = 1; i < 10; i++)
            {
                RPQ rpq = new RPQ(i);
                var myAnwser = rpq.SchrageWithDivision();
                var fileAnwser = rpq.anwser;

                Console.WriteLine("{0,3} {1,8} {2,8}", i, myAnwser, fileAnwser);
            }
            timer.Stop();
            Console.WriteLine("Czas wykonywania: " + timer.Elapsed.Milliseconds + "ms");
            Console.WriteLine();
            return timer.Elapsed.Milliseconds;
        }

        public static void Run()
        {
            var test1 = TestSchrageWithoutQueue();
            var test2 = TestSchrageWithoutQueueWithDivision();
            var test3 = TestSchrage();
            var test4 = TestSchrageWithDivision();

            Console.WriteLine("Podsumowanie: ");
            Console.WriteLine("Schrage bez kolejki: " + test1 + "ms");
            Console.WriteLine("Schrage bez kolejki(z podziałem) " + test2 + "ms");
            Console.WriteLine("Schrage z kolejką: " + test3 + "ms");
            Console.WriteLine("Schrage z kolejką: (z podziałem) " + test4 + "ms");

        }
    }


    class Program
    {
        static void Main()
        {
            Test.Run();
        }
    }
}
