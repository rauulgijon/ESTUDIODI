using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenManuelDI2Recuperacion.model
{
    internal class Reservation
    {
        private int idReservation;
        private int guestId;
        private int parcelId;
        private string checkInDate;  
        private string checkOutDate;
        private float totalCost;
        private string status;

        public int IdReservation { get { return idReservation; } set { idReservation = value; } }
        public int GuestId { get { return guestId; } set { guestId = value; } }
        public int ParcelId { get { return parcelId; } set { parcelId = value; } }
        public string CheckInDate { get { return checkInDate; } set { checkInDate = value; } }
        public string CheckOutDate { get { return checkOutDate; } set { checkOutDate = value; } }
        public float TotalCost { get { return totalCost; } set { totalCost = value; } }
        public string Status { get { return status; } set { status = value; } }

        public Reservation(int id, int guestId, int parcelId, string checkIn, string checkOut, float cost, string status)
        {
            this.idReservation = id;
            this.guestId = guestId;
            this.parcelId = parcelId;
            this.checkInDate = checkIn;
            this.checkOutDate = checkOut;
            this.totalCost = cost;
            this.status = status;
        }
    }
}