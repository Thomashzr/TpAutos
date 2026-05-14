using TpAuto.Controladores;
using TpAuto.Modelos;

namespace TpAuto
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            funcionMenu();
        }

        public static void funcionMenu()
        {
            string[] opciones = {
                "Gestión de Vehículos",
                "Gestión de Clientes",
                "Gestión de Reservas",
                "Salir"
            };

            int seleccion = MenuSeleccion.MenuSeleccionar(opciones, 1, "SISTEMA DE ALQUILER DE AUTOS", ConsoleColor.DarkCyan);

            switch (seleccion)
            {
                case 1: nVehiculo.Menu(); funcionMenu(); break;
                case 2: nCliente.Menu(); funcionMenu(); break;
                case 3: nReserva.Menu(); funcionMenu(); break;
                case 4: Console.WriteLine("¡Hasta luego!"); break;
            }
        }
    }
}



