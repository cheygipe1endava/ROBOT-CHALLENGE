using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO; //FIND THE TEXT FILE
using System.IO.Compression;
using System.Linq;


namespace ROBOT_APP
{

    class Program
    {

        public static string ReadingText()
        {
            string filePath = Path.GetFullPath("prueba2.txt");


            StreamReader ReadFile = new StreamReader(filePath); //TEXT READER THAT READS CHARACTERS FROM A BYTE STREAM IN A PARTICULAR ENCODING
            String FileRead = ReadFile.ReadToEnd();//READS ALL CHARACTERS FROM THE CURRENT POSITION TO THE END OF THE STREAM

            return FileRead;
        }




        public static int[,] WeightMatrix()
        {
            string FileRead2 = ReadingText();
            string[] stringSeparators = new string[] { "\r\n", "," };

            int z = 0, h = 0, i = 0, j = 0, x = 0;
            string a, b;


            string[] words = FileRead2.Split(stringSeparators, StringSplitOptions.None);


            while (words[z].Length != 0)//COUNT ONLY THE NODES
            {
                z++;
            }

            string[] NodeList = new string[z];


            for (h = 0; h < z; h++)//FILL ARRAY WITH ONLY THE NODES
            {

                NodeList[h] = words[h];
                //Console.WriteLine(NodeList[h]);

            }

            int z2 = z + 1;//SETS POSITION TO READ THE STRINGS THAT ARE AFTER THE LINE BREAK.
            int z3 = (words.Length - z2) / 3;//GETS THE EDGE MATRIX [n,0] DIMENSION

            string[,] Edges = new string[z3, 3];
            int[,] WeightMatrix = new int[z, z];


            while (z2 < words.Length)
            {
                for (h = 0; h < z3; h++)

                {
                    for (i = 0; i < 3; i++)
                    {

                        Edges[h, i] = words[z2];
                        z2++;


                    }
                }
            }


            for (h = 0; h < z; h++)
            {
                for (i = 0; i < z3; i++)
                {
                    if (NodeList[h] == Edges[i, 0])
                    {
                        a = Edges[i, 1];
                        b = Edges[i, 2];
                        x = Int32.Parse(b);

                        for (j = h + 1; j < z; j++)
                        {
                            if (a == NodeList[j])
                            {
                                WeightMatrix[h, j] = x;
                                WeightMatrix[j, h] = x;
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {

                    }
                }
            }

            string[] NodoDisconexo = new string[z];


            for (i = 0; i < z; i++)
            {
                for (j = 0; j < z3; j++)
                {
                    if (NodoDisconexo.Contains(Edges[j, 0]))
                    {
                        //Console.WriteLine("YA EXISTE");
                    }
                    else
                    {
                        NodoDisconexo[i] = Edges[j, 0];
                        //Console.WriteLine("NO EXISTE");
                    }
                    if (NodoDisconexo.Contains(Edges[j, 1]))
                    {
                        //Console.WriteLine("YA EXISTE");
                    }
                    else
                    {
                        NodoDisconexo[i] = Edges[j, 1];
                        //Console.WriteLine("NO EXISTE");
                    }
                }
            }




            for (i = 0; i < z; i++)//WEIGHTED MATRIX ZERO DIAGONAL
            {
                for (j = 0; j < z; j++)
                {
                    if (i == j)
                    {
                        WeightMatrix[i, j] = 0;
                    }
                }
            }

            return WeightMatrix;

        }




        static int minDistance(int[] dist, bool[] sptSet, int CountVertices)
        {
            // Initialize min value 
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < CountVertices; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        // A utility function to print 
        // the constructed distance array 
        static void printSolution(int[] dist, int n)
        {
            Console.Write("Vertex     Distance "
                          + "from Source\n");
            for (int i = 0; i < n; i++)
                Console.Write(i + " \t\t " + dist[i] + "\n");
        }

        // Function that implements Dijkstra's 
        // single source shortest path algorithm 
        // for a graph represented using adjacency 
        // matrix representation 
        static void Dijkstra(int[,] graph, int src, int ending, int CountVertices)
        {
            int[] dist = new int[CountVertices]; // The output array. dist[i] 
                                     // will hold the shortest 
                                     // distance from src to i 

            // sptSet[i] will true if vertex 
            // i is included in shortest path 
            // tree or shortest distance from 
            // src to i is finalized 
            bool[] sptSet = new bool[CountVertices];

            // Initialize all distances as 
            // INFINITE and stpSet[] as false 
            for (int i = 0; i < CountVertices; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            // Distance of source vertex 
            // from itself is always 0 
            dist[src] = 0;

            // Find shortest path for all vertices 
            for (int count = 0; count < CountVertices - 1; count++)
            {
                // Pick the minimum distance vertex 
                // from the set of vertices not yet 
                // processed. u is always equal to 
                // src in first iteration. 
                int u = minDistance(dist, sptSet, CountVertices);

                // Mark the picked vertex as processed 
                sptSet[u] = true;

                // Update dist value of the adjacent 
                // vertices of the picked vertex. 
                for (int v = 0; v < CountVertices; v++)

                    // Update dist[v] only if is not in 
                    // sptSet, there is an edge from u 
                    // to v, and total weight of path 
                    // from src to v through u is smaller 
                    // than current value of dist[v] 
                    if (!sptSet[v] && graph[u, v] != 0 &&
                         dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }

            // print the constructed distance array 
            printSolution(dist, CountVertices);
        }



        public static void DijkstraImplementation()
        {
            Console.WriteLine("\n//------------------------DIJKSTRA------------------------//\n");
            int[,] WeightCopy = WeightMatrix();
            int XLength = WeightCopy.GetLength(0);
            int YLength = WeightCopy.GetLength(1);
            for (int x = 0; x < XLength; x++)
            {
                for (int y = 0; y < YLength; y++)
                {
                    if (x == y)
                    {
                        WeightCopy[x, y] = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (WeightCopy[x, y] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(WeightCopy[x, y] + "\t");
                }
                Console.WriteLine(Environment.NewLine);
            }

            Console.Write("ENTER INITIAL NODE: ");
            string InputOption3 = Console.ReadLine();
            int Input3 = Int32.Parse(InputOption3);

            Console.Write("ENTER FINAL NODE: ");
            string InputOption4 = Console.ReadLine();                     
            int Input4 = Int32.Parse(InputOption4);

            Dijkstra(WeightCopy, Input3, Input4, XLength);

        }
          

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("WHICH OPERATION DO YOU WANT TO EXECUTE?");
            Console.WriteLine("1. PASS THROUGH EACH NODE FROM AN INITIAL NODE\n" +
                "2. FIND SHORTEST PATH FROM INITIAL TO ENDING NODE\n");
            Console.Write("ENTER OPTION: ");
            string InputOption = Console.ReadLine();
            int Input2 = Int32.Parse(InputOption);

            switch(Input2)
            {
                case 1:
                    break;

                case 2:
                    DijkstraImplementation();
                    break;

                default:
                    Console.WriteLine("Tu seleccion es invalida, prueba de nuevo");
                    break;
            }

            
            Console.ReadKey();
        }
        
    }
}