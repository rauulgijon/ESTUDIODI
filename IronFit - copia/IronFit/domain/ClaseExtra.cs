using IronFit.persistence;
using System;
using System.Collections.Generic;

namespace IronFit.domain
{
    internal class ClaseExtra
    {
        private int idClase;
        private int idSocio;
        private DateTime fecha;
        private string nombreClase;
        private double coste;
        private ClaseExtraPersistence pm;

        public int IdClase { get { return idClase; } set { idClase = value; } }
        public int IdSocio { get { return idSocio; } set { idSocio = value; } }
        public DateTime Fecha { get { return fecha; } set { fecha = value; } }
        public string NombreClase { get { return nombreClase; } set { nombreClase = value; } }
        public double Coste { get { return coste; } set { coste = value; } }

        public ClaseExtra(int idClase, int idSocio, DateTime fecha, string nombreClase, double coste)
        {
            this.idClase = idClase;
            this.idSocio = idSocio;
            this.fecha = fecha;
            this.nombreClase = nombreClase;
            this.coste = coste;
            pm = new ClaseExtraPersistence();
        }

        public ClaseExtra()
        {
            pm = new ClaseExtraPersistence();
        }

        // ==========================================
        // MÉTODOS ACTIVE RECORD
        // ==========================================
        public void insertar()
        {
            pm.insertarClaseExtra(this);
            this.IdClase = pm.ObtenerUltimoId();
        }

        public void modificar()
        {
            pm.actualizarClaseExtra(this);
        }

        public void borrar()
        {
            pm.eliminarClaseExtra(this.IdClase);
        }

        public static List<ClaseExtra> leerTodas()
        {
            return ClaseExtraPersistence.leerClasesExtras();
        }
    }
}