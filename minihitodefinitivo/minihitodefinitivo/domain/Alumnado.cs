using System;
using System.Collections.Generic;
using minihitodefinitivo.persistence;

namespace minihitodefinitivo.domain
{
    internal class Alumnado
    {
        private int idAlumnado;
        private String nombre;
        private String apellidos;
        private int especialidad;
        private int? grupo;
        public AlumnadoPersistence pm;

        public Alumnado(string nombre, string apellidos, int especialidad)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.especialidad = especialidad;
            pm = new AlumnadoPersistence();
        }

        public Alumnado(int idAlumnado, string nombre, string apellidos, int especialidad, int idGrupo)
        {
            this.idAlumnado = idAlumnado;
            this.apellidos = apellidos;
            this.nombre = nombre;
            this.especialidad = especialidad;
            this.grupo = idGrupo;
            pm = new AlumnadoPersistence();
        }

        public Alumnado()
        {
            pm = new AlumnadoPersistence();
        }

        public Alumnado(int idAlumnado)
        {
            pm = new AlumnadoPersistence();
            this.idAlumnado = idAlumnado;
        }

        public List<Alumnado> getListaPersonas()
        {
            return pm.LeerAlumno();
        }

        public int Id { get => idAlumnado; set => idAlumnado = value; }
        public String Nombre { get => nombre; set => nombre = value; }
        public String Apellidos { get => apellidos; set => apellidos = value; }
        public int Especialidad { get => especialidad; set => especialidad = value; }
        public int? Grupo { get => grupo; set => grupo = value; }

        public void insertar()
        {
            pm.InsertarAlumno(this);
        }

        public void borrar()
        {
            pm.BorrarAlumno(this);
        }

        public void modificar()
        {
            pm.ModificarAlumno(this);
        }

        public void asignarGrupo(int idGrupo)
        {
            this.grupo = idGrupo;
            pm.AsignarGrupo(this);
        }

        public void desasignarGrupo()
        {
            this.grupo = null;
            pm.DesasignarGrupo(this);
        }

        public string NombreCompleto
        {
            get
            {
                string n = Nombre?.Trim() ?? string.Empty;
                string a = Apellidos?.Trim() ?? string.Empty;
                return string.IsNullOrEmpty(a) ? n : $"{n} {a}";
            }
        }
    }
}