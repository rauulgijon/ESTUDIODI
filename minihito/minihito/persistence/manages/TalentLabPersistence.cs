using minihito.Persistence;
using minihito.domain.talentlab;
using System;
using System.Collections.Generic;
using System.Data;

namespace minihito.domain
{
    public class TalentLabPersistence
    {
    
        public List<TalentLab> LeerTodo()
        {
            List<TalentLab> lista = new List<TalentLab>();

            string sql = @"
                SELECT 
                    t.idtalent_lab, 
                    t.reto1, r1.descripcion_reto,
                    t.reto2, r2.descripcion_reto,
                    t.reto3, r3.descripcion_reto,
                    t.empresa, e.razonsocial,
                    t.grupo, g.nombre,
                    t.titulo_descriptivo, 
                    t.descripcion,
                    t.coste
                FROM talent_lab t
                INNER JOIN reto r1 ON t.reto1 = r1.id_reto
                LEFT JOIN reto r2 ON t.reto2 = r2.id_reto
                LEFT JOIN reto r3 ON t.reto3 = r3.id_reto
                INNER JOIN empresa e ON t.empresa = e.idEmpresa
                INNER JOIN grupo g ON t.grupo = g.idgrupo;";

            List<Object> aux = DBBroker.obtenerAgente().leer(sql);

            foreach (List<Object> c in aux)
            {
                TalentLab t = new TalentLab();
                t.Id_Talentlab = Convert.ToInt32(c[0]);
                t.Reto1 = Convert.ToInt32(c[1]);
                t.NombreReto1 = c[2]?.ToString();

                t.Reto2 = c[3] != DBNull.Value ? (int?)Convert.ToInt32(c[3]) : null;
                t.NombreReto2 = c[4]?.ToString();

                t.Reto3 = c[5] != DBNull.Value ? (int?)Convert.ToInt32(c[5]) : null;
                t.NombreReto3 = c[6]?.ToString();

                t.Empresa = Convert.ToInt32(c[7]);
                t.NombreEmpresa = c[8]?.ToString();

                t.Grupo = Convert.ToInt32(c[9]);
                t.NombreGrupo = c[10]?.ToString();

                t.Titulo_descriptivo = c[11]?.ToString();
                t.Descripcion = c[12]?.ToString();

                t.Coste = c[13] != DBNull.Value ? Convert.ToDouble(c[13]) : 0;

                lista.Add(t);
            }
            return lista;
        }

       
        public void InsertarTalent(TalentLab t)
        {
            string r2 = t.Reto2.HasValue ? t.Reto2.ToString() : "NULL";
            string r3 = t.Reto3.HasValue ? t.Reto3.ToString() : "NULL";


            string sql = $"INSERT INTO talent_lab (reto1, reto2, reto3, empresa, grupo, titulo_descriptivo, descripcion, coste) " +
                 $"VALUES ({t.Reto1}, {r2}, {r3}, {t.Empresa}, {t.Grupo}, '{t.Titulo_descriptivo}', '{t.Descripcion}', {t.Coste});";

            DBBroker.obtenerAgente().modificar(sql);
        }

   
        public void ModificarTalent(TalentLab t)
        {
            string r2 = t.Reto2.HasValue ? t.Reto2.ToString() : "NULL";
            string r3 = t.Reto3.HasValue ? t.Reto3.ToString() : "NULL";

            string sql = $"UPDATE talent_lab SET " +
                         $"reto1 = {t.Reto1}, " +
                         $"reto2 = {r2}, " +
                         $"reto3 = {r3}, " +
                         $"empresa = {t.Empresa}, " +
                         $"grupo = {t.Grupo}, " +
                         $"titulo_descriptivo = '{t.Titulo_descriptivo}', " +
                         $"coste = {t.Coste} "+
                         $"descripcion = '{t.Descripcion}' " +
                         $"WHERE idtalent_lab = {t.Id_Talentlab};";

            DBBroker.obtenerAgente().modificar(sql);
        }

       
        public void BorrarTalent(TalentLab t)
        {
            string sql = $"DELETE FROM talent_lab WHERE idtalent_lab = {t.Id_Talentlab};";
            DBBroker.obtenerAgente().modificar(sql);
        }
    }
}