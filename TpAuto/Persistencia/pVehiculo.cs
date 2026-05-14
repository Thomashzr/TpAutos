
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using TpAuto.Modelos;
using TpAuto.Persistencia;

namespace TpAuto.Persistencia
{
    internal class pVehiculo
    {
        public static List<Vehiculo> getAll()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();

            SqliteCommand sqliteCommand = new SqliteCommand("Select Id, Marca, Modelo, Patente, Capacidad, PrecioDia from Autos");

            sqliteCommand.Connection = Conexion.MiConexion;

            SqliteDataReader dataReader = sqliteCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculo.Id = dataReader.GetInt32(0);
                vehiculo.Marca = dataReader.GetString(1);
                vehiculo.Modelo = dataReader.GetString(2);
                vehiculo.Patente = dataReader.GetString(3);
                vehiculo.Capacidad = dataReader.GetInt32(4);
                vehiculo.PrecioPorDia = dataReader.GetDouble(5);
                vehiculos.Add(vehiculo);
            }

            return vehiculos;
        }

        public static Vehiculo GuardarVehiculo(Vehiculo v)
        {
            SqliteCommand sqliteCommand = new SqliteCommand("INSERT INTO Autos (Id, Marca, Modelo, Patente, Capacidad, PrecioDia) VALUES (@Id, @Marca, @Modelo, @Patente, @Capacidad, @PrecioDia)");
            sqliteCommand.Parameters.Add(new SqliteParameter("@Id", v.Id));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Marca", v.Marca));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Modelo", v.Modelo));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Patente", v.Patente));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Capacidad", v.Capacidad));
            sqliteCommand.Parameters.Add(new SqliteParameter("@PrecioDia", v.PrecioPorDia));
            sqliteCommand.Connection = Conexion.MiConexion;
            sqliteCommand.ExecuteNonQuery();
            return v;
        }

        public static void ModificarVehiculo(Vehiculo v)
        {
            SqliteCommand sqliteCommand = new SqliteCommand("UPDATE Autos SET Marca = @Marca, Modelo = @Modelo, PrecioDia = @PrecioDia, Capacidad = @Capacidad WHERE Id = @Id");
            sqliteCommand.Parameters.Add(new SqliteParameter("@Marca", v.Marca));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Modelo", v.Modelo));
            sqliteCommand.Parameters.Add(new SqliteParameter("@PrecioDia", v.PrecioPorDia));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Capacidad", v.Capacidad));
            sqliteCommand.Parameters.Add(new SqliteParameter("@Id", v.Id));
            sqliteCommand.Connection = Conexion.MiConexion;
            sqliteCommand.ExecuteNonQuery();
        }

        public static void EliminarVehiculo(int id)
        {
            SqliteCommand sqliteCommand = new SqliteCommand("DELETE FROM Autos WHERE Id = @Id");
            sqliteCommand.Parameters.Add(new SqliteParameter("@Id", id));
            sqliteCommand.Connection = Conexion.MiConexion;
            sqliteCommand.ExecuteNonQuery();
        }
    }
}