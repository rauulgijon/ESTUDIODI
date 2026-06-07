using System;
using System.Collections.Generic;
using minihitodefinitivo.persistence;

namespace minihitodefinitivo.domain
{
    internal class Grupo
    {
        private int idGrupo;
        private string nombre;
        public GrupoPersistence pm;

        public Grupo(int idGrupo, string nombre)
        {
            this.idGrupo = idGrupo;
            this.nombre = nombre;
            pm = new GrupoPersistence();
        }

        public Grupo(string nombre)
        {
            this.nombre = nombre;
            pm = new GrupoPersistence();
        }

        public Grupo(int idGrupo)
        {
            pm = new GrupoPersistence();
            this.idGrupo = idGrupo;
        }

        public Grupo()
        {
            pm = new GrupoPersistence();
        }

        public int Id { get => idGrupo; set => idGrupo = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public List<Grupo> getGrupos()
        {
            return pm.LeerGrupos();
        }

        public void insertar()
        {
            pm.InsertarGrupo(this);
        }

        public void borrar()
        {
            pm.BorrarGrupo(this);
        }

        public void modificar()
        {
            pm.ModificarGrupo(this);
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}