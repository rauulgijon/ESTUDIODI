using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenManuelDI2Recuperacion.model
{
    internal class VehicleType
    {
        private int idVehicleType;
        private string type;

        public int IdVehicleType { get { return idVehicleType; } set { idVehicleType = value; } }
        public string Type { get { return type; } set { type = value; } }

        public VehicleType(int id, string type)
        {
            this.idVehicleType = id;
            this.type = type;
        }
    }
}