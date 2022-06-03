using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
namespace small_world_phenomenon
{
    class FileManager
    {
        public FileManager()
        {
        }
        ~FileManager()
        {

        }
        public void readFile(ref Graph graph)
        {

            String movie = "";
            String actorName = "";

            List<String> actors = new List<String>();
            bool indicator = false;
            using (var f = File.OpenRead("E:/small world phenomenon/small world phenomenon/test.txt"))
            using (var reader = new StreamReader(f))
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    indicator = false;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] != '/' && indicator == false)
                        {
                            movie += line[i];
                        }
                        else
                        {
                            indicator = true;
                        }
                        if (indicator == true)
                        {
                            if (line[i] != '/')
                                actorName += line[i];
                            else
                            {
                                if (actorName != "")
                                {
                                    actors.Add(actorName);
                                    actorName = "";
                                }

                            }
                        }
                    }
                    if (actorName != "")
                        actors.Add(actorName);
                    graph.makeGraph(movie, actors);
                    actors.Clear();
                    actorName = "";
                    movie = "";
                }
        }
       
    }
}