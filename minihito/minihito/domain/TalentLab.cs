using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minihito.domain.talentlab
{
    public class TalentLab
    {
        public int Id_Talentlab { get; set; }
        public int Reto1 { get; set; }
        public int? Reto2 { get; set; }
        public int? Reto3 { get; set; }
        public int Empresa { get; set; }
        public int Grupo { get; set; }
        public string Titulo_descriptivo { get; set; }
        public string Descripcion { get; set; }

        public double Coste { get; set; }

        public string NombreReto1 { get; set; }
        public string NombreReto2 { get; set; }
        public string NombreReto3 { get; set; }

        public string NombreEmpresa { get; set; }
        public string NombreGrupo { get; set; }

        public TalentLabPersistence pm;

        public TalentLab()
        {
            pm = new TalentLabPersistence();
        }

        public void insertar() => pm.InsertarTalent(this);
        public void modificar() => pm.ModificarTalent(this);
        public void borrar() => pm.BorrarTalent(this);
        public List<TalentLab> getLista() => pm.LeerTodo();
    }
}