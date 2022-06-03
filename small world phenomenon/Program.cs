using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using small_world_phenomenon;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            Graph graph = new Graph();
            FileManager filemanger = new FileManager();
            filemanger.readFile(ref graph);
            string[] readfile = System.IO.File.ReadAllLines(@"E:/small world phenomenon/small world phenomenon/queries200.txt");
            List<KeyValuePair<string, string>> question = new List<KeyValuePair<string, string>>();
            int cou = 0;
            foreach (string line in readfile)
            {
                cou++;
                string[] st = line.Split("/");
                question.Add(new KeyValuePair<string, string>(st[0], st[1]));
                List<string> m = new List<string>();
                List<int> actors = new List<int>();
                int dos = graph.DegreeOfSeperation(st[0], st[1]);
                int RelationStregnth1 = graph.RelationStregnth(st[0], st[1]);
                Console.WriteLine("Degree of Seperation is: " + dos);
                Console.WriteLine("Relation Stregnth is : " + RelationStregnth1);
                m = graph.MovieChain();
                Console.Write("Chain of Movies: ");
                for (int i = 0; i < m.Count; i++)
                {
                    Console.Write(m[i] + "--->");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("test number: " + cou);
                Console.WriteLine();
            }

        }
    }
}