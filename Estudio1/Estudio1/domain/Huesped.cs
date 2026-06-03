namespace Estudio1.domain
{
    public class Huesped
    {
        public int idHuesped { get; set; }
        public string nombre { get; set; }
        public int dniPasaporte { get; set; }
        public int telefono { get; set; }
        public int idTipoVehiculo { get; set; }
        public string matricula { get; set; }

        public Huesped(int idHuesped, string nombre, int dniPasaporte, int telefono, int idTipoVehiculo, string matricula)
        {
            this.idHuesped = idHuesped;
            this.nombre = nombre;
            this.dniPasaporte = dniPasaporte;
            this.telefono = telefono;
            this.idTipoVehiculo = idTipoVehiculo;
            this.matricula = matricula;
        }
    }
}