using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TuProyecto.persistence  // <-- 1. CAMBIA ESTO
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private MySqlConnection conexion;

        // <-- 2. REVISA TU CONTRASEÑA Y BASE DE DATOS AQUÍ
        private const string connectionString = "server=localhost;port=3306;database=manchacentro;uid=root;pwd=toor;";

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

        // ========================================================
        // MÉTODO ESTRELLA PARA CRYSTAL REPORTS (Devuelve DataTable)
        // ========================================================
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
            catch (Exception ex)
            {
                throw new Exception("Error en la BD al cargar el informe: " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            return dt;
        }

        // ========================================================
        // MÉTODOS CLÁSICOS DEL CRUD (Listas y Modificaciones)
        // ========================================================
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
            catch (Exception ex)
            {
                throw new Exception("Error de lectura en BD: " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            return resultado;
        }

        public int modificar(string sql)
        {
            int filasAfectadas = 0;
            try
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conexion);
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la BD: " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            return filasAfectadas;
        }
    }
}