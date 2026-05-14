namespace TpAuto;

using System;
using System.IO;

class MenuSeleccion
{

    public static int MenuSeleccionar(string[] opciones, int seleccionado, string titulo, ConsoleColor colorCuadro = ConsoleColor.White, int maxAncho = 40)
    {

        // VALIDACIONES
        // 1: parametros null
        if (opciones == null)
        {
            Console.WriteLine("ERROR: La lista de opciones no puede ser nula.");
            return -1;
        }
        if (titulo == null)
        {
            Console.WriteLine("ERROR: El titulo no puede ser nulo.");
            return -1;
        }
        // 2: opciones esta vacio
        if (opciones.Length == 0)
        {
            Console.WriteLine("ERROR: La lista de opciones no puede estar vacia.");
            return -1;
        }
        // 3: titulo es vacio
        if (titulo == string.Empty)
        {
            Console.WriteLine("ERROR: El titulo no puede estar vacio.");
            return -1;
        }
        // 4: maxAncho debe tener espacio mínimo para mostrar algo (7 caracteres) además de "..."
        if (maxAncho < 10)
            maxAncho = 10;
        // 5: Elementos null dentro de opciones se normalizan
        //    si una opcion supera el ancho maximo, se corta y se agrega "..."
        for (int i = 0; i < opciones.Length; i++)
        {
            if (opciones[i] == null)
                opciones[i] = string.Empty;

            if (opciones[i].Length > maxAncho)
                opciones[i] = opciones[i].Substring(0, maxAncho - 3) + "...";
        }
        // 6: seleccionado fuera del rango valido se fija al borde mas cercano
        if (seleccionado < 1)
            seleccionado = 1;
        else if (seleccionado > opciones.Length)
            seleccionado = opciones.Length;

        // LOGICA DE IMPRESION

        // Calculo del ancho del menu
        int length = titulo.Length;
        for (int i = 0; i < opciones.Length; i++)
        {
            if (opciones[i].Length > length)
            {
                length = opciones[i].Length;
            }
        }

        Console.Clear();
        length += 5;

        // Borde superior
        Console.ForegroundColor = colorCuadro;
        for (int j = 0; j < length; j++)
        {
            if (j == 0) Console.Write("╔");
            else if (j == length - 1) Console.Write("╗\n");
            else Console.Write("═");
        }

        // Titulo
        Console.Write("║");
        Console.ForegroundColor = ConsoleColor.White;
        Console.CursorLeft = (length - titulo.Length) / 2;
        Console.Write(titulo);
        Console.CursorLeft = length - 1;
        Console.ForegroundColor = colorCuadro;
        Console.Write("║\n");

        // Separador entre titulo y opciones
        for (int k = 0; k < length; k++)
        {
            if (k == 0) Console.Write("╠");
            else if (k == length - 1) Console.Write("╣\n");
            else Console.Write("═");
        }

        // Opciones
        for (int l = 0; l < opciones.Length; l++)
        {
            Console.ForegroundColor = colorCuadro;
            Console.Write("║");
            if (l + 1 == seleccionado)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" " + opciones[l]);
            Console.CursorLeft = length - 1;
            Console.ForegroundColor = colorCuadro;
            Console.Write("║\n");
        }

        // Borde inferior
        for (int m = 0; m < length; m++)
        {
            if (m == 0) Console.Write("╚");
            else if (m == length - 1) Console.Write("╝\n");
            else Console.Write("═");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("Seleccione una opcion usando las flechas del teclado!!");

        // Control de entradas por teclas
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.UpArrow:
                seleccionado = ((seleccionado <= 1) ? MenuSeleccionar(opciones, seleccionado, titulo, colorCuadro, maxAncho) : MenuSeleccionar(opciones, seleccionado - 1, titulo, colorCuadro, maxAncho));
                break;
            case ConsoleKey.DownArrow:
                seleccionado = ((seleccionado >= opciones.Length) ? MenuSeleccionar(opciones, seleccionado, titulo, colorCuadro, maxAncho) : MenuSeleccionar(opciones, seleccionado + 1, titulo, colorCuadro, maxAncho));
                break;
            default:
                seleccionado = MenuSeleccionar(opciones, seleccionado, titulo, colorCuadro, maxAncho);
                break;
            case ConsoleKey.Enter:
                break;
        }

        return seleccionado;
    }


}
