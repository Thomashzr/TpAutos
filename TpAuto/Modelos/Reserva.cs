namespace TpAuto.Modelos
{
    internal class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public double CostoTotal { get; set; }
        public string Estado { get; set; } = "Activa";

        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }

        public Cliente? ClienteDeLaReserva { get; set; }
        public Vehiculo? VehiculoDeLaReserva { get; set; }

        public int CantidadDias => (FechaHasta - FechaDesde).Days;

        public Reserva() { }

        public Reserva(int id, DateTime fechaDesde, DateTime fechaHasta,
                       int clienteId, int vehiculoId, double precioPorDia)
        {
            Id = id;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            ClienteId = clienteId;
            VehiculoId = vehiculoId;
            Estado = "Activa";
            CostoTotal = (fechaHasta - fechaDesde).Days * precioPorDia;
        }

        public override string ToString()
        {
            string cliente = ClienteDeLaReserva != null ? $"{ClienteDeLaReserva.Apellido}, {ClienteDeLaReserva.Nombre}" : $"ClienteId:{ClienteId}";
            string vehiculo = VehiculoDeLaReserva != null ? $"{VehiculoDeLaReserva.Marca} {VehiculoDeLaReserva.Modelo}" : $"VehiculoId:{VehiculoId}";
            return $"[{Id}] {cliente} | {vehiculo} | {FechaDesde:dd/MM/yyyy} → {FechaHasta:dd/MM/yyyy} ({CantidadDias} días) | ${CostoTotal:F2} | {Estado}";
        }
    }
}