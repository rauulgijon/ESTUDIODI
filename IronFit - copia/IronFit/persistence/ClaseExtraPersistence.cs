using IronFit.persistence;
using IronFit.domain;
using System;
using System.Collections.Generic;

namespace IronFit.persistence
{
    internal class ClaseExtraPersistence
    {
        public static List<ClaseExtra> leerClasesExtras()
        {
            ClaseExtra c = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("Select * from claseextra;");
            List<ClaseExtra> clases = new List<ClaseExtra>();

            foreach (List<Object> fila in aux)
            {
                c = new ClaseExtra(
                    Convert.ToInt32(fila[0]),
                    Convert.ToInt32(fila[1]),
                    Convert.ToDateTime(fila[2]),
                    fila[3].ToString(),
                    Convert.ToDouble(fila[4])
                );
                clases.Add(c);
            }
            return clases;
        }

        public void insertarClaseExtra(ClaseExtra c)
        {
            string costeStr = c.Coste.ToString().Replace(",", ".");
            string fechaStr = c.Fecha.ToString("yyyy-MM-dd");

            string sql = "INSERT INTO claseextra (idSocio, fecha, nombreClase, coste) VALUES (" +
                         c.IdSocio + ", '" +
                         fechaStr + "', '" +
                         c.NombreClase + "', " +
                         costeStr + ");";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarClaseExtra(ClaseExtra c)
        {
            string costeStr = c.Coste.ToString().Replace(",", ".");
            string fechaStr = c.Fecha.ToString("yyyy-MM-dd");

            string sql = "UPDATE claseextra SET " +
                         "idSocio = " + c.IdSocio + ", " +
                         "fecha = '" + fechaStr + "', " +
                         "nombreClase = '" + c.NombreClase + "', " +
                         "coste = " + costeStr + " " +
                         "WHERE idClase = " + c.IdClase + ";";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarClaseExtra(int id)
        {
            string sql = "DELETE FROM claseextra WHERE idClase = " + id + ";";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public int ObtenerUltimoId()
        {
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT MAX(idClase) FROM claseextra;");
            foreach (List<Object> c in aux)
            {
                if (c[0] != DBNull.Value && c[0] != null)
                    return Convert.ToInt32(c[0]);
            }
            return 0;
        }
    }
}