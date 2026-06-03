using System;
using System.Collections.Generic;
using ExamenManuelDI2Recuperacion.model;
namespace ExamenManuelDI2Recuperacion.controller

{
    internal class VehicleTypePersistence
    {
        public static List<VehicleType> leerVehicleTypes()
        {
            VehicleType vt = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("Select * from vehicletype;");
            List<VehicleType> vehicleTypes = new List<VehicleType>();
            foreach (List<Object> fila in aux)
            {
                vt = new VehicleType(Convert.ToInt32(fila[0]), fila[1].ToString());
                vehicleTypes.Add(vt);
            }
            return vehicleTypes;
        }

        public void insertarVehicleType(VehicleType vt)
        {
            string sql = "INSERT INTO vehicletype (vehicleType) VALUES ('" +
                         vt.Type + "');";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarVehicleType(VehicleType vt)
        {
            string sql = "UPDATE vehicletype SET " +
                         "vehicleType = '" + vt.Type + "' " +
                         "WHERE idvehicleType = " + vt.IdVehicleType + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarVehicleType(int id)
        {
            string sql = "DELETE FROM vehicletype WHERE idvehicleType = " + id + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }
    }
}