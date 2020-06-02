using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace neh
{
    public class Neh
    {
        public int n { get; set; } // n zadań

        public int m { get; set; } // m maszyn

        public List<Task> Tasks { get; set; }

        public Neh(int number)
        {
            StreamReader file = new StreamReader(@"D:\REPO\SPD\neh\data\NEH" + number + ".DAT");
            Tasks = new List<Task>();
            var line = file.ReadLine();

            var lineSplited = line.Split(" ");

            n = int.Parse(lineSplited[0]);
            m = int.Parse(lineSplited[1]);

            for (int i = 0; i < n; i++)
            {
                line = file.ReadLine();
                var temp = line.Split(" ");
                var task = new Task();

                foreach (var item in temp)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                        task.PList.Add(int.Parse(item));
                }

                Tasks.Add(task);
            }
        }

        public int Calculate()
        {
            var tasksList = Tasks.OrderBy(x => x.PList.Sum()).ToList();
            var sequence = new List<Task>();
            sequence.Add(tasksList[0]);
            List<Task> bestSequence = new List<Task>();
            int bestCmax = 0;

            for (int i = 1; i < n; i++)
            {
                bestCmax = int.MaxValue;

                for (int j = 0; j < i+1; j++)
                {
                    var temp_sequence = new List<Task>();
                    foreach (var item in sequence)
                    {
                        temp_sequence.Add(item);
                    }
                    temp_sequence.Insert(j, tasksList[i]);
                    var cMaxSeq = C_max(temp_sequence, temp_sequence.Count(), m)[temp_sequence.Count(), m];
                    if (cMaxSeq < bestCmax)
                    {
                        bestSequence = temp_sequence;
                        bestCmax = cMaxSeq;
                    }
                }
                sequence = bestSequence;
            }

            return bestCmax;
        }



        public int[,] C_max(List<Task> tasks, int n, int m)
        {
            var c = new int[n+1, m+1];
            for (int i = 0; i < n+1; i++)
            {
                for (int j = 0; j < m+1; j++)
                {
                    c[i, j] = 0;
                }
            }

            for (int j = 1; j < n + 1; j++)
            {
                for (int k = 1; k < m + 1; k++)
                {
                    c[j, k] = Math.Max(c[j - 1, k], c[j, k - 1]) + tasks[j - 1].PList[k - 1];
                }
            }

            return c;
        }

    }
}
