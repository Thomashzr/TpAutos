using TP_2_Autos.Modelos;

namespace TP_2_Autos.Controladores
{
    internal class nVehiculo
    {
        public static List<Vehiculo> vehiculos = new List<Vehiculo>();

        public static void Crear()
        {
            Console.Clear();

            int id = vehiculos.Count > 0 ? vehiculos.Max(v => v.Id) + 1 : 1;

            Console.Write("Patente        : ");
            string patente = Console.ReadLine().Trim().ToUpper();

            while (vehiculos.Any(v => v.Patente == patente))
            {
                Console.WriteLine($"\n Ya existe un vehículo con la patente '{patente}'. Ingrese una patente válida: ");
                patente = Console.ReadLine().Trim().ToUpper();
            }

            Console.Write("Marca          : ");
            string marca = Console.ReadLine().Trim();

            Console.Write("Modelo         : ");
            string modelo = Console.ReadLine().Trim();

            double precio;
            do
            {
                Console.Write("Precio por día : $");
            } while (!double.TryParse(Console.ReadLine(), out precio) || precio <= 0);

            int capacidad;
            do
            {
                Console.Write("Capacidad      : ");
            } while (!int.TryParse(Console.ReadLine(), out capacidad) || capacidad <= 0);

            Vehiculo v = new Vehiculo(id, patente, marca, modelo, precio, capacidad);
            vehiculos.Add(v);
            GuardarVehiculo(v);

            Console.WriteLine($"\n Vehículo registrado con ID {id}. Presione una tecla...");
            Console.ReadKey();
        }

        public static void GuardarVehiculo(Vehiculo v)
        {
            string linea = $"V|{v.Id}|{v.Patente}|{v.Marca}|{v.Modelo}|{v.PrecioPorDia}|{v.Capacidad}";
            File.AppendAllText("autos.txt", linea + Environment.NewLine);
        }

        public static void OrdenarVehiculos()
        {
            vehiculos = vehiculos.OrderBy(v => v.Marca).ThenBy(v => v.Modelo).ToList();
        }

        public static List<Vehiculo> Imprimir()
        {
            Console.Clear();
            OrdenarVehiculos();

            string[,] tabla = new string[vehiculos.Count + 1, 5];
            tabla[0, 0] = "ID";
            tabla[0, 1] = "Patente";
            tabla[0, 2] = "Marca";
            tabla[0, 3] = "Modelo";
            tabla[0, 4] = "$/día";

            for (int i = 0; i < vehiculos.Count; i++)
            {
                tabla[i + 1, 0] = vehiculos[i].Id.ToString();
                tabla[i + 1, 1] = vehiculos[i].Patente;
                tabla[i + 1, 2] = vehiculos[i].Marca;
                tabla[i + 1, 3] = vehiculos[i].Modelo;
                tabla[i + 1, 4] = $"${vehiculos[i].PrecioPorDia:F0}";
            }

            Tabla.DibujaTabla(tabla);
            return vehiculos;
        }

        public static Vehiculo? Seleccionar()
        {
            Imprimir();

            if (vehiculos.Count == 0)
            {
                Console.WriteLine("No hay vehículos registrados. Presione una tecla...");
                Console.ReadKey();
                return null;
            }

            Console.Write("\nIngrese el ID del vehículo: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Presione una tecla...");
                Console.ReadKey();
                return null;
            }

            Vehiculo? vehiculo = vehiculos.FirstOrDefault(v => v.Id == id);

            if (vehiculo == null)
            {
                Console.WriteLine($"No se encontró un vehículo con ID {id}. Presione una tecla...");
                Console.ReadKey();
            }

            return vehiculo;
        }

        public static void Eliminar()
        {
            Vehiculo? v = Seleccionar();
            if (v == null) return;

            if (v.Reservas.Any(r => r.Estado == "Activa"))
            {
                Console.WriteLine("\n El vehículo tiene reservas activas y no puede eliminarse. Presione una tecla...");
                Console.ReadKey();
                return;
            }

            Console.Write($"\n¿Confirma eliminar '{v.Marca} {v.Modelo} ({v.Patente})'? (S/N): ");
            if (Console.ReadLine()!.Trim().ToUpper() != "S") return;

            vehiculos.Remove(v);
            Console.WriteLine(" Vehículo eliminado. Presione una tecla...");
            Console.ReadKey();
        }

        public static void Modificar()
        {
            Vehiculo? v = Seleccionar();
            if (v == null) return;

            Console.Clear();
            Console.WriteLine($"Modificando: {v}\n");
            Console.WriteLine("(Deje en blanco para conservar el valor actual)\n");

            Console.Write($"Marca [{v.Marca}]: ");
            string marca = Console.ReadLine()!.Trim();
            if (!string.IsNullOrEmpty(marca)) v.Marca = marca;

            Console.Write($"Modelo [{v.Modelo}]: ");
            string modelo = Console.ReadLine()!.Trim();
            if (!string.IsNullOrEmpty(modelo)) v.Modelo = modelo;

            Console.Write($"Precio por día [{v.PrecioPorDia}]: $");
            string precioStr = Console.ReadLine()!.Trim();
            if (double.TryParse(precioStr, out double precio) && precio > 0) v.PrecioPorDia = precio;

            Console.Write($"Capacidad [{v.Capacidad}]: ");
            string capacidadStr = Console.ReadLine()!.Trim();
            if (int.TryParse(capacidadStr, out int capacidad) && capacidad > 0) v.Capacidad = capacidad;

            Console.WriteLine("\n Vehículo modificado. Presione una tecla...");
            Console.ReadKey();
        }

        public static List<Vehiculo> DisponiblesParaFecha(DateTime desde, DateTime hasta)
        {
            return vehiculos.Where(v =>
                !v.Reservas.Any(r =>
                    r.Estado == "Activa" &&
                    r.FechaDesde < hasta &&
                    r.FechaHasta > desde
                )
            ).ToList();
        }

        public static void MostrarDisponiblesParaFecha()
        {
            Console.Clear();

            DateTime desde, hasta;

            do
            {
                Console.Write("Fecha desde (dd/MM/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                       null, System.Globalization.DateTimeStyles.None, out desde));

            do
            {
                Console.Write("Fecha hasta (dd/MM/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                       null, System.Globalization.DateTimeStyles.None, out hasta) || hasta <= desde);

            List<Vehiculo> disponibles = DisponiblesParaFecha(desde, hasta);

            Console.Clear();
            Console.WriteLine($"Vehículos disponibles del {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}:\n");

            if (disponibles.Count == 0)
            {
                Console.WriteLine("No hay vehículos disponibles para ese período.");
                Console.WriteLine("\nPresione una tecla...");
                Console.ReadKey();
                return;
            }

            string[,] tabla = new string[disponibles.Count + 1, 5];
            tabla[0, 0] = "ID";
            tabla[0, 1] = "Patente";
            tabla[0, 2] = "Marca";
            tabla[0, 3] = "Modelo";
            tabla[0, 4] = "$/día";

            for (int i = 0; i < disponibles.Count; i++)
            {
                tabla[i + 1, 0] = disponibles[i].Id.ToString();
                tabla[i + 1, 1] = disponibles[i].Patente;
                tabla[i + 1, 2] = disponibles[i].Marca;
                tabla[i + 1, 3] = disponibles[i].Modelo;
                tabla[i + 1, 4] = $"${disponibles[i].PrecioPorDia:F0}";
            }

            Tabla.DibujaTabla(tabla);
            Console.WriteLine("Presione una tecla...");
            Console.ReadKey();
        }

        public static void ReporteVehiculosMasUsados()
        {
            Console.Clear();

            var ranking = vehiculos
                .OrderByDescending(v => v.Reservas.Count)
                .ToList();

            string[,] tabla = new string[ranking.Count + 1, 4];
            tabla[0, 0] = "Pos.";
            tabla[0, 1] = "Marca y Modelo";
            tabla[0, 2] = "Patente";
            tabla[0, 3] = "Reservas";

            for (int i = 0; i < ranking.Count; i++)
            {
                tabla[i + 1, 0] = $"{i + 1}°";
                tabla[i + 1, 1] = $"{ranking[i].Marca} {ranking[i].Modelo}";
                tabla[i + 1, 2] = ranking[i].Patente;
                tabla[i + 1, 3] = ranking[i].Reservas.Count.ToString();
            }

            Tabla.DibujaTabla(tabla);
            ExportarSiCorresponde(
                ranking.Select((v, i) => $"{i + 1}. {v.Marca} {v.Modelo} ({v.Patente}) — {v.Reservas.Count} reserva(s)").ToList(),
                "reporte_vehiculos_mas_usados.txt");
        }

        private static void ExportarSiCorresponde(List<string> lineas, string nombreArchivo)
        {
            Console.Write("¿Exportar a archivo? (S/N): ");
            if (Console.ReadLine()!.Trim().ToUpper() == "S")
            {
                File.WriteAllLines(nombreArchivo, lineas);
                Console.WriteLine($"✔ Exportado a '{nombreArchivo}'.");
            }

            Console.WriteLine("Presione una tecla...");
            Console.ReadKey();
        }

        public static void Menu()
        {
            string[] opciones = {
                "Registrar vehículo",
                "Listar vehículos",
                "Modificar vehículo",
                "Eliminar vehículo",
                "Vehículos disponibles por fecha",
                "Reporte vehículos más usados",
                "Volver"
            };

            int sel = MenuSeleccion.MenuSeleccionar(opciones, 1, "GESTIÓN DE VEHÍCULOS", ConsoleColor.DarkCyan);

            switch (sel)
            {
                case 1: Crear(); Menu(); break;
                case 2: Imprimir(); Console.ReadKey(); Menu(); break;
                case 3: Modificar(); Menu(); break;
                case 4: Eliminar(); Menu(); break;
                case 5: MostrarDisponiblesParaFecha(); Menu(); break;
                case 6: ReporteVehiculosMasUsados(); Menu(); break;
                case 7: break;
            }
        }
    }
}