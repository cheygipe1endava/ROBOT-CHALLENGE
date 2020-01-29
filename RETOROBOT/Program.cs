using System;
using System.Text;
using System.Collections;
using System.IO; //FIND THE TEXT FILE
using System.IO.Compression;
using System.Linq;


namespace ROBOT_APP
{
    public static class Program
    {
        

        static void Main(string[] args)
        {

            WeightMatrix();
            Console.ReadKey();

        }




        public static string ReadingText()
        {
            string filePath = Path.GetFullPath("prueba2.txt");


            StreamReader ReadFile = new StreamReader(filePath); //TEXT READER THAT READS CHARACTERS FROM A BYTE STREAM IN A PARTICULAR ENCODING
            String FileRead = ReadFile.ReadToEnd();//READS ALL CHARACTERS FROM THE CURRENT POSITION TO THE END OF THE STREAM
                       
            return FileRead;            
        }


        

        public static void WeightMatrix()
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


        }
    }
        

       
    }
