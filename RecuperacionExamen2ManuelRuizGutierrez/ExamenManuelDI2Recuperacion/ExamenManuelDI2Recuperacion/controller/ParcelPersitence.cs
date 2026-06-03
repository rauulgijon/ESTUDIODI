using System;
using System.Collections.Generic;
using System.Windows.Controls;
using ExamenManuelDI2Recuperacion.model;
namespace ExamenManuelDI2Recuperacion.controller
{
    internal class ParcelPersistence
    {
        public static List<Parcel> leerParcels()
        {
            Parcel p = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("Select * from parcel;");
            List<Parcel> parcels = new List<Parcel>();
            foreach (List<Object> fila in aux)
            {
                p = new Parcel(
                    Convert.ToInt32(fila[0]),
                    fila[1].ToString(),
                    Convert.ToInt32(fila[2]),
                    Convert.ToInt32(fila[3]),
                    Convert.ToSingle(fila[4])
                );
                parcels.Add(p);
            }
            return parcels;
        }

        public void insertarParcel(Parcel p)
        {
            string sql = "INSERT INTO parcel (parcelSize, light, water, Parcelcol) VALUES ('" +
                         p.ParcelSize + "', " +
                         p.Light + ", " +
                         p.Water + ", " +
                         p.Parcelcol.ToString(System.Globalization.CultureInfo.InvariantCulture) + ");";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarParcel(Parcel p)
        {
            string sql = "UPDATE parcel SET " +
                         "parcelSize = '" + p.ParcelSize + "', " +
                         "light = " + p.Light + ", " +
                         "water = " + p.Water + ", " +
                         "Parcelcol = " + p.Parcelcol.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " +
                         "WHERE idParcel = " + p.IdParcel + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarParcel(int id)
        {
            string sql = "DELETE FROM parcel WHERE idParcel = " + id + ";";
            int a = DBBroker.obtenerAgente().modificar(sql);
        }
    }
}