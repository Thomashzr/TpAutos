using TpAuto.Controladores;
using TpAuto.Modelos;
using TpAuto.Persistencia;

namespace TpAuto
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Conexion.OpenConexion();

            nVehiculo.vehiculos = pVehiculo.getAll();
            nCliente.clientes = pCliente.getAll();
            nReserva.reservas = pReserva.getAll();

            foreach (var r in nReserva.reservas)
            {
               
                r.ClienteDeLaReserva = nCliente.clientes.FirstOrDefault(c => c.Id == r.ClienteId);
                r.VehiculoDeLaReserva = nVehiculo.vehiculos.FirstOrDefault(v => v.Id == r.VehiculoId);

                
                if (r.ClienteDeLaReserva != null)
                    r.ClienteDeLaReserva.Reservas.Add(r);

                if (r.VehiculoDeLaReserva != null)
                    r.VehiculoDeLaReserva.Reservas.Add(r);
            }

           
            funcionMenu();

            
            Conexion.CloseConexion();
        

        
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



