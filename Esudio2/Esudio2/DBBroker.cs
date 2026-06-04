using System;
using System.Collections.Generic;

namespace Estudio1.persistence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySql.Data.MySqlClient.MySqlConnection conexion;

        // ¡IMPORTANTE! Revisa tu contraseña
        private const String cadenaConexion = "server=localhost;database=taller_db;uid=root;pwd=toor";

        private DBBroker()
        {
            conexion = new MySql.Data.MySqlClient.MySqlConnection(cadenaConexion);
        }

        public static DBBroker obtenerAgente()
        {
            if (_instancia == null) _instancia = new DBBroker();
            return _instancia;
        }

        public List<Object> leer(String sql)
        {
            List<Object> resultado = new List<object>();
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, conexion);

            conectar();
            MySql.Data.MySqlClient.MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                List<Object> fila = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fila.Add(reader[i].ToString());
                }
                resultado.Add(fila);
            }
            desconectar();
            return resultado;
        }

        public System.Data.DataTable LeerParaReporte(String sql)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, conexion);
            MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(com);

            conectar();
            adapter.Fill(dt);
            desconectar();

            return dt;
        }

        public int modificar(String sql)
        {
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, conexion);
            conectar();
            int resultado = com.ExecuteNonQuery();
            desconectar();
            return resultado;
        }

        private void conectar()
        {
            if (conexion.State == System.Data.ConnectionState.Closed) conexion.Open();
        }

        private void desconectar()
        {
            if (conexion.State == System.Data.ConnectionState.Open) conexion.Close();
        }
    }
}