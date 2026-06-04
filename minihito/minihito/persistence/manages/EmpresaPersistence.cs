using minihito.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using minihito.domain;
namespace minihito.persistence.manages
{
    public class EmpresaPersistence
    {
        private DataTable table { get; set; }
        List<Empresa> ListaEmpresa { get; set; }

        public EmpresaPersistence()
        {
            table = new DataTable();
            ListaEmpresa = new List<Empresa>();
        }

        public List<Empresa> LeerEmpresa()
        {
            List<Empresa> lista = new List<Empresa>();
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT idEmpresa, razonsocial, dni, descripcion, telefono,direccion FROM aceptasreto.empresa;");
            foreach (List<Object> c in aux)
            {
                int idEmpresa = SafeInt(c, 0);
                string razonsocial = SafeString(c, 1);
                string dni = SafeString(c, 2);
                string descripcion = SafeString(c, 3);
                int telefono = SafeInt(c, 4);
                string direccion = SafeString(c, 5);
                Empresa empresa = new Empresa(idEmpresa, razonsocial, dni, descripcion, telefono, direccion);
                lista.Add(empresa);
            }
            return lista;
        }

        public void InsertarEmpresa(Empresa empresa)
        {
            String sql = $"INSERT INTO aceptasreto.empresa VALUES (null,'{empresa.Razon}','{empresa.Dni}','{empresa.Descripcion}','{empresa.Telefono}','{empresa.Direccion}')";
            DBBroker.obtenerAgente().modificar(sql);
        }
        public void BorrarEmpresa(Empresa empresa)
        {
            String sql = $"DELETE FROM aceptasreto.empresa WHERE idEmpresa = {empresa.Id};";
            DBBroker.obtenerAgente().modificar(sql);
        }
        public void ModificarEmpresa(Empresa empresa)
        {
            String sql = $"UPDATE aceptasreto.empresa SET razonsocial = '{empresa.Razon}', dni = '{empresa.Dni}', descripcion = '{empresa.Descripcion}', telefono = '{empresa.Telefono}', direccion = '{empresa.Direccion}' WHERE idEmpresa = {empresa.Id};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public List<Empresa> LeerEmpresas()
        {
            List<Empresa> lista = new List<Empresa>();
            List<Object> aux = DBBroker.obtenerAgente().leer(
                "SELECT idEmpresa, razonsocial, dni, descripcion, telefono,direccion FROM aceptasreto.empresa ORDER BY razonsocial;"
            );

            foreach (List<Object> c in aux)
            {
                int idEmpresa = SafeInt(c, 0);
                string razonsocial = SafeString(c, 1);
                string dni = SafeString(c, 2);
                string descripcion = SafeString(c, 3);
                int telefono = SafeInt(c, 4);
                string direccion = SafeString(c, 5);
                Empresa empresa = new Empresa(idEmpresa, razonsocial, dni, descripcion, telefono, direccion);
                lista.Add(empresa);
            }
            return lista;
        }
        private static int SafeInt(List<object> row, int index)
        {
            if (row == null || index >= row.Count) return 0;
            object v = row[index];
            if (v == null || v == DBNull.Value) return 0;
            if (int.TryParse(v.ToString(), out int r)) return r;
            return 0;
        }

        private static string SafeString(List<object> row, int index)
        {
            if (row == null || index >= row.Count) return string.Empty;
            object v = row[index];
            return v == null || v == DBNull.Value ? string.Empty : v.ToString();
        }
    }
}
