using System;
using System.Collections.Generic;
using minihitodefinitivo.persistence;

namespace minihitodefinitivo.domain
{
    public class Empresa
    {
        private int idEmpresa;
        private String razonsocial;
        private String dni;
        private String descripcion;
        private int telefono;
        private String direccion;
        public EmpresaPersistence pm;

        public Empresa(string razonsocial, string dni, string descripcion, int telefono, string direccion)
        {
            this.razonsocial = razonsocial;
            this.dni = dni;
            this.descripcion = descripcion;
            this.telefono = telefono;
            this.direccion = direccion;
            pm = new EmpresaPersistence();
        }

        public Empresa(int idEmpresa, string razonsocial, string dni, string descripcion, int telefono, string direccion)
        {
            this.idEmpresa = idEmpresa;
            this.razonsocial = razonsocial;
            this.dni = dni;
            this.descripcion = descripcion;
            this.telefono = telefono;
            this.direccion = direccion;
            pm = new EmpresaPersistence();
        }

        public Empresa()
        {
            pm = new EmpresaPersistence();
        }

        public Empresa(int idEmpresa)
        {
            pm = new EmpresaPersistence();
            this.idEmpresa = idEmpresa;
        }

        public List<Empresa> getListaEmpresa()
        {
            return pm.LeerEmpresas();
        }

        public int Id { get => idEmpresa; set => idEmpresa = value; }
        public String Razon { get => razonsocial; set => razonsocial = value; }
        public String Dni { get => dni; set => dni = value; }
        public String Descripcion { get => descripcion; set => descripcion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public String Direccion { get => direccion; set => direccion = value; }

        public void insertar()
        {
            pm.InsertarEmpresa(this);
        }

        public void borrar()
        {
            pm.BorrarEmpresa(this);
        }

        public void modificar()
        {
            pm.ModificarEmpresa(this);
        }
    }
}