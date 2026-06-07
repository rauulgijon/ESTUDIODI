using System;
using System.Collections.Generic;
using minihitodefinitivo.persistence;

namespace minihitodefinitivo.domain
{
    public class Reto
    {
        public int Id_Reto { get; set; }
        public RetoPersistence pm;
        public string Descripcion_reto { get; set; }

        public Reto()
        {
            pm = new RetoPersistence();
        }

        public Reto(int id, string descripcion)
        {
            this.Id_Reto = id;
            this.Descripcion_reto = descripcion;
            pm = new RetoPersistence();
        }

        public Reto(string descripcion)
        {
            this.Descripcion_reto = descripcion;
            pm = new RetoPersistence();
        }

        public Reto(int id_reto)
        {
            pm = new RetoPersistence();
            this.Id_Reto = id_reto;
        }

        public void insertar()
        {
            pm.InsertarReto(this);
        }

        public void borrar()
        {
            pm.BorrarReto(this);
        }

        public void modificar()
        {
            pm.ModificarReto(this);
        }

        public List<Reto> getListaReto()
        {
            if (pm == null) pm = new RetoPersistence();
            return pm.LeerRetos();
        }
    }
}