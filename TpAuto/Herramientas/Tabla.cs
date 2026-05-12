namespace TP_2_Autos;

using System;
using System.Collections.Generic;
using System.Text;

    class Tabla
    {
        public static void DibujaTabla(string[,] tabla)
        {
            int[] anchos = new int[tabla.GetLength(1)];
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    string celda = tabla[i, j] ?? "";
                    if (celda.Length + 2 > anchos[j])
                        anchos[j] = celda.Length + 2;
                }
            }

            // Borde superior
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int k = 0; k < tabla.GetLength(1); k++)
            {
                if (k == 0) Console.Write("╔");
                for (int l = 0; l < anchos[k]; l++) Console.Write("═");
                if (k < tabla.GetLength(1) - 1) Console.Write("╦");
                if (k == tabla.GetLength(1) - 1) Console.Write("╗");
            }
            Console.WriteLine();

            // Filas
            for (int m = 0; m < tabla.GetLength(0); m++)
            {
                for (int n = 0; n < tabla.GetLength(1); n++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("║");

                    if (m == 0)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    string celda = " " + (tabla[m, n] ?? "") + " ";
                    Console.Write(celda.PadRight(anchos[n]));
                }

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("║");
                Console.WriteLine();

                if (m < tabla.GetLength(0) - 1)
                {
                    for (int num2 = 0; num2 < tabla.GetLength(1); num2++)
                    {
                        if (num2 == 0) Console.Write("╠");
                        for (int num3 = 0; num3 < anchos[num2]; num3++) Console.Write("═");
                        if (num2 < tabla.GetLength(1) - 1) Console.Write("╬");
                        if (num2 == tabla.GetLength(1) - 1) Console.Write("╣");
                    }
                    Console.WriteLine();
                }
            }

            // Borde inferior
            for (int num2 = 0; num2 < tabla.GetLength(1); num2++)
            {
                if (num2 == 0) Console.Write("╚");
                for (int num3 = 0; num3 < anchos[num2]; num3++) Console.Write("═");
                if (num2 < tabla.GetLength(1) - 1) Console.Write("╩");
                if (num2 == tabla.GetLength(1) - 1) Console.Write("╝");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
        }
    }


