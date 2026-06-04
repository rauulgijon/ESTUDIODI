
using System;
using System.Collections.Generic;
using minihito.Persistence;
namespace minihito.persistence
{
    internal class GrupoPersistence
    {

        public List<Grupo> LeerGrupos()
        {
            List<Grupo> lista = new List<Grupo>();
            List<Object> aux = DBBroker.obtenerAgente().leer(
                "SELECT idGrupo, nombre FROM aceptasreto.grupo ORDER BY nombre;"
            );

            foreach (List<Object> c in aux)
            {
                Grupo grupo = new Grupo(Convert.ToInt32(c[0]), c[1].ToString());
                lista.Add(grupo);
            }
            return lista;
        }


        public void InsertarGrupo(Grupo grupo)
        {
            String sql = $"INSERT INTO aceptasreto.grupo (nombre) VALUES ('{grupo.Nombre}');";
            DBBroker.obtenerAgente().modificar(sql);
        }

       
        public void BorrarGrupo(Grupo grupo)
        {
            
            String sqlDesasignar = $"UPDATE aceptasreto.alumnado SET grupo = NULL WHERE grupo = {grupo.Id};";
            DBBroker.obtenerAgente().modificar(sqlDesasignar);

            
            String sql = $"DELETE FROM aceptasreto.grupo WHERE idGrupo = {grupo.Id};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        
        public void ModificarGrupo(Grupo grupo)
        {
            String sql = $"UPDATE aceptasreto.grupo SET nombre = '{grupo.Nombre}' WHERE idGrupo = {grupo.Id};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        
        public int ObtenerUltimoId()
        {
            List<Object> aux = DBBroker.obtenerAgente().leer(
                "SELECT MAX(idGrupo) FROM aceptasreto.grupo;"
            );

            foreach (List<Object> c in aux)
            {
                if (c[0] != DBNull.Value && c[0] != null)
                    return Convert.ToInt32(c[0]);
            }
            return 0;
        }
    }
}