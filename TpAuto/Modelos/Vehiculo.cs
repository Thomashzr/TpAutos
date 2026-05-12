namespace TP_2_Autos.Modelos
{
    internal class Vehiculo
    {
        public int Id { get; set; }
        public string Patente { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Anio { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public double PrecioPorDia { get; set; }
        public bool Disponible { get; set; } = true;

        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Vehiculo() { }

        public Vehiculo(int id, string patente, string marca, string modelo,
                        int anio, string categoria, double precioPorDia)
        {
            Id = id;
            Patente = patente;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Categoria = categoria;
            PrecioPorDia = precioPorDia;
            Disponible = true;
            Reservas = new List<Reserva>();
        }

        public override string ToString()
        {
            return $"[{Id}] {Marca} {Modelo} ({Anio}) - Patente: {Patente} | Categoría: {Categoria} | ${PrecioPorDia}/día";
        }
    }
}