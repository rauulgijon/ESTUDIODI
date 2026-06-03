using System;
using System.Collections.Generic;
using ExamenManuelDI2Recuperacion.model;

namespace ExamenManuelDI2Recuperacion.controller
{
    internal class GuestPersistence
    {
        public static List<Guest> leerGuests()
        {
            Guest g = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("Select * from guest;");
            List<Guest> guests = new List<Guest>();
            foreach (List<Object> fila in aux)
            {
                g = new Guest(
                    Convert.ToInt32(fila[0]),
                    fila[1].ToString(),
                    Convert.ToInt32(fila[2]),
                    Convert.ToInt32(fila[3]),
                    Convert.ToInt32(fila[4]),
                    fila[5].ToString()
                );
                guests.Add(g);
            }
            return guests;
        }

        public void insertarGuest(Guest g)
        {
            string sql = "INSERT INTO guest (name, passport, telephone, vehicleID, licencePlate) VALUES ('" +
                         g.Name + "', " +
                         g.Passport + ", " +
                         g.Telephone + ", " +
                         g.VehicleId + ", '" +
                         g.LicencePlate + "');";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarGuest(Guest g)
        {
            string sql = "UPDATE guest SET " +
                         "name = '" + g.Name + "', " +
                         "passport = " + g.Passport + ", " +
                         "telephone = " + g.Telephone + ", " +
                         "vehicleID = " + g.VehicleId + ", " +
                         "licencePlate = '" + g.LicencePlate + "' " +
                         "WHERE idGUEST = " + g.IdGuest + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarGuest(int id)
        {
            string sql = "DELETE FROM guest WHERE idGUEST = " + id + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }
    }
}