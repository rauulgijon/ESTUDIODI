using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExamenVeterinaria.persistence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private MySqlConnection conexion;
        private const string connectionString = "server=localhost;port=3306;database=veterinaria_db;uid=root;pwd=toor;";

        private DBBroker()
        {
            conexion = new MySqlConnection(connectionString);
        }

        public static DBBroker obtenerAgente()
        {
            if (_instancia == null)
            {
                _instancia = new DBBroker();
            }
            return _instancia;
        }

        // Método tradicional para leer listas (el que usabas en los DataGrid)
        public List<Object> leer(string sql)
        {
            List<Object> resultado = new List<Object>();
            try
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<Object> fila = new List<Object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fila.Add(reader[i].ToString());
                    }
                    resultado.Add(fila);
                }
                reader.Close();
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }

        // Método específico para Crystal Reports (Devuelve un DataTable)
        public DataTable LeerParaReporte(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }

        // Método para hacer INSERT, UPDATE o DELETE
        public int modificar(string sql)
        {
            int filasAfectadas = 0;
            try
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }
            return filasAfectadas;
        }
    }
}