using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace minihitodefinitivo.persistence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySqlConnection conexion;
        private const String cadenaConexion = "server=localhost;database=aceptasreto;uid=root;pwd=toor";

        private DBBroker()
        {
            DBBroker.conexion = new MySqlConnection(DBBroker.cadenaConexion);
        }

        public static DBBroker obtenerAgente()
        {
            if (DBBroker._instancia == null)
            {
                DBBroker._instancia = new DBBroker();
            }
            return DBBroker._instancia;
        }

        public List<Object> leer(String sql)
        {
            List<Object> resultado = new List<object>();
            List<Object> fila;
            int i;
            MySqlDataReader reader;
            MySqlCommand com = new MySqlCommand(sql, DBBroker.conexion);

            conectar();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                fila = new List<object>();
                for (i = 0; i <= reader.FieldCount - 1; i++)
                {
                    fila.Add(reader[i].ToString());
                }
                resultado.Add(fila);
            }
            desconectar();
            return resultado;
        }

        public int modificar(String sql)
        {
            MySqlCommand com = new MySqlCommand(sql, DBBroker.conexion);
            int resultado;
            conectar();
            resultado = com.ExecuteNonQuery();
            desconectar();
            return resultado;
        }

        private void conectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Closed)
            {
                DBBroker.conexion.Open();
            }
        }

        private void desconectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Open)
            {
                DBBroker.conexion.Close();
            }
        }

        public DataTable leerDataTable(string sql)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conexion);
            conectar();
            da.Fill(dt);
            desconectar();
            return dt;
        }
    }
}