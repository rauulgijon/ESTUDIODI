using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenManuelDI2Recuperacion.model
{
    internal class Parcel
    {
        private int idParcel;
        private string parcelSize;
        private int light;
        private int water; 
        private float parcelcol;

        public int IdParcel { get { return idParcel; } set { idParcel = value; } }
        public string ParcelSize { get { return parcelSize; } set { parcelSize = value; } }
        public int Light { get { return light; } set { light = value; } }
        public int Water { get { return water; } set { water = value; } }
        public float Parcelcol { get { return parcelcol; } set { parcelcol = value; } }

        public Parcel(int id, string size, int light, int water, float col)
        {
            this.idParcel = id;
            this.parcelSize = size;
            this.light = light;
            this.water = water;
            this.parcelcol = col;
        }
    }
}