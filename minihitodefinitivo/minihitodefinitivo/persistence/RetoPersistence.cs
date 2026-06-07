using System;
using System.Collections.Generic;
using minihitodefinitivo.domain;

namespace minihitodefinitivo.persistence
{
    public class RetoPersistence
    {
        public List<Reto> LeerRetos()
        {
            List<Reto> lista = new List<Reto>();
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT id_reto, descripcion_reto FROM aceptasreto.reto;");

            foreach (List<Object> c in aux)
            {
                int id = SafeInt(c, 0);
                string descripcion = SafeString(c, 1);
                Reto reto = new Reto(id, descripcion);
                lista.Add(reto);
            }
            return lista;
        }

        public void InsertarReto(Reto reto)
        {
            String sql = $"INSERT INTO aceptasreto.reto (descripcion_reto) VALUES ('{reto.Descripcion_reto}');";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void BorrarReto(Reto reto)
        {
            String sql = $"DELETE FROM aceptasreto.reto WHERE id_reto = {reto.Id_Reto};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void ModificarReto(Reto reto)
        {
            String sql = $"UPDATE aceptasreto.reto SET descripcion_reto = '{reto.Descripcion_reto}' WHERE id_reto = {reto.Id_Reto};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        private static int SafeInt(List<object> row, int index)
        {
            if (row == null || index >= row.Count) return 0;
            object v = row[index];
            if (v == null || v == DBNull.Value) return 0;
            if (int.TryParse(v.ToString(), out int r)) return r;
            return 0;
        }

        private static string SafeString(List<object> row, int index)
        {
            if (row == null || index >= row.Count) return string.Empty;
            object v = row[index];
            if (v == null || v == DBNull.Value) return string.Empty;
            return v.ToString();
        }
    }
}