using System;
using System.Text;
using System.Collections;
using System.IO; //FIN THE TEXT FILE
using System.IO.Compression;
using System.Linq;


namespace APLICACION_ROBOT
{
    public static class Program
    {
        public const int inf = 9999;
        public const int Vertices = 4;

        static void Main(string[] args)
        {

            int[,] grafo =
                    {
                        {0, 9, inf, 10},
                        {inf, 0, 7, inf},
                        {14, inf, 0, 11},
                        {inf, inf, inf, 0}
                    };

            //MetPrincipal(grafo);            
            InputProcess();
            Console.ReadKey();

        }





        static void MetPrincipal(int[,] grafo)
        {
            int[,] distancia = new int[Vertices, Vertices];
            int inicio = 0;
            int mitad = 0;
            int final = 0;
            int i = 0;
            int j = 0;

            //IGUALATION FOR BOTH MATRIX
            for (i = 0; i < Vertices; i++)
            {
                for (j = 0; j < Vertices; j++)
                {
                    distancia[i, j] = grafo[i, j];
                }
            }

            //COMPARATION AND IGUALATION TO OPTIMAL DISTANCES
            for (mitad = 0; mitad < Vertices; mitad++)
            {
                for (inicio = 0; inicio < Vertices; inicio++)
                {
                    for (final = 0; final < Vertices; final++)
                    {
                        if (distancia[inicio, final] > distancia[inicio, mitad] + distancia[mitad, final])
                        {
                            distancia[inicio, final] = distancia[inicio, mitad] + distancia[mitad, final];
                        }
                    }
                }
            }

            //GRAPH RESULT PRINT
            for (i = 0; i < Vertices; i++)
            {
                for (j = 0; j < Vertices; j++)
                {
                    if (distancia[i, j] == inf)
                    {
                        Console.Write("∞\t");
                    }
                    else
                    {
                        Console.Write(distancia[i, j] + "\t");
                    }
                }

                Console.WriteLine();

            }
        }


        public static void Rename(this FileInfo fileInfo, string newName)
        {
            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);

        }



        public static void InputProcess()
        {

            int z = 0, h = 0, i = 0, j = 0, x = 0;
            string a, b;


            string[] stringSeparators = new string[] { "\r\n", "," };


            string filePath = Path.GetFullPath("prueba2.txt");


            StreamReader ReadFile = new StreamReader(filePath); //TEXT READER THAT READS CHARACTERS FROM A BYTE STREAM IN A PARTICULAR ENCODING
            String FileRead = ReadFile.ReadToEnd();//READS ALL CHARACTERS FROM THE CURRENT POSITION TO THE END OF THE STREAM

            //char[] AfterLim = { '\n', ',' };
            //byte[] bytes = Encoding.Default.GetBytes(FileRead);
            //string FileText = Encoding.UTF8.GetString(bytes);

            //string[] words = FileRead.Split(AfterLim);//FILLS ONLY THE STRINGS READ BEFORE THE LINE BREAK.    


            string[] words = FileRead.Split(stringSeparators, StringSplitOptions.None);


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


            /*
            for (h = 0; h < z3; h++)//EDGE MATRIX PRINT
            {
                i = 0;
                Console.WriteLine(Edges[h, i] + Edges[h, i + 1] + Edges[h, i + 2]);
            }
            */


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

            for (i = 0; i < z; i++)
            {
                if (!NodoDisconexo.Contains(NodeList[i]))//DISCONNECTED NODES 
                {
                    for (j = 0; j < z; j++)
                    {
                        WeightMatrix[i, j] = 900;
                        WeightMatrix[j, i] = 900;

                        //Console.Write(WeightMatrix[i, j] + "\t");
                    }
                    //Console.WriteLine(NodeList[i] + " NO TIENE CONEXIONES");
                }
                else
                {

                }
            }


            /*
            for (i = 0; i < z; i++) 
            {
                Console.WriteLine(NodoDisconexo[i]);
            }
            */

            for (i = 0; i < z; i++)//WEIGHTED MATRIX PRINT 
            {
                for (j = 0; j < z; j++)
                {
                    if (i == j)
                    {
                        WeightMatrix[i, j] = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                        //Console.Write(WeightMatrix[i, j] + "\t");

                    }
                    else if (WeightMatrix[i, j] == 0)
                    {
                        WeightMatrix[i, j] = 800;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        //Console.Write(WeightMatrix[i, j] + "\t");
                    }
                    else if (WeightMatrix[i, j] == 900)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(WeightMatrix[i, j] + "\t");
                }
                //Console.WriteLine();
                Console.WriteLine(Environment.NewLine);
            }



            //---------------------------------PRIM ALGORITHM---------------------------------------

            string[] VisitedNodes = new string[z];
            int N1 = Convert.ToInt32(Console.ReadLine());
            int minvalue = 0;
            minvalue = WeightMatrix[N1, 0];

            for (i = 1; i < z; i++)
            {

                if (minvalue >= WeightMatrix[N1, i] && WeightMatrix[N1, i] > 0 && WeightMatrix[N1, i] < 800)
                {
                    minvalue = WeightMatrix[N1, i];
                }
                else
                {

                }


            }

            Console.WriteLine("VALOR MINIMO: " + minvalue);

        }





    }
}
