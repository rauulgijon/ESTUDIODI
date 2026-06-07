using IronFit.persistence;
using System;
using System.Collections.Generic;

namespace IronFit.domain
{
    internal class Socio
    {
        private int idSocio;
        private string nombre;
        private string dni;
        private double cuotaMensual;
        private SocioPersistence pm;

        public int IdSocio { get { return idSocio; } set { idSocio = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Dni { get { return dni; } set { dni = value; } }
        public double CuotaMensual { get { return cuotaMensual; } set { cuotaMensual = value; } }

        public Socio(int idSocio, string nombre, string dni, double cuotaMensual)
        {
            this.idSocio = idSocio;
            this.nombre = nombre;
            this.dni = dni;
            this.cuotaMensual = cuotaMensual;
            pm = new SocioPersistence();
        }

        public Socio()
        {
            pm = new SocioPersistence();
        }

        // ==========================================
        // MÉTODOS ACTIVE RECORD
        // ==========================================
        public void insertar()
        {
            pm.insertarSocio(this);
            this.IdSocio = pm.ObtenerUltimoId();
        }

        public void modificar()
        {
            pm.actualizarSocio(this);
        }

        public void borrar()
        {
            pm.eliminarSocio(this.IdSocio);
        }

        public static List<Socio> leerTodos()
        {
            return SocioPersistence.leerSocios();
        }
    }
}