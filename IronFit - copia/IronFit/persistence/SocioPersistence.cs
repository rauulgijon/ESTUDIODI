using Estudio1.persistence;
using IronFit.domain;
using System;
using System.Collections.Generic;

namespace IronFit.persistence
{
    internal class SocioPersistence
    {
        public static List<Socio> leerSocios()
        {
            Socio s = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("Select * from socio;");
            List<Socio> socios = new List<Socio>();
            foreach (List<Object> fila in aux)
            {
                s = new Socio(
                    Convert.ToInt32(fila[0]),
                    fila[1].ToString(),
                    fila[2].ToString(),
                    Convert.ToDouble(fila[3])
                );
                socios.Add(s);
            }
            return socios;
        }

        public void insertarSocio(Socio s)
        {
            string cuota = s.CuotaMensual.ToString().Replace(",", ".");
            string sql = "INSERT INTO socio (nombre, dni, cuotaMensual) VALUES ('" +
                         s.Nombre + "', '" +
                         s.Dni + "', " +
                         cuota + ");";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarSocio(Socio s)
        {
            string cuota = s.CuotaMensual.ToString().Replace(",", ".");
            string sql = "UPDATE socio SET " +
                         "nombre = '" + s.Nombre + "', " +
                         "dni = '" + s.Dni + "', " +
                         "cuotaMensual = " + cuota + " " +
                         "WHERE idSocio = " + s.IdSocio + ";";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarSocio(int id)
        {
            string sql = "DELETE FROM socio WHERE idSocio = " + id + ";";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public int ObtenerUltimoId()
        {
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT MAX(idSocio) FROM socio;");
            foreach (List<Object> c in aux)
            {
                if (c[0] != DBNull.Value && c[0] != null)
                    return Convert.ToInt32(c[0]);
            }
            return 0;
        }
    }
}