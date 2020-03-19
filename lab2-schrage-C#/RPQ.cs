using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace lab2_Schrage
{
    public class RPQ
    {
        /// <summary>
        /// Konstruktor, który jako parametr przyjmuje numer pliku z którego chcemy wczytać dane
        /// </summary>
        /// <param name="number"></param>
        public RPQ(int number)
        {
            this.number = number;

            //string[] text = File.ReadAllLines(@"..\..\..\data\SCHRAGE" + this.number + ".DAT"); //cały text z pliku

            StreamReader file = new StreamReader(@"..\..\..\data\SCHRAGE" + this.number + ".DAT");
            var line = file.ReadLine();

            n = int.Parse(line);


            tasks = new Task[n];

            //tasks[0] = new Task { r = 0, p = 0, q = 0 };

            for (int i = 0; i < n; i++)
            {
                line = file.ReadLine();
                var entries = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                var r = int.Parse(entries[0]);
                var p = int.Parse(entries[1]);
                var q = int.Parse(entries[2]);

                tasks[i] = new Task{r= r, p = p, q = q};
            }

            string[] fileAnwser = File.ReadAllLines(@"..\..\..\data\SCHRAGE" + number + ".OUT");

            anwser = int.Parse(fileAnwser[0]);
        }

        private int number; // zmienna pomocnicza do odczytywania numeru pliku z danymi
        public int anwser; // odpowiedź z pliku .OUT
        public int n { get; set; } // ilość zadań
        public Task[] tasks { get; set; } // zadania
        

        /// <summary>
        /// Wyświetla parametry wszystkich zadań
        /// </summary>
        public void PrintRPQ()
        {
            System.Console.WriteLine("R P Q");
            Console.WriteLine("n:" + this.n);
            for (int i = 0; i < n; i++)
            {
                System.Console.Write(tasks[i].r);
                System.Console.Write(" ");
                System.Console.Write(tasks[i].p);
                System.Console.Write(" ");
                System.Console.Write(tasks[i].q);
                System.Console.WriteLine();
            }
        }

        /// <summary>
        /// Algorytm schrage bez podziału, korzystający z kolejek priorytetowych
        /// Wylicza permutacje zadań na maszynie
        /// </summary>
        /// <returns>Maksymalny czas dostarczenia zadań</returns>
        public int Schrage()
        {
            int t = 0; // chwila czasowa
            int k = 0; // pozycja w permutacji pi
            var G = new MaxPriorityQueue(); // zbiór zadań gotowych do realizacji
            var N = new MinPriorityQueue(tasks); // zbiór zadań nieuszeregowanych
            Task element; // zadanie gotowe do wykonania

            //Szukane
            var cmax = 0; // maksymalny z terminów dostarczenia zadań
            var pi = new Task[n]; // permutacja wykonania zadań na maszynie


            while (!G.IsEmpty() || !N.IsEmpty())
            {
                while (!N.IsEmpty() && N.Peek().r <= t) // dopóki są jakieś nieuszeregowane zadania i jest dostępne zadanie w chwili czasowej t
                {
                    element = N.Poll(); // weź zadanie z najmniejszym możliwym r, usuń je ze zbioru zadań nieuszeregowanych
                    G.Add(element); // dodaj to zadanie do zbioru zadań uszeregowanych
                }

                if (G.IsEmpty()) // jeśli nie ma żadnych zadań gotowych do realizacji
                {
                    t = N.Peek().r; // przesuń chwilę czasową do najmniejszego dostępnego terminu dostępności zadania ze zbioru zadań nieuszeregowanych
                    continue;
                }
                 // jeżeli są jakieś zadania gotowe do realizacji
                    element = G.Poll(); // weź zadanie z największym możliwym q, usuń je ze zbioru zadań gotowych do realizacji i "wstaw je na maszynę" - do permutacji pi
                    pi[k] = element; // dodaj to zadanie do permutacji zadań wykonywanych na maszynie
                    k += 1; // zwiększ pozycję w permutacji
                    t += element.p; // zwiększ chwilę czasową o czas wykonania zadania - p
                    cmax = Math.Max(cmax, t + element.q); // oblicz najpóźniejszy moment dostarczenia
            }
            return cmax;
        }

        /// <summary>
        /// Algorytm schrage z podziałem korzystający z kolejek priorytetowych
        /// Nie wylicza permutacji zadań na maszynie
        /// </summary>
        /// <returns>Maksymalny termin dostarczenia zadań</returns>
        public int SchrageWithDivision()
        {
            var t = 0; // chwila czasowa
            var G = new MaxPriorityQueue(); // kolejka zadań gotowych do wykonania
            var N = new MinPriorityQueue(tasks); // kolejka zadań nieuszeregowanych
            Task taskReady; // zadanie gotowe do wykonania
            Task taskOnMachine = new Task{q=int.MaxValue}; // aktualnie wykonywane zadanie na maszynie
            
            //szukane
            var cmax = 0; // maksymalny termin dostarczenia


            while (!G.IsEmpty() || !N.IsEmpty())
            {
                while (!N.IsEmpty() && N.Peek().r <= t)
                {
                    taskReady = N.Poll(); // zadanie gotowe do wykonania
                    G.Add(taskReady); // dodaj to zadanie do kolejki zadań gotowych do wykonania

                    //wprowadzenie podziału
                    //jeśli czas dostarczenia zadania gotowego do realizacji jest dłuższy 
                    //od zadania aktualnie znajdującego się na maszynie
                    if (taskReady.q > taskOnMachine.q)
                    {
                        taskOnMachine.p = t - taskReady.r; // wykonuj do zadanie do momentu, gdy następne zadanie jest gotowe
                        t = taskReady.r;
                        //  jeżeli do momentu aż zadanie będzie gotowe poprzednie się nie skończy
                        // to dodaj to zadanie do kolejki zadań gotowych do realizacji
                        // z p -> czasem wykonania, krótszym o tyle ile się zdążyło wykonać
                        if (taskOnMachine.p > 0) 
                        {
                            G.Add(taskOnMachine);
                        }
                    }
                }

                if (G.IsEmpty())
                {
                    t = N.Peek().r;
                }
                else
                {
                    taskReady = G.Poll();
                    taskOnMachine = taskReady;
                    t = t + taskReady.p;
                    cmax = Math.Max(cmax, t + taskReady.q);
                }
            }
            return cmax;
        }
    }
}

