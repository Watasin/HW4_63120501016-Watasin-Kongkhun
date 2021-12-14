using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // First create the graph
            int[,] graph = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                                      { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                                      { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                                      { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                                      { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                                      { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
                                      { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
                                      { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                                      { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
            Dijkstra dijk = new Dijkstra();
            dijk.dijkstraAlgo(graph, 0);
        }
    }

    public class Dijkstra
    {
        // A utility function to find the vertex with minimum distance value, from the set of vertices not yet included in shortest path tree 
        static int numberOfVertices = 9;

        int minDistance(int[] dist, bool[] sptSet)
        {
            // Initialize min value 
            int min = int.MaxValue, min_index = -1;
            for (int v = 0; v < numberOfVertices; v++)
            {
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }
            }
            return min_index;
        }

        // A utility function to print the constructed distance array 
        void printPaths(int[] dist, int n, int srcVert)
        {
            Console.Write("Vertex Distance from Source {0}\n", srcVert);
            for (int i = 0; i < numberOfVertices; i++)
            {
                Console.Write(i + " \t\t " + dist[i] + "\n");
            }
        }

        // Function that implements Dijkstra's single source shortest path algorithm 
        // for a graph represented using adjacency matrix representation 
        public void dijkstraAlgo(int[,] graph, int srcVert)
        {
            int[] dist = new int[numberOfVertices];
            // The output array. dist[i] will hold the shortest distance from src to i 
            // sptSet[i] will true if vertex i is included in shortest path tree or shortest distance from src to i is finalized 
            bool[] sptSet = new bool[numberOfVertices];

            // Initialize all distances as INFINITE and stpSet[] as false 
            for (int i = 0; i < numberOfVertices; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            // Distance of source vertex from itself is always 0 
            dist[srcVert] = 0;
            // Find shortest path for all vertices 
            for (int count = 0; count < numberOfVertices - 1; count++)
            {
                // Pick the minimum distance vertex from the set of vertices not yet processed. 
                // minDistIndx is always equal to srcVert in first iteration. 
                int minDistIndx = minDistance(dist, sptSet);
                // Mark picked vertex as processed 
                sptSet[minDistIndx] = true;
                // Update dist value of the adjacent vertices of the picked vertex. 
                for (int indx = 0; indx < numberOfVertices; indx++)
                {
                    // Update dist[v] only if is not in sptSet, there is an edge from u to v, 
                    // and total weight of path from src to v through u is smaller than current value of dist[v] 
                    if (!sptSet[indx] && graph[minDistIndx, indx] != 0 && dist[minDistIndx] != int.MaxValue && dist[minDistIndx] + graph[minDistIndx, indx] < dist[indx])
                    {
                        dist[indx] = dist[minDistIndx] + graph[minDistIndx, indx];
                    }
                }
            }
            // print the constructed distance array 
            printPaths(dist, numberOfVertices, srcVert);
        }
    }
}