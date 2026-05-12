using TP_2_Autos.Controladores;
using TP_2_Autos.Modelos;

namespace TP_2_Autos
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CargarDatos();
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
        public static void CargarDatos()
        {
            // ════════════════════════════════════════════════════════════════════════
            // VEHÍCULOS
            // ════════════════════════════════════════════════════════════════════════
            nVehiculo.vehiculos.Add(new Vehiculo(1, "AB123CD", "Toyota", "Corolla", 2021, "Sedan", 8500));
            nVehiculo.vehiculos.Add(new Vehiculo(2, "GH456IJ", "Ford", "EcoSport", 2020, "SUV", 9200));
            nVehiculo.vehiculos.Add(new Vehiculo(3, "MN789OP", "Chevrolet", "S10", 2019, "Pickup", 11000));
            nVehiculo.vehiculos.Add(new Vehiculo(4, "QR012ST", "Volkswagen", "Polo", 2022, "Hatchback", 7800));
            nVehiculo.vehiculos.Add(new Vehiculo(5, "UV345WX", "Renault", "Kangoo", 2018, "Furgón", 8000));

            // ════════════════════════════════════════════════════════════════════════
            // CLIENTES
            // ════════════════════════════════════════════════════════════════════════
            nCliente.clientes.Add(new Cliente(1, "32456789", "Martín", "González", "11-4523-6789", "mgonzalez@gmail.com"));
            nCliente.clientes.Add(new Cliente(2, "28934512", "Laura", "Fernández", "11-5634-2341", "lfernandez@hotmail.com"));
            nCliente.clientes.Add(new Cliente(3, "35678901", "Diego", "Ramírez", "11-4712-8823", "dramirez@gmail.com"));
            nCliente.clientes.Add(new Cliente(4, "30123456", "Valentina", "López", "11-6823-1190", "vlopez@yahoo.com"));
            nCliente.clientes.Add(new Cliente(5, "27890123", "Sebastián", "Torres", "11-5901-4457", "storres@gmail.com"));
            nCliente.clientes.Add(new Cliente(6, "33567890", "Camila", "Morales", "11-4234-7762", "cmorales@hotmail.com"));
            nCliente.clientes.Add(new Cliente(7, "29345678", "Federico", "Sánchez", "11-6345-9981", "fsanchez@gmail.com"));
            nCliente.clientes.Add(new Cliente(8, "36789012", "Lucía", "Herrera", "11-4456-3314", "lherrera@gmail.com"));
            nCliente.clientes.Add(new Cliente(9, "31234567", "Nicolás", "Medina", "11-5567-6628", "nmedina@yahoo.com"));
            nCliente.clientes.Add(new Cliente(10, "26012345", "Florencia", "Castillo", "11-4678-9943", "fcastillo@hotmail.com"));

            // ════════════════════════════════════════════════════════════════════════
            // FUNCIÓN AUXILIAR para crear reservas y enlazar referencias
            // ════════════════════════════════════════════════════════════════════════
            void AgregarReserva(int id, DateTime desde, DateTime hasta, int clienteId, int vehiculoId)
            {
                Vehiculo v = nVehiculo.vehiculos.First(x => x.Id == vehiculoId);
                Cliente c = nCliente.clientes.First(x => x.Id == clienteId);

                Reserva r = new Reserva(id, desde, hasta, clienteId, vehiculoId, v.PrecioPorDia);
                r.ClienteDeLaReserva = c;
                r.VehiculoDeLaReserva = v;

                nReserva.reservas.Add(r);
                c.Reservas.Add(r);
                v.Reservas.Add(r);
            }

            // ════════════════════════════════════════════════════════════════════════
            // RESERVAS — pasadas, activas y futuras para datos variados
            // ════════════════════════════════════════════════════════════════════════

            // Pasadas (Estado queda "Activa" pero fechas vencidas — realista para historial)
            AgregarReserva(1, new DateTime(2026, 1, 5), new DateTime(2026, 1, 10), 1, 1);  // González → Corolla
            AgregarReserva(2, new DateTime(2026, 1, 12), new DateTime(2026, 1, 15), 3, 2);  // Ramírez  → EcoSport
            AgregarReserva(3, new DateTime(2026, 2, 3), new DateTime(2026, 2, 8), 5, 3);  // Torres   → S10
            AgregarReserva(4, new DateTime(2026, 2, 14), new DateTime(2026, 2, 17), 2, 4);  // Fernández→ Polo
            AgregarReserva(5, new DateTime(2026, 2, 20), new DateTime(2026, 2, 25), 7, 5);  // Sánchez  → Kangoo
            AgregarReserva(6, new DateTime(2026, 3, 1), new DateTime(2026, 3, 6), 4, 1);  // López    → Corolla
            AgregarReserva(7, new DateTime(2026, 3, 10), new DateTime(2026, 3, 14), 6, 2);  // Morales  → EcoSport
            AgregarReserva(8, new DateTime(2026, 3, 18), new DateTime(2026, 3, 22), 9, 3);  // Medina   → S10
            AgregarReserva(9, new DateTime(2026, 4, 2), new DateTime(2026, 4, 7), 1, 4);  // González → Polo      (repite cliente)
            AgregarReserva(10, new DateTime(2026, 4, 10), new DateTime(2026, 4, 13), 8, 5);  // Herrera  → Kangoo
            AgregarReserva(11, new DateTime(2026, 4, 20), new DateTime(2026, 4, 25), 10, 2);  // Castillo → EcoSport
            AgregarReserva(12, new DateTime(2026, 4, 28), new DateTime(2026, 5, 2), 3, 1);  // Ramírez  → Corolla   (repite cliente)

            // Activas / próximas
            AgregarReserva(13, new DateTime(2026, 5, 15), new DateTime(2026, 5, 20), 2, 3);  // Fernández→ S10
            AgregarReserva(14, new DateTime(2026, 5, 22), new DateTime(2026, 5, 26), 5, 4);  // Torres   → Polo      (repite cliente)
            AgregarReserva(15, new DateTime(2026, 6, 1), new DateTime(2026, 6, 7), 6, 1);  // Morales  → Corolla   (repite vehículo más usado)
        }
    }

}