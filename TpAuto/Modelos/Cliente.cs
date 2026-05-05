using System.Text.Json.Serialization;

namespace TP_2_Autos.Modelos
{
    internal class Cliente
    {
        // ── Propiedades ─────────────────────────────────────────────────────────
        public int    Id       { get; set; }
        public string Dni      { get; set; } = string.Empty;
        public string Nombre   { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email    { get; set; } = string.Empty;

        // Lista de reservas realizadas por este cliente
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        // ── Constructores ────────────────────────────────────────────────────────
        public Cliente() { }

        public Cliente(int id, string dni, string nombre, string apellido,
                       string telefono, string email)
        {
            Id       = id;
            Dni      = dni;
            Nombre   = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Email    = email;
            Reservas = new List<Reserva>();
        }

        // ── Override ─────────────────────────────────────────────────────────────
        public override string ToString()
        {
            return $"[{Id}] {Apellido}, {Nombre} | DNI: {Dni} | Tel: {Telefono} | Email: {Email}";
        }
    }
}
