using System.Text.Json.Serialization;

namespace TP_2_Autos.Modelos
{
    internal class Vehiculo
    {
        // ── Propiedades ─────────────────────────────────────────────────────────
        public int    Id              { get; set; }
        public string Patente         { get; set; } = string.Empty;
        public string Marca           { get; set; } = string.Empty;
        public string Modelo          { get; set; } = string.Empty;
        public int    Anio            { get; set; }
        public string Categoria       { get; set; } = string.Empty;   // Ej: "Sedan", "SUV", "Pickup"
        public double PrecioPorDia    { get; set; }
        public bool   Disponible      { get; set; } = true;

        // Lista de reservas asociadas a este vehículo
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        // ── Constructores ────────────────────────────────────────────────────────
        public Vehiculo() { }

        public Vehiculo(int id, string patente, string marca, string modelo,
                        int anio, string categoria, double precioPorDia)
        {
            Id           = id;
            Patente      = patente;
            Marca        = marca;
            Modelo       = modelo;
            Anio         = anio;
            Categoria    = categoria;
            PrecioPorDia = precioPorDia;
            Disponible   = true;
            Reservas     = new List<Reserva>();
        }

        // ── Override ─────────────────────────────────────────────────────────────
        public override string ToString()
        {
            return $"[{Id}] {Marca} {Modelo} ({Anio}) - Patente: {Patente} | Categoría: {Categoria} | ${PrecioPorDia}/día";
        }
    }
}
