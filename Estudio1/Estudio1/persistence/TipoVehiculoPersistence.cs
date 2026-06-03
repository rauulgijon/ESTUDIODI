using System;
using System.Collections.Generic;
using Estudio1.domain;

namespace Estudio1.persistence
{
    public class TipoVehiculoPersistence
    {
        public static List<TipoVehiculo> leerTiposVehiculo()
        {
            // Asumo que la columna en tu base de datos se llama tipoVehiculo
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT idTipoVehiculo, tipoVehiculo FROM tipovehiculo;");
            List<TipoVehiculo> tipos = new List<TipoVehiculo>();

            foreach (List<Object> fila in aux)
            {
                TipoVehiculo t = new TipoVehiculo(Convert.ToInt32(fila[0]), fila[1].ToString());
                tipos.Add(t);
            }
            return tipos;
        }

        public void insertarTipoVehiculo(TipoVehiculo t)
        {
            string sql = $"INSERT INTO tipovehiculo (tipoVehiculo) VALUES ('{t.tipoVehiculoNombre}');";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarTipoVehiculo(TipoVehiculo t)
        {
            string sql = $"UPDATE tipovehiculo SET tipoVehiculo = '{t.tipoVehiculoNombre}' WHERE idTipoVehiculo = {t.idTipoVehiculo};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarTipoVehiculo(int id)
        {
            string sql = $"DELETE FROM tipovehiculo WHERE idTipoVehiculo = {id};";
            DBBroker.obtenerAgente().modificar(sql);
        }
    }
}