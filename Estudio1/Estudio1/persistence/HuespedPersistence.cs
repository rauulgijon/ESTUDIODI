using System;
using System.Collections.Generic;
using Estudio1.domain;

namespace Estudio1.persistence
{
    public class HuespedPersistence
    {
        public static List<Huesped> leerHuespedes()
        {
            // Cambiado a idHuesped, nombre, dni...
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT idHuesped, nombre, dni, telefono, idTipoVehiculo, matricula FROM huesped;");
            List<Huesped> huespedes = new List<Huesped>();

            foreach (List<Object> fila in aux)
            {
                Huesped h = new Huesped(Convert.ToInt32(fila[0]), fila[1].ToString(), Convert.ToInt32(fila[2]), Convert.ToInt32(fila[3]), Convert.ToInt32(fila[4]), fila[5].ToString());
                huespedes.Add(h);
            }
            return huespedes;
        }

        public void insertarHuesped(Huesped h)
        {
            string sql = $"INSERT INTO huesped (nombre, dni, telefono, idTipoVehiculo, matricula) VALUES ('{h.nombre}', {h.dniPasaporte}, {h.telefono}, {h.idTipoVehiculo}, '{h.matricula}');";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarHuesped(Huesped h)
        {
            string sql = $"UPDATE huesped SET nombre = '{h.nombre}', dni = {h.dniPasaporte}, telefono = {h.telefono}, idTipoVehiculo = {h.idTipoVehiculo}, matricula = '{h.matricula}' WHERE idHuesped = {h.idHuesped};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarHuesped(int id)
        {
            string sql = $"DELETE FROM huesped WHERE idHuesped = {id};";
            DBBroker.obtenerAgente().modificar(sql);
        }
    }
}