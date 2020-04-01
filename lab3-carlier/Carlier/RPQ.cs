using System;
using System.IO;
using System.Linq;

namespace Carlier
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

            StreamReader file = new StreamReader(@"..\..\..\..\data\SCHRAGE" + this.number + ".DAT");
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

                tasks[i] = new Task { r = r, p = p, q = q , nr = i};
            }

            string[] fileAnwser = File.ReadAllLines(@"..\..\..\..\data\CARLIER" + number + ".OUT");

            anwser = int.Parse(fileAnwser[0]);
        }

        private int number; // zmienna pomocnicza do odczytywania numeru pliku z danymi
        public int anwser; // odpowiedź z pliku .OUT
        public int n { get; set; } // ilość zadań
        public Task[] tasks { get; set; } // zadania
        public int C_max { get; set; }


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
        public int Schrage(Task[] pi)
        {
            int t = 0; // chwila czasowa
            int k = 0; // pozycja w permutacji pi
            var G = new MaxPriorityQueue(); // zbiór zadań gotowych do realizacji
            var N = new MinPriorityQueue(tasks); // zbiór zadań nieuszeregowanych

            //Szukane
            var cmax = 0; // maksymalny z terminów dostarczenia zadań


            while (!G.IsEmpty() || !N.IsEmpty())
            {
                Task taskReady; // zadanie gotowe do wykonania
                while (!N.IsEmpty() && N.Peek().r <= t) // dopóki są jakieś nieuszeregowane zadania i jest dostępne zadanie w chwili czasowej t
                {
                    taskReady = N.Poll(); // weź zadanie z najmniejszym możliwym r, usuń je ze zbioru zadań nieuszeregowanych
                    G.Add(taskReady); // dodaj to zadanie do zbioru zadań uszeregowanych
                }

                if (G.IsEmpty()) // jeśli nie ma żadnych zadań gotowych do realizacji
                {
                    t = N.Peek().r; // przesuń chwilę czasową do najmniejszego dostępnego terminu dostępności zadania ze zbioru zadań nieuszeregowanych
                    continue;
                }
                // jeżeli są jakieś zadania gotowe do realizacji
                taskReady = G.Poll(); // weź zadanie z największym możliwym q, usuń je ze zbioru zadań gotowych do realizacji i "wstaw je na maszynę" - do permutacji pi
                taskReady.s = t;
                pi[k] = taskReady; // dodaj to zadanie do permutacji zadań wykonywanych na maszynie
                k += 1; // zwiększ pozycję w permutacji
                t += taskReady.p; // zwiększ chwilę czasową o czas wykonania zadania - p
                cmax = Math.Max(cmax, t + taskReady.q); // oblicz najpóźniejszy moment dostarczenia
            }

            C_max = cmax;
            return cmax;
        }

        /// <summary>
        /// Algorytm schrage z podziałem korzystający z kolejek priorytetowych
        /// Nie wylicza permutacji zadań na maszynie
        /// </summary>
        /// <returns>Maksymalny termin dostarczenia zadań</returns>
        public int SchragePMTN()
        {
            var t = 0; // chwila czasowa
            var G = new MaxPriorityQueue(); // kolejka zadań gotowych do wykonania
            var N = new MinPriorityQueue(tasks); // kolejka zadań nieuszeregowanych
            Task taskReady; // zadanie gotowe do wykonania
            Task taskOnMachine = new Task { q = int.MaxValue }; // aktualnie wykonywane zadanie na maszynie

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

        public Task[] Carlier(Task[] pi)
        {
            int U, UB = Int32.MaxValue;
            Task[] piOpt = new Task[n];

            U = Schrage(pi);
            if (U < UB)
            {
                UB = U;
                piOpt = pi;
            }

            //sekcja krytyczna

            var b = GetB(pi);
            var a = GetA(pi, b);
            var c = GetC(pi, a, b);

            if (c==-1)
            {
                //Console.WriteLine("Shrage znalazł rozwiązanie optymalne");
                return piOpt;
            }

            var k = GetK(pi, b, c);

            var r = k.Min(x => x.r);
            var q = k.Min(x => x.q);
            var p = k.Sum(task => task.p);

            //zapisz r zadania c
            var tempNr= pi[c].nr;
            var tempValue = pi[c].r;

            // wymuś aby zadanie c bylo wykonywanie PO bloku K
            pi[c].r = Math.Max(pi[c].r, r + p);

            var LB = SchragePMTN();

            if (LB < UB)
            {
                //Console.WriteLine("LB < UB 1");
                Carlier(pi);
            }

            // restore
            for (var i = 0; i < pi.Length; i++)
            {
                if (pi[i].nr == tempNr)
                {
                    pi[i].r = tempValue;
                    break;
                }
            }

            //zapisz 
            tempValue = pi[c].q;

            pi[c].q = Math.Max(pi[c].q, q + p);

            LB = SchragePMTN();
            if (LB < UB)
            {
                //Console.WriteLine("LB < UB 2");
                Carlier(pi);
            }

            //restore 
            for (var i = 0; i < pi.Length; i++)
            {
                if (pi[i].nr == tempNr)
                {
                    pi[i].q = tempValue;
                    break;
                }
            }

            return piOpt;
        }

        public int GetB(Task[] pi)
        {
            int index = 0;


            for (int i = n-1; i >= 0; i--)
            {
                if (C_max == pi[i].c + pi[i].q)
                {
                    index = i;
                    break;
                }
            }

            //Console.WriteLine(max);
            return index;
        }

        public int GetA(Task[] pi, int b)
        {
            var i = 0;

            for (i = 0; i < b; i++)
            {
                var suma = 0;
                for (int j = i; j <= b; j++)
                {
                    suma += pi[j].p; // suma czasu wykonania zadań od a do b
                }

                if (C_max == pi[i].r + suma + pi[b].q)
                {
                    return i;
                }
            }

            return i;
        }

        public int GetC(Task[] pi, int a, int b)
        {
            int c = -1;

            for (int i = b -1; i >= a; i--)
            {
                if (pi[i].q < pi[b].q)
                {
                    return i;
                }
            }


            return c;
        }

        public Task[] GetK(Task[] pi, int b, int c)
        {
            var size = b - c;
            var k = new Task[size];
            var index = c + 1;

            for (int i = 0; i < size; i++)
            {
                k[i] = pi[index + i];
            }

            return k;
        }

    }
}
