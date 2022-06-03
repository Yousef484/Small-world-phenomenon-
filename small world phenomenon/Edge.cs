using System;
using System.Collections.Generic;
using System.Text;

namespace small_world_phenomenon
{
    class Edge
    {
        public string neighbour, movie;

        public Edge(string neighbour , string movie)
        {
            this.neighbour = neighbour;
            this.movie = movie;
        }

    }
}
