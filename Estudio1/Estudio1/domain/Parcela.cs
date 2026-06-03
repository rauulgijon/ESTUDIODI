namespace Estudio1.domain
{
    public class Parcela
    {
        public int idParcela { get; set; }
        public string tamanoParcela { get; set; }
        public int luz { get; set; }
        public int agua { get; set; }
        public float precioNoche { get; set; }

        public Parcela(int idParcela, string tamanoParcela, int luz, int agua, float precioNoche)
        {
            this.idParcela = idParcela;
            this.tamanoParcela = tamanoParcela;
            this.luz = luz;
            this.agua = agua;
            this.precioNoche = precioNoche;
        }
    }
}