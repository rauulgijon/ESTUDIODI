using System;
using System.Collections.Generic;
using ExamenManuelDI2Recuperacion.model;
namespace ExamenManuelDI2Recuperacion.controller
{
    internal class ReservationPersistence
    {
        public static List<Reservation> leerReservations()
        {
            Reservation r = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("Select * from reservation;");
            List<Reservation> reservations = new List<Reservation>();
            foreach (List<Object> fila in aux)
            {
                DateTime checkIn = Convert.ToDateTime(fila[3]);
                DateTime checkOut = Convert.ToDateTime(fila[4]);

                r = new Reservation(
                    Convert.ToInt32(fila[0]),
                    Convert.ToInt32(fila[1]),
                    Convert.ToInt32(fila[2]),
                    checkIn.ToString("dd/MM/yyyy"),
                    checkOut.ToString("dd/MM/yyyy"),
                    Convert.ToSingle(fila[5]),
                    fila[6].ToString()
                );
                reservations.Add(r);
            }
            return reservations;
        }

        public void insertarReservation(Reservation r)
        {
            string sql = "INSERT INTO reservation (guestID, ParcelID, checkInDate, checkOutDate, totalCost, status) VALUES (" +
                         r.GuestId + ", " +
                         r.ParcelId + ", '" +
                         Convert.ToDateTime(r.CheckInDate).ToString("yyyy-MM-dd") + "', '" +
                         Convert.ToDateTime(r.CheckOutDate).ToString("yyyy-MM-dd") + "', " +
                         r.TotalCost.ToString(System.Globalization.CultureInfo.InvariantCulture) + ", '" +
                         r.Status + "');";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarReservation(Reservation r)
        {
            string sql = "UPDATE reservation SET " +
                         "guestID = " + r.GuestId + ", " +
                         "ParcelID = " + r.ParcelId + ", " +
                         "checkInDate = '" + Convert.ToDateTime(r.CheckInDate).ToString("yyyy-MM-dd") + "', " +
                         "checkOutDate = '" + Convert.ToDateTime(r.CheckOutDate).ToString("yyyy-MM-dd") + "', " +
                         "totalCost = " + r.TotalCost.ToString(System.Globalization.CultureInfo.InvariantCulture) + ", " +
                         "status = '" + r.Status + "' " +
                         "WHERE idRESERVATION = " + r.IdReservation + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarReservation(int id)
        {
            string sql = "DELETE FROM reservation WHERE idRESERVATION = " + id + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }
    }
}