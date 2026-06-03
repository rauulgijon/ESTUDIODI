using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenManuelDI2Recuperacion.model
{
    internal class Guest
    {
        private int idGuest;
        private string name;
        private int passport;
        private int telephone;
        private int vehicleId;
        private string licencePlate;

        public int IdGuest { get { return idGuest; } set { idGuest = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Passport { get { return passport; } set { passport = value; } }
        public int Telephone { get { return telephone; } set { telephone = value; } }
        public int VehicleId { get { return vehicleId; } set { vehicleId = value; } }
        public string LicencePlate { get { return licencePlate; } set { licencePlate = value; } }

        public Guest(int id, string name, int passport, int telephone, int vehicleId, string licencePlate)
        {
            this.idGuest = id;
            this.name = name;
            this.passport = passport;
            this.telephone = telephone;
            this.vehicleId = vehicleId;
            this.licencePlate = licencePlate;
        }
    }
}