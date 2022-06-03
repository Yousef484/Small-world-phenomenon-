using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace small_world_phenomenon
{
    class Graph
    {
        private Dictionary<int, List<KeyValuePair<int, string>>> adjList = new Dictionary<int, List<KeyValuePair<int, string>>>();
        private List<string> MoviesChain = new List<string>();
        private int degreeOfSeperation = -1;
        private bool[] checking = new bool[500000];
        private int[] distanceFromParent = new int[500000];
        private Dictionary<int, List<string>> chainOfMovies = new Dictionary<int, List<string>>();
        private Dictionary<(int, int), int> movieBetweenActors = new Dictionary<(int, int), int>();
        private Dictionary<int, List<int>> parent = new Dictionary<int, List<int>>();
        public int relation = -1;
        private HashSet<int> names = new HashSet<int>();
        private Dictionary<string, int> actorsMap = new Dictionary<string, int>();
        private int[] ActorsRelationStrength = new int[5000000];


        public Graph()
        {
        }

        public void makeGraph(string movie, List<string> actors)
        {
            int Firstcounter = 0, Secondcounter = 0;
            for (int i = 0; i < actors.Count; i++)
            {
                if (!actorsMap.ContainsKey(actors[i]))
                    actorsMap[actors[i]] = actorsMap.Count;


                Firstcounter = actorsMap[actors[i]];
                names.Add(Firstcounter);
                checking[Firstcounter] = false;
                distanceFromParent[Firstcounter] = 0;
                chainOfMovies[Firstcounter] = new List<string>();
                ActorsRelationStrength[Firstcounter] = 0;
                
                for (int j = 0; j < actors.Count; j++)
                {
                    if (i != j)
                    {
                        if (!actorsMap.ContainsKey(actors[j]))
                            actorsMap[actors[j]] = actorsMap.Count;
                        
                        Secondcounter = actorsMap[actors[j]];
                        
                        if (!movieBetweenActors.ContainsKey((Firstcounter, Secondcounter)))
                            this.movieBetweenActors[(Firstcounter, Secondcounter)] = 0;
                        
                        this.movieBetweenActors[(Firstcounter, Secondcounter)] += 1;

                        if (!this.adjList.ContainsKey(Firstcounter))
                            this.adjList[Firstcounter] = new List<KeyValuePair<int, string>>();

                        this.adjList[Firstcounter].Add(new KeyValuePair<int, string>(Secondcounter, movie));
                    }
                }
            }
           
        }

        private void ResetAllData(ref bool [] checking, ref int [] distance , ref int[] actorsRelationStrength , ref HashSet<int> names, ref Dictionary<int, List<string>> chainOfMovies )
        {
            checking = new bool[500000];
            distance = new int[500000];
            actorsRelationStrength = new int[500000];
            foreach (int st in names)
            {
                chainOfMovies[st] = new List<string>();
            }
            this.relation = 0;
            degreeOfSeperation = 0;
        }

        public void SolveOneAndThree(string firstActor, string secondActor)
        {
           
            ResetAllData(ref checking, ref distanceFromParent, ref ActorsRelationStrength, ref names, ref chainOfMovies);
           
            int first = actorsMap[firstActor], second = actorsMap[secondActor], currentNeighbour, actor;
            Queue<int> queueOfActors = new Queue<int>();        
            queueOfActors.Enqueue(first);
            checking[first] = true;
            while (queueOfActors.Count > 0)
            {
                actor = queueOfActors.Dequeue();

                for (int i = 0; i < adjList[actor].Count; i++)
                {
                    currentNeighbour = adjList[actor][i].Key;

                    if ((distanceFromParent[currentNeighbour] == (distanceFromParent[actor] + 1)) && (ActorsRelationStrength[actor] + movieBetweenActors[(actor, currentNeighbour)]) > ActorsRelationStrength[currentNeighbour])
                        ActorsRelationStrength[currentNeighbour] = (ActorsRelationStrength[actor] + movieBetweenActors[(actor, currentNeighbour)]);
                      
                    
                    if (currentNeighbour == second)
                    {

                        checking[currentNeighbour] = true;

                        if (this.degreeOfSeperation <= 0)
                        {
                            for (int m = 0; m < chainOfMovies[actor].Count; m++)
                                chainOfMovies[currentNeighbour].Add(chainOfMovies[actor][m]);
                            
                            this.chainOfMovies[currentNeighbour].Add(adjList[actor][i].Value);
                            this.MoviesChain = chainOfMovies[currentNeighbour];
                            
                            this.degreeOfSeperation = distanceFromParent[actor] + 1;
                        }

                        if (degreeOfSeperation == 1)
                            return;
                        
                        this.relation = Math.Max(this.relation, (ActorsRelationStrength[actor] + movieBetweenActors[(actor, currentNeighbour)]));

                    }
                    else if (checking[currentNeighbour] == false)
                    {

                        for (int m = 0; m < chainOfMovies[actor].Count; m++)
                        {
                            chainOfMovies[currentNeighbour].Add(chainOfMovies[actor][m]);
                        }
                        this.chainOfMovies[currentNeighbour].Add(adjList[actor][i].Value);

                       
                        distanceFromParent[currentNeighbour] = distanceFromParent[actor] + 1;
                        checking[currentNeighbour] = true;
                        ActorsRelationStrength[currentNeighbour] = Math.Max(ActorsRelationStrength[currentNeighbour], (ActorsRelationStrength[actor] + movieBetweenActors[(actor, currentNeighbour)]));
                        queueOfActors.Enqueue(currentNeighbour);
                    }

                    if (degreeOfSeperation > 0 && (distanceFromParent[actor] + 1 > degreeOfSeperation))
                        return;
                }


            }

        }
        public int DegreeOfSeperation(string firstActor, string secondActor)
        {
            SolveOneAndThree(firstActor, secondActor);

            return this.degreeOfSeperation;
        }

        public List<string> MovieChain()
        {
            return this.MoviesChain;
        }
      
        public int RelationStregnth(string firstActor, string secondActor)
        {
            int first = actorsMap[firstActor], second = actorsMap[secondActor];
            if (degreeOfSeperation == 1)
            {
                this.relation = 0;
                for (int i = 0; i < adjList[first].Count; i++)
                {
                    if (adjList[first][i].Key == second)
                    {
                        this.relation++;
                    }
                }
            }
            return this.relation;
        }


    }
}