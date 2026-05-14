using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using TpAuto.Modelos;

namespace TpAuto.Persistencia
{
    internal class pReserva
    {
        public static List<Reserva> getAll()
        {
            List<Reserva> reservas = new List<Reserva>();

            SqliteCommand sqliteCommand = new SqliteCommand("Select Id, IdCliente, IdAuto, FechaDesde, FechaHasta, PrecioTotal from Reservas");

            sqliteCommand.Connection = Conexion.MiConexion;

            SqliteDataReader dataReader = sqliteCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Reserva reserva = new Reserva();
                reserva.Id = dataReader.GetInt32(0);
                reserva.ClienteId = dataReader.GetInt32(1);
                reserva.VehiculoId = dataReader.GetInt32(2);
                reserva.FechaDesde = DateTime.ParseExact(dataReader.GetString(3), "dd/MM/yyyy", null);
                reserva.FechaHasta = DateTime.ParseExact(dataReader.GetString(4), "dd/MM/yyyy", null);
                reserva.CostoTotal = dataReader.GetDouble(5);
                reservas.Add(reserva);
            }

            return reservas;
        }

        public static Reserva GuardarReserva(Reserva r)
        {
            SqliteCommand sqliteCommand = new SqliteCommand("INSERT INTO Reservas (Id, IdCliente, IdAuto, FechaDesde, FechaHasta, PrecioTotal) VALUES (@Id, @IdCliente, @IdAuto, @FechaDesde, @FechaHasta, @PrecioTotal)");
            sqliteCommand.Parameters.Add(new SqliteParameter("@Id", r.Id));
            sqliteCommand.Parameters.Add(new SqliteParameter("@IdCliente", r.ClienteId));
            sqliteCommand.Parameters.Add(new SqliteParameter("@IdAuto", r.VehiculoId));
            sqliteCommand.Parameters.Add(new SqliteParameter("@FechaDesde", r.FechaDesde.ToString("dd/MM/yyyy")));
            sqliteCommand.Parameters.Add(new SqliteParameter("@FechaHasta", r.FechaHasta.ToString("dd/MM/yyyy")));
            sqliteCommand.Parameters.Add(new SqliteParameter("@PrecioTotal", r.CostoTotal));
            sqliteCommand.Connection = Conexion.MiConexion;
            sqliteCommand.ExecuteNonQuery();
            return r;
        }

        public static void ModificarReserva(Reserva r)
        {
            SqliteCommand sqliteCommand = new SqliteCommand("UPDATE Reservas SET FechaDesde = @FechaDesde, FechaHasta = @FechaHasta, PrecioTotal = @PrecioTotal WHERE Id = @Id");
            sqliteCommand.Parameters.Add(new SqliteParameter("@FechaDesde", r.FechaDesde.ToString("dd/MM/yyyy")));
            sqliteCommand.Parameters.Add(new SqliteParameter("@FechaHasta", r.FechaHasta.ToString("dd/MM/yyyy")));
            sqliteCommand.Parameters.Add(new SqliteParameter("@PrecioTotal", r.CostoTotal));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Id", r.Id));
            sqliteCommand.Connection = Conexion.MiConexion;
            sqliteCommand.ExecuteNonQuery();
        }

        public static void EliminarReserva(int id)
        {
            SqliteCommand sqliteCommand = new SqliteCommand("DELETE FROM Reservas WHERE Id = @Id");
            sqliteCommand.Parameters.Add(new SqliteParameter("@Id", id));
            sqliteCommand.Connection = Conexion.MiConexion;
            sqliteCommand.ExecuteNonQuery();
        }

        public static List<Reserva> getByVehiculo(int idAuto)
        {
            List<Reserva> reservas = new List<Reserva>();

            SqliteCommand sqliteCommand = new SqliteCommand("Select Id, IdCliente, IdAuto, FechaDesde, FechaHasta, PrecioTotal from Reservas WHERE IdAuto = @IdAuto");
            sqliteCommand.Parameters.Add(new SqliteParameter("@IdAuto", idAuto));
            sqliteCommand.Connection = Conexion.MiConexion;

            SqliteDataReader dataReader = sqliteCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Reserva reserva = new Reserva();
                reserva.Id = dataReader.GetInt32(0);
                reserva.ClienteId = dataReader.GetInt32(1);
                reserva.VehiculoId = dataReader.GetInt32(2);
                reserva.FechaDesde = DateTime.ParseExact(dataReader.GetString(3), "dd/MM/yyyy", null);
                reserva.FechaHasta = DateTime.ParseExact(dataReader.GetString(4), "dd/MM/yyyy", null);
                reserva.CostoTotal = dataReader.GetDouble(5);
                reservas.Add(reserva);
            }

            return reservas;
        }
    }
}