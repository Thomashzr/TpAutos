using TpAuto.Modelos;

namespace TpAuto.Controladores
{
    internal class nReserva
    {
        public static List<Reserva> reservas = new List<Reserva>();
        
        public static void Crear()
        {
            Console.Clear();
            Console.WriteLine("── Paso 1: Seleccionar cliente ──");
            Cliente? cliente = nCliente.Seleccionar();
            if (cliente == null) return;

            Console.WriteLine("\n── Paso 2: Ingresar fechas ──");
            DateTime desde, hasta;

            do
            {
                Console.Write("Fecha desde (dd/MM/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                       null, System.Globalization.DateTimeStyles.None, out desde)
                     || desde.Date < DateTime.Today);

            do
            {
                Console.Write("Fecha hasta (dd/MM/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                       null, System.Globalization.DateTimeStyles.None, out hasta)
                     || hasta <= desde);

            Console.WriteLine("\n── Paso 3: Seleccionar vehículo disponible ──");
            List<Vehiculo> disponibles = nVehiculo.DisponiblesParaFecha(desde, hasta);

            if (disponibles.Count == 0)
            {
                Console.WriteLine("\n⚠ No hay vehículos disponibles para ese período. Presione una tecla...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nVehículos disponibles del {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}:\n");

            string[,] tablaDisp = new string[disponibles.Count + 1, 5];
            tablaDisp[0, 0] = "ID";
            tablaDisp[0, 1] = "Marca";
            tablaDisp[0, 2] = "Modelo";
            tablaDisp[0, 3] = "Patente";
            tablaDisp[0, 4] = "$/día";

            for (int i = 0; i < disponibles.Count; i++)
            {
                tablaDisp[i + 1, 0] = disponibles[i].Id.ToString();
                tablaDisp[i + 1, 1] = disponibles[i].Marca;
                tablaDisp[i + 1, 2] = disponibles[i].Modelo;
                tablaDisp[i + 1, 3] = disponibles[i].Patente;
                tablaDisp[i + 1, 4] = $"${disponibles[i].PrecioPorDia:F0}";
            }

            Tabla.DibujaTabla(tablaDisp);

            Console.Write("Ingrese el ID del vehículo: ");
            if (!int.TryParse(Console.ReadLine(), out int vidElegido))
            {
                Console.WriteLine("ID inválido. Presione una tecla...");
                Console.ReadKey();
                return;
            }

            Vehiculo? vehiculo = disponibles.FirstOrDefault(v => v.Id == vidElegido);

            if (vehiculo == null)
            {
                Console.WriteLine("El vehículo seleccionado no está disponible para esas fechas. Presione una tecla...");
                Console.ReadKey();
                return;
            }

            int id = reservas.Count > 0 ? reservas.Max(r => r.Id) + 1 : 1;

            Reserva nueva = new Reserva(id, desde, hasta, cliente.Id, vehiculo.Id, vehiculo.PrecioPorDia);
            nueva.ClienteDeLaReserva = cliente;
            nueva.VehiculoDeLaReserva = vehiculo;

            reservas.Add(nueva);
            cliente.Reservas.Add(nueva);
            vehiculo.Reservas.Add(nueva);

            GuardarReserva(nueva);

            Console.WriteLine($"\n✔ Reserva #{id} creada. Total: ${nueva.CostoTotal:F2} ({nueva.CantidadDias} días). Presione una tecla...");
            Console.ReadKey();
        }

        public static void GuardarReserva(Reserva r)
        {
            string linea = $"R|{r.Id}|{r.FechaDesde:dd/MM/yyyy}|{r.FechaHasta:dd/MM/yyyy}|{r.ClienteId}|{r.VehiculoId}|{r.CostoTotal:F2}|{r.Estado}";
            File.AppendAllText("autos.txt", linea + Environment.NewLine);
        }

        public static void OrdenarReservas()
        {
            reservas = reservas.OrderByDescending(r => r.FechaDesde).ToList();
        }

        public static List<Reserva> Imprimir()
        {
            Console.Clear();
            OrdenarReservas();

            string[,] tabla = new string[reservas.Count + 1, 7];
            tabla[0, 0] = "ID";
            tabla[0, 1] = "Cliente";
            tabla[0, 2] = "Vehículo";
            tabla[0, 3] = "Desde";
            tabla[0, 4] = "Hasta";
            tabla[0, 5] = "Días";
            tabla[0, 6] = "Total";

            for (int i = 0; i < reservas.Count; i++)
            {
                string clienteNombre = reservas[i].ClienteDeLaReserva != null
                    ? $"{reservas[i].ClienteDeLaReserva!.Apellido}, {reservas[i].ClienteDeLaReserva!.Nombre}"
                    : $"ID:{reservas[i].ClienteId}";
                string vehiculoNombre = reservas[i].VehiculoDeLaReserva != null
                    ? $"{reservas[i].VehiculoDeLaReserva!.Marca} {reservas[i].VehiculoDeLaReserva!.Modelo}"
                    : $"ID:{reservas[i].VehiculoId}";

                tabla[i + 1, 0] = reservas[i].Id.ToString();
                tabla[i + 1, 1] = clienteNombre;
                tabla[i + 1, 2] = vehiculoNombre;
                tabla[i + 1, 3] = reservas[i].FechaDesde.ToString("dd/MM/yyyy");
                tabla[i + 1, 4] = reservas[i].FechaHasta.ToString("dd/MM/yyyy");
                tabla[i + 1, 5] = reservas[i].CantidadDias.ToString();
                tabla[i + 1, 6] = $"${reservas[i].CostoTotal:F2}";
            }

            Tabla.DibujaTabla(tabla);
            return reservas;
        }

        public static Reserva? Seleccionar()
        {
            Imprimir();

            if (reservas.Count == 0)
            {
                Console.WriteLine("No hay reservas registradas. Presione una tecla...");
                Console.ReadKey();
                return null;
            }

            Console.Write("\nIngrese el ID de la reserva: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Presione una tecla...");
                Console.ReadKey();
                return null;
            }

            Reserva? reserva = reservas.FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                Console.WriteLine($"No se encontró una reserva con ID {id}. Presione una tecla...");
                Console.ReadKey();
            }

            return reserva;
        }

        public static void Cancelar()
        {
            Reserva? r = Seleccionar();
            if (r == null) return;

            if (r.Estado != "Activa")
            {
                Console.WriteLine($"\n⚠ La reserva ya está '{r.Estado}' y no puede cancelarse. Presione una tecla...");
                Console.ReadKey();
                return;
            }

            Console.Write($"\n¿Confirma cancelar la reserva #{r.Id}? (S/N): ");
            if (Console.ReadLine()!.Trim().ToUpper() != "S") return;

            r.Estado = "Cancelada";
            Console.WriteLine("✔ Reserva cancelada. Presione una tecla...");
            Console.ReadKey();
        }

        public static void Modificar()
        {
            Reserva? r = Seleccionar();
            if (r == null) return;

            if (r.Estado != "Activa")
            {
                Console.WriteLine($"\n⚠ Solo se pueden modificar reservas activas. Estado actual: '{r.Estado}'. Presione una tecla...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine($"Modificando reserva #{r.Id}");
            Console.WriteLine($"Fechas actuales: {r.FechaDesde:dd/MM/yyyy} → {r.FechaHasta:dd/MM/yyyy}\n");

            DateTime desde, hasta;

            do
            {
                Console.Write("Nueva fecha desde (dd/MM/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                       null, System.Globalization.DateTimeStyles.None, out desde));

            do
            {
                Console.Write("Nueva fecha hasta (dd/MM/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                       null, System.Globalization.DateTimeStyles.None, out hasta)
                     || hasta <= desde);

            bool hayConflicto = r.VehiculoDeLaReserva!.Reservas.Any(otra =>
                otra.Id != r.Id &&
                otra.Estado == "Activa" &&
                otra.FechaDesde < hasta &&
                otra.FechaHasta > desde
            );

            if (hayConflicto)
            {
                Console.WriteLine("\n⚠ El vehículo ya tiene una reserva en ese período. Presione una tecla...");
                Console.ReadKey();
                return;
            }

            r.FechaDesde = desde;
            r.FechaHasta = hasta;
            r.CostoTotal = r.CantidadDias * r.VehiculoDeLaReserva!.PrecioPorDia;

            Console.WriteLine($"\n✔ Reserva actualizada. Nuevo total: ${r.CostoTotal:F2}. Presione una tecla...");
            Console.ReadKey();
        }

        public static void Eliminar()
        {
            Reserva? r = Seleccionar();
            if (r == null) return;

            Console.Write($"\n¿Confirma eliminar la reserva #{r.Id}? (S/N): ");
            if (Console.ReadLine()!.Trim().ToUpper() != "S") return;

            r.ClienteDeLaReserva?.Reservas.Remove(r);
            r.VehiculoDeLaReserva?.Reservas.Remove(r);
            reservas.Remove(r);

            Console.WriteLine("✔ Reserva eliminada. Presione una tecla...");
            Console.ReadKey();
        }

        public static void ReporteReservas()
        {
            Console.Clear();
            OrdenarReservas();

            string[,] tabla = new string[reservas.Count + 1, 8];
            tabla[0, 0] = "ID";
            tabla[0, 1] = "Cliente";
            tabla[0, 2] = "Vehículo";
            tabla[0, 3] = "Desde";
            tabla[0, 4] = "Hasta";
            tabla[0, 5] = "Días";
            tabla[0, 6] = "Total";
            tabla[0, 7] = "Estado";

            for (int i = 0; i < reservas.Count; i++)
            {
                string clienteNombre = reservas[i].ClienteDeLaReserva != null
                    ? $"{reservas[i].ClienteDeLaReserva!.Apellido}, {reservas[i].ClienteDeLaReserva!.Nombre}"
                    : $"ID:{reservas[i].ClienteId}";
                string vehiculoNombre = reservas[i].VehiculoDeLaReserva != null
                    ? $"{reservas[i].VehiculoDeLaReserva!.Marca} {reservas[i].VehiculoDeLaReserva!.Modelo}"
                    : $"ID:{reservas[i].VehiculoId}";

                tabla[i + 1, 0] = reservas[i].Id.ToString();
                tabla[i + 1, 1] = clienteNombre;
                tabla[i + 1, 2] = vehiculoNombre;
                tabla[i + 1, 3] = reservas[i].FechaDesde.ToString("dd/MM/yyyy");
                tabla[i + 1, 4] = reservas[i].FechaHasta.ToString("dd/MM/yyyy");
                tabla[i + 1, 5] = reservas[i].CantidadDias.ToString();
                tabla[i + 1, 6] = $"${reservas[i].CostoTotal:F2}";
                tabla[i + 1, 7] = reservas[i].Estado;
            }

            Tabla.DibujaTabla(tabla);
            ExportarSiCorresponde(
                reservas.Select(r => r.ToString()).ToList(),
                "reporte_reservas.txt");
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
                "Nueva reserva",
                "Listar reservas",
                "Modificar reserva",
                "Cancelar reserva",
                "Eliminar reserva",
                "Reporte de reservas",
                "Volver"
            };

            int sel = MenuSeleccion.MenuSeleccionar(opciones, 1, "GESTIÓN DE RESERVAS", ConsoleColor.DarkCyan);

            switch (sel)
            {
                case 1: Crear(); Menu(); break;
                case 2: Imprimir(); Console.ReadKey(); Menu(); break;
                case 3: Modificar(); Menu(); break;
                case 4: Cancelar(); Menu(); break;
                case 5: Eliminar(); Menu(); break;
                case 6: ReporteReservas(); Menu(); break;
                case 7: break;
            }
        }
    }
}