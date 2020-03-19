using System;
using System.IO;

namespace lab2_Schrage
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Schrage bez podziału");
            Console.WriteLine();

            Console.WriteLine("{0,3} {1,8} {2,8}", "Nr", "Mój alg", "Odp");

            for (int i = 1; i < 10; i++)
            {
                RPQ rpq = new RPQ(i);
                var myAnwser = rpq.Schrage();
                var fileAnwser = rpq.anwser;

                Console.WriteLine("{0,3} {1,8} {2,8}", i, myAnwser, fileAnwser);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Schrage z podziałem");
            Console.WriteLine();
            Console.WriteLine("{0,3} {1,8} {2,8}", "Nr", "Mój alg", "Odp");

            for (int i = 1; i < 10; i++)
            {
                RPQ rpq = new RPQ(i);
                var myAnwser = rpq.SchrageWithDivision();
                var fileAnwser = rpq.anwser;

                Console.WriteLine("{0,3} {1,8} {2,8}", i, myAnwser, fileAnwser);
            }


            Console.ReadKey();
        }
    }
}
