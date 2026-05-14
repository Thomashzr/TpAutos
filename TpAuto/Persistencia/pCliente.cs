
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using TpAuto.Modelos;
using TpAuto.Persistencia;

namespace TpAuto.Persistencia
{
    internal class pCliente
    {
        public static List<Cliente> getAll()
        {
            List<Cliente> clientes = new List<Cliente>();

            SqliteCommand sqliteCommand = new SqliteCommand("Select Id, Nombre, Apellido, Telefono, DNI from Cliente");

            sqliteCommand.Connection = Conexion.MiConexion;

            SqliteDataReader dataReader = sqliteCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Cliente cliente = new Cliente();
                cliente.Id = dataReader.GetInt32(0);
                cliente.Nombre = dataReader.GetString(1);
                cliente.Apellido = dataReader.GetString(2);
                cliente.Telefono = dataReader.GetString(3);
                cliente.Dni = dataReader.GetString(4);
                clientes.Add(cliente);
            }

            return clientes;
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
    }
}
