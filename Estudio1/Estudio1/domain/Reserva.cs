namespace Estudio1.domain
{
    public class Reserva
    {
        public int idReserva { get; set; }
        public int idHuesped { get; set; }
        public int idParcela { get; set; }
        public string fechaEntrada { get; set; }
        public string fechaSalida { get; set; }
        public float costeTotal { get; set; }
        public string estado { get; set; }

        public Reserva(int idReserva, int idHuesped, int idParcela, string fechaEntrada, string fechaSalida, float costeTotal, string estado)
        {
            this.idReserva = idReserva;
            this.idHuesped = idHuesped;
            this.idParcela = idParcela;
            this.fechaEntrada = fechaEntrada;
            this.fechaSalida = fechaSalida;
            this.costeTotal = costeTotal;
            this.estado = estado;
        }
    }
}