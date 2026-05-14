using System.Security.Cryptography.X509Certificates;

namespace TpAuto.Modelos
{
    internal class Vehiculo
    {
        public int Id { get; set; }
        public string Patente { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public double PrecioPorDia { get; set; }
        public int Capacidad { get; set; }

        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Vehiculo() { }

        public Vehiculo(int id, string patente, string marca, string modelo, double precioPorDia, int capacidad)
        {
            Id = id;
            Patente = patente;
            Marca = marca;
            Modelo = modelo;
            PrecioPorDia = precioPorDia;
            Capacidad = capacidad;

            Reservas = new List<Reserva>();
        }

        public override string ToString()
        {
            return $"[{Id}] {Marca} {Modelo} - Patente: {Patente} | ${PrecioPorDia}/día";
        }
    }
}