namespace Estudio1.domain
{
    public class TipoVehiculo
    {
        public int idTipoVehiculo { get; set; }
        public string tipoVehiculoNombre { get; set; }

        public TipoVehiculo(int idTipoVehiculo, string tipoVehiculoNombre)
        {
            this.idTipoVehiculo = idTipoVehiculo;
            this.tipoVehiculoNombre = tipoVehiculoNombre;
        }
    }
}