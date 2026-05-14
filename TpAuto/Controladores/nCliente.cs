using dbPersonas.Controladores;
using Microsoft.Data.Sqlite;
using TP_2_Autos.Modelos;


namespace TP_2_Autos.Controladores
{
    internal class nCliente
    {
        public static List<Cliente> clientes = new List<Cliente>();

        public static void Crear()
        {
            Console.Clear();

            bool ValidarSoloLetras(string input) => !string.IsNullOrWhiteSpace(input) && input.All(c => !char.IsDigit(c));
            bool ValidarSoloNumeros(string input) => !string.IsNullOrWhiteSpace(input) && input.All(c => !char.IsLetter(c));

         
            int id = clientes.Count > 0 ? clientes.Max(c => c.Id) + 1 : 1;

            Console.Write("DNI:");
            string dni = Console.ReadLine()!.Trim();

            if (!ValidarSoloNumeros(dni))
            {
                Console.WriteLine("DNI inválido (solo números). Presione una tecla.");
                Console.ReadKey();
                return;
            }

            if (clientes.Any(c => c.Dni == dni))
            {
                Console.WriteLine($"Ya existe un cliente con DNI '{dni}'. Presione una tecla.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!.Trim();
            if (!ValidarSoloLetras(nombre))
            {
                Console.WriteLine("Nombre inválido (solo letras). Presione una tecla.");
                Console.ReadKey();
                return;
            }


            Console.Write("Apellido: ");
            string apellido = Console.ReadLine()!.Trim();
            if (!ValidarSoloLetras(apellido))
            {
                Console.WriteLine("Apellido inválido (solo letras). Presione una tecla.");
                Console.ReadKey();
                return;
            }

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine()!.Trim();
            if (!ValidarSoloNumeros(telefono))
            {
                Console.WriteLine("Teléfono inválido (solo números). Presione una tecla.");
                Console.ReadKey();
                return;
            }

            Cliente c = new Cliente(id, dni, nombre, apellido, telefono);
            clientes.Add(c);
            GuardarCliente(c);

            Console.WriteLine($"Cliente registrado con ID {id}. Presione una tecla...");
            Console.ReadKey();
        }

        public static Cliente GuardarCliente(Cliente c)
        {
            SqliteCommand sqliteCommand = new SqliteCommand("INSERT INTO Cliente (Id, DNI, Nombre, Apellido, Telefono) VALUES (@Id, @DNI, @Nombre, @Apellido, @Telefono)");
            sqliteCommand.Parameters.Add(new SqliteParameter("@Id", c.Id));
            sqliteCommand.Parameters.Add(new SqliteParameter("@DNI", c.Dni));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Nombre", c.Nombre));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Apellido", c.Apellido));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Telefono", c.Telefono));
            sqliteCommand.Connection = Conexion.MiConexion;
            sqliteCommand.ExecuteNonQuery();
            return c;
        }

        public static void OrdenarClientes()
        {
            clientes = clientes.OrderBy(c => c.Apellido).ThenBy(c => c.Nombre).ToList();
        }

        public static List<Cliente> Imprimir()
        {
            Console.Clear();
            OrdenarClientes();

            string[,] tabla = new string[clientes.Count + 1, 5];
            tabla[0, 0] = "ID";
            tabla[0, 1] = "DNI";
            tabla[0, 2] = "Apellido";
            tabla[0, 3] = "Nombre";
            tabla[0, 4] = "Teléfono";

            for (int i = 0; i < clientes.Count; i++)
            {
                tabla[i + 1, 0] = clientes[i].Id.ToString();
                tabla[i + 1, 1] = clientes[i].Dni;
                tabla[i + 1, 2] = clientes[i].Apellido;
                tabla[i + 1, 3] = clientes[i].Nombre;
                tabla[i + 1, 4] = clientes[i].Telefono;
            }

            Tabla.DibujaTabla(tabla);
            return clientes;
        }

        public static Cliente? Seleccionar()
        {
            Imprimir();

            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados. Presione una tecla...");
                Console.ReadKey();
                return null;
            }

            Console.Write("\nIngrese el ID del cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Presione una tecla...");
                Console.ReadKey();
                return null;
            }

            Cliente? cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                Console.WriteLine($"No se encontró un cliente con ID {id}. Presione una tecla...");
                Console.ReadKey();
            }

            return cliente;
        }

        public static void Eliminar()
        {
            Cliente? c = Seleccionar();
            if (c == null) return;

            if (c.Reservas.Any(r => r.Estado == "Activa"))
            {
                Console.WriteLine("El cliente tiene reservas activas y no puede eliminarse. Presione una tecla...");
                Console.ReadKey();
                return;
            }

            Console.Write($"\n¿Confirma eliminar a '{c.Apellido}, {c.Nombre}'? (S/N): ");
            if (Console.ReadLine()!.Trim().ToUpper() != "S") return;

            clientes.Remove(c);
            Console.WriteLine("Cliente eliminado. Presione una tecla...");
            Console.ReadKey();
        }

        public static void Modificar()
        {
            Cliente? c = Seleccionar();
            if (c == null) return;

            Console.Clear();
            Console.WriteLine($"Modificando: {c}\n");
            Console.WriteLine("(Deje en blanco para conservar el valor actual)\n");

            Console.Write($"Nombre [{c.Nombre}]: ");
            string nombre = Console.ReadLine()!.Trim();
            if (!string.IsNullOrEmpty(nombre)) c.Nombre = nombre;

            Console.Write($"Apellido [{c.Apellido}]: ");
            string apellido = Console.ReadLine()!.Trim();
            if (!string.IsNullOrEmpty(apellido)) c.Apellido = apellido;

            Console.Write($"Teléfono [{c.Telefono}]: ");
            string telefono = Console.ReadLine()!.Trim();
            if (!string.IsNullOrEmpty(telefono)) c.Telefono = telefono;

            Console.WriteLine("Cliente modificado. Presione una tecla...");
            Console.ReadKey();
        }

        public static void ReporteClientesMasAlquilan()
        {
            Console.Clear();

            var ranking = clientes
                .OrderByDescending(c => c.Reservas.Count)
                .ToList();

            string[,] tabla = new string[ranking.Count + 1, 4];
            tabla[0, 0] = "Pos.";
            tabla[0, 1] = "Apellido y Nombre";
            tabla[0, 2] = "DNI";
            tabla[0, 3] = "Reservas";

            for (int i = 0; i < ranking.Count; i++)
            {
                tabla[i + 1, 0] = $"{i + 1}°";
                tabla[i + 1, 1] = $"{ranking[i].Apellido}, {ranking[i].Nombre}";
                tabla[i + 1, 2] = ranking[i].Dni;
                tabla[i + 1, 3] = ranking[i].Reservas.Count.ToString();
            }

            Tabla.DibujaTabla(tabla);
            ExportarSiCorresponde(
                ranking.Select((c, i) => $"{i + 1}. {c.Apellido}, {c.Nombre} (DNI: {c.Dni}) — {c.Reservas.Count} reserva(s)").ToList(),
                "reporte_clientes_mas_alquilan.txt");
        }

        private static void ExportarSiCorresponde(List<string> lineas, string nombreArchivo)
        {
            Console.Write("¿Exportar a archivo? (S/N): ");
            if (Console.ReadLine()!.Trim().ToUpper() == "S")
            {
                File.WriteAllLines(nombreArchivo, lineas);
                Console.WriteLine($"Exportado a '{nombreArchivo}'.");
            }

            Console.WriteLine("Presione una tecla...");
            Console.ReadKey();
        }

        public static void Menu()
        {
            string[] opciones = {
                "Registrar cliente",
                "Listar clientes",
                "Modificar cliente",
                "Eliminar cliente",
                "Reporte clientes que más alquilan",
                "Volver"
            };

            int sel = MenuSeleccion.MenuSeleccionar(opciones, 1, "GESTIÓN DE CLIENTES", ConsoleColor.DarkCyan);

            switch (sel)
            {
                case 1: Crear(); Menu(); break;
                case 2: Imprimir(); Console.ReadKey(); Menu(); break;
                case 3: Modificar(); Menu(); break;
                case 4: Eliminar(); Menu(); break;
                case 5: ReporteClientesMasAlquilan(); Menu(); break;
                case 6: break;
            }
        }


    }
}