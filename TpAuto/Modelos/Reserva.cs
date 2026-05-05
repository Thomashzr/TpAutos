using System.Text.Json.Serialization;

namespace TP_2_Autos.Modelos
{
    internal class Reserva
    {
        // ── Propiedades ─────────────────────────────────────────────────────────
        public int      Id          { get; set; }
        public DateTime FechaDesde  { get; set; }
        public DateTime FechaHasta  { get; set; }
        public double   CostoTotal  { get; set; }
        public string   Estado      { get; set; } = "Activa";   // "Activa" | "Cancelada" | "Finalizada"

        // Claves foráneas (se persisten en JSON)
        public int ClienteId  { get; set; }
        public int VehiculoId { get; set; }

        // Referencias de navegación en memoria — excluidas de la serialización
        // para evitar referencias circulares al serializar a JSON
        [JsonIgnore]
        public Cliente? ClienteDeLaReserva { get; set; }

        [JsonIgnore]
        public Vehiculo? VehiculoDeLaReserva { get; set; }

        // ── Propiedades calculadas ───────────────────────────────────────────────
        [JsonIgnore]
        public int CantidadDias => (FechaHasta - FechaDesde).Days;

        // ── Constructores ────────────────────────────────────────────────────────
        public Reserva() { }

        public Reserva(int id, DateTime fechaDesde, DateTime fechaHasta,
                       int clienteId, int vehiculoId, double precioPorDia)
        {
            Id          = id;
            FechaDesde  = fechaDesde;
            FechaHasta  = fechaHasta;
            ClienteId   = clienteId;
            VehiculoId  = vehiculoId;
            Estado      = "Activa";
            CostoTotal  = (fechaHasta - fechaDesde).Days * precioPorDia;
        }

        // ── Override ─────────────────────────────────────────────────────────────
        public override string ToString()
        {
            string cliente  = ClienteDeLaReserva  != null ? $"{ClienteDeLaReserva.Apellido}, {ClienteDeLaReserva.Nombre}" : $"ClienteId:{ClienteId}";
            string vehiculo = VehiculoDeLaReserva != null ? $"{VehiculoDeLaReserva.Marca} {VehiculoDeLaReserva.Modelo}" : $"VehiculoId:{VehiculoId}";
            return $"[{Id}] {cliente} | {vehiculo} | {FechaDesde:dd/MM/yyyy} → {FechaHasta:dd/MM/yyyy} ({CantidadDias} días) | ${CostoTotal:F2} | {Estado}";
        }
    }
}
