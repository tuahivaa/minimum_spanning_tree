using System;
using System.Collections.Generic;
using System.IO;

namespace own
{
    class MainClass
    {
        // Number of nodes in the graph 
        static int V;
        static List<string> graphWithLetters = new List<string>();

        // find the vertex with minimum key value, from the set of vertices not yet included in MST
        static int minKey(int[] key, bool[] mstSet)
        {

            // min value 
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }

            return min_index;
        }

        // print the constructed MST stored in parent[] 
        public static int total = 0;
        static string firstLine ="";
        static void printMST(int[] parent, int n, int[,] graph)
        {
            //Console.WriteLine("Edge \tWeight");
            for (int i = 1; i < V; i++)
            {
                //Console.WriteLine(parent[i] + " - " + i + "\t" +
                                    //graph[i, parent[i]] );

                //Console.WriteLine( firstLine[parent[i]] + " - " + firstLine[i] + "\t" +
                                     //graph[i, parent[i]] );

                total += graph[i,parent[i]];
                graphWithLetters.Add(firstLine[parent[i]] + " - " + firstLine[i] + "\t" + graph[i, parent[i]]) ;
                
            }
        }

        // Function to construct and  
        // print MST for a graph represented 
        // using adjacency matrix representation 
        static void primMST(int[,] graph)
        {

            // Array to store constructed MST 
            int[] parent = new int[V];

            // Key values used to pick 
            // minimum weight edge in cut 
            int[] key = new int[V];

            // To represent set of vertices 
            // not yet included in MST 
            bool[] mstSet = new bool[V];

            // Initialize all keys 
            // as INFINITE 
            for (int i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            // Always include first 1st vertex in MST. 
            // Make key 0 so that this vertex is 
            // picked as first vertex 
            // First node is always root of MST 
            key[0] = 0;
            parent[0] = -1;

            // The MST will have V vertices 
            for (int count = 0; count < V - 1; count++)
            {

                // Pick thd minimum key vertex 
                // from the set of vertices 
                // not yet included in MST 
                int u = minKey(key, mstSet);

                // Add the picked vertex 
                // to the MST Set 
                mstSet[u] = true;

                // Update key value and parent  
                // index of the adjacent vertices 
                // of the picked vertex. Consider 
                // only those vertices which are  
                // not yet included in MST 
                for (int v = 0; v < V; v++)

                    // graph[u][v] is non zero only  
                    // for adjacent vertices of m 
                    // mstSet[v] is false for vertices 
                    // not yet included in MST Update  
                    // the key only if graph[u][v] is 
                    // smaller than key[v] 
                    if (graph[u, v] != 0 && mstSet[v] == false &&
                                            graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
            }

            // print the constructed MST 
            printMST(parent, V, graph);
        }

        // Driver Code 
        public static void Main(string[] args)
        {
            try
            {
                //keep repeating to enter only one paramter
                if (args.Length == 1)
                {
                    StreamReader file = new StreamReader(args[0]);
            firstLine = file.ReadLine();
            V = firstLine.Length;
            int[,] graph2 = new int[V, V];


            //create an adjacent matrix with 0 for each index.
            for (int i =0; i<V; i++)
            {
                for ( int j =0; j<V; j++ )
                {
                    graph2[i,j]= 0;
                }
            }

            //we read the file and correct the weight in a for loop.
            while ( file.Peek() >= 0 ) {

                string line = file.ReadLine();
                string[] lines = line.Split(' ');

                //Console.WriteLine(lines[0] + lines[1] + lines [2]);

                int a = firstLine.IndexOf(lines[0]);
                int b = firstLine.IndexOf(lines[1]);
                //int a = int.Parse(lines[0]);
                //int b = int.Parse(lines[1]);
                int c = int.Parse(lines[2]);

                graph2[a, b] = c;
                graph2[b, a] = c;

            }

            //print the current adjacent matrix that we are using, just to see if it is correct.
            //for (int i = 0; i < V; i++)
            //{
            //    for (int j = 0; j < V; j++)
            //    {
            //        Console.Write(graph2[i,j]);
            //    }
            //    Console.WriteLine();
            //}



            int[,] graph = new int[,] {
                                   {0, 2, 1, 0, 0, 0, 0, 0, 0}, //A
                                   {0, 0, 0, 3, 2, 0, 0, 0, 0}, //B
                                   {1, 0, 0, 1, 0, 2, 0, 0, 0}, //C
                                   {0, 0, 3, 1, 4, 3, 0, 0, 0}, //D
                                   {0, 2, 0, 4, 0, 0, 1, 0, 0}, //E
                                   {0, 0, 2, 3, 0, 0, 3, 0, 0}, //F
                                   {0, 0, 0, 0, 0, 0, 0, 2, 2}, //G
                                   {0, 0, 0, 0, 0, 0, 2, 0, 2}, //H
                                   {0, 0, 0, 0, 0, 0, 2, 2, 0}, //I

                                   };

            // Print the solution 
            primMST(graph2);
            Console.WriteLine("MST has a weight of: " + total + ", and consists of these edges: ");
            foreach ( string a in graphWithLetters)
            {
                Console.WriteLine(a);
            }
                }
                else
                {
                    Console.WriteLine("please enter only one parameter.");
                }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found.");





            }
        }
    }
}
