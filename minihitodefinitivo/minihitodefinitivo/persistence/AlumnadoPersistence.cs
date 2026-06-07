using System;
using System.Collections.Generic;
using System.Data;
using minihitodefinitivo.domain;

namespace minihitodefinitivo.persistence
{
    internal class AlumnadoPersistence
    {
        public List<Alumnado> LeerAlumno()
        {
            List<Alumnado> lista = new List<Alumnado>();
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT idAlumnado, nombre, apellidos, especialidad, grupo FROM aceptasreto.alumnado;");
            foreach (List<Object> c in aux)
            {
                int id = SafeInt(c, 0);
                string nombre = SafeString(c, 1);
                string apellidos = SafeString(c, 2);
                int especialidad = SafeInt(c, 3);
                int idGrupo = SafeInt(c, 4);
                Alumnado alumno = new Alumnado(id, nombre, apellidos, especialidad, idGrupo);
                lista.Add(alumno);
            }
            return lista;
        }

        public List<Alumnado> LeerAlumnos()
        {
            List<Alumnado> lista = new List<Alumnado>();
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT idAlumnado, nombre, apellidos, especialidad, grupo FROM aceptasreto.alumnado ORDER BY nombre;");
            foreach (List<Object> c in aux)
            {
                int id = SafeInt(c, 0);
                string nombre = SafeString(c, 1);
                string apellidos = SafeString(c, 2);
                int especialidad = SafeInt(c, 3);
                int idGrupo = SafeInt(c, 4);
                Alumnado alumno = new Alumnado(id, nombre, apellidos, especialidad, idGrupo);
                lista.Add(alumno);
            }
            return lista;
        }

        public List<Alumnado> LeerAlumnosSinGrupo()
        {
            List<Alumnado> lista = new List<Alumnado>();
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT idAlumnado, nombre, apellidos, especialidad, grupo FROM aceptasreto.alumnado WHERE grupo IS NULL ORDER BY nombre;");
            foreach (List<Object> c in aux)
            {
                int id = SafeInt(c, 0);
                string nombre = SafeString(c, 1);
                string apellidos = SafeString(c, 2);
                int especialidad = SafeInt(c, 3);
                int idGrupo = SafeInt(c, 4);
                Alumnado alumno = new Alumnado(id, nombre, apellidos, especialidad, idGrupo);
                lista.Add(alumno);
            }
            return lista;
        }

        public List<Alumnado> LeerAlumnosPorGrupo(int idGrupo)
        {
            List<Alumnado> lista = new List<Alumnado>();
            List<Object> aux = DBBroker.obtenerAgente().leer($"SELECT idAlumnado, nombre, apellidos, especialidad, grupo FROM aceptasreto.alumnado WHERE grupo = {idGrupo} ORDER BY nombre;");
            foreach (List<Object> c in aux)
            {
                int id = SafeInt(c, 0);
                string nombre = SafeString(c, 1);
                string apellidos = SafeString(c, 2);
                int especialidad = SafeInt(c, 3);
                int idGrupo2 = SafeInt(c, 4);
                Alumnado alumno = new Alumnado(id, nombre, apellidos, especialidad, idGrupo2);
                lista.Add(alumno);
            }
            return lista;
        }

        public void InsertarAlumno(Alumnado alumno)
        {
            String sql = $"INSERT INTO aceptasreto.alumnado (nombre, apellidos, especialidad, grupo) VALUES ('{alumno.Nombre}', '{alumno.Apellidos}', {alumno.Especialidad}, NULL);";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void BorrarAlumno(Alumnado alumno)
        {
            String sql = $"DELETE FROM aceptasreto.alumnado WHERE idAlumnado = {alumno.Id};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void ModificarAlumno(Alumnado alumno)
        {
            String sql = $"UPDATE aceptasreto.alumnado SET nombre = '{alumno.Nombre}', apellidos = '{alumno.Apellidos}', especialidad = {alumno.Especialidad} WHERE idAlumnado = {alumno.Id};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void AsignarGrupo(Alumnado alumno)
        {
            String sql = $"UPDATE aceptasreto.alumnado SET grupo = {alumno.Grupo} WHERE idAlumnado = {alumno.Id};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void DesasignarGrupo(Alumnado alumno)
        {
            String sql = $"UPDATE aceptasreto.alumnado SET grupo = NULL WHERE idAlumnado = {alumno.Id};";
            DBBroker.obtenerAgente().modificar(sql);
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