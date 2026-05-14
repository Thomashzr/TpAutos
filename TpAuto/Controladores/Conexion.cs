using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace dbPersonas.Controladores
{
    internal class Conexion
    {

        private static SqliteConnection miConexion = new SqliteConnection("Data Source=DbAutos.db");

        public static void OpenConexion()
        {

            if (miConexion.State == System.Data.ConnectionState.Closed)
            {
                miConexion.Open();
            }
        }



        public static void CloseConexion()
        {
            miConexion.Close();
        }

        public static SqliteConnection MiConexion { get { return miConexion; } }
    }
}
