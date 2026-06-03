using System;
using System.Collections.Generic;
using Estudio1.domain;

namespace Estudio1.persistence
{
    public class ParcelaPersistence
    {
        public static List<Parcela> leerParcelas()
        {
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT idParcela, tamañoParcela, luz, agua, precioNoche FROM parcela;");
            List<Parcela> parcelas = new List<Parcela>();

            foreach (List<Object> fila in aux)
            {
                Parcela p = new Parcela(Convert.ToInt32(fila[0]), fila[1].ToString(), Convert.ToInt32(fila[2]), Convert.ToInt32(fila[3]), float.Parse(fila[4].ToString()));
                parcelas.Add(p);
            }
            return parcelas;
        }

        public void insertarParcela(Parcela p)
        {
            string sql = $"INSERT INTO parcela (tamañoParcela, luz, agua, precioNoche) VALUES ('{p.tamanoParcela}', {p.luz}, {p.agua}, {p.precioNoche.ToString().Replace(',', '.')});";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarParcela(Parcela p)
        {
            string sql = $"UPDATE parcela SET tamañoParcela = '{p.tamanoParcela}', luz = {p.luz}, agua = {p.agua}, precioNoche = {p.precioNoche.ToString().Replace(',', '.')} WHERE idParcela = {p.idParcela};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarParcela(int id)
        {
            string sql = $"DELETE FROM parcela WHERE idParcela = {id};";
            DBBroker.obtenerAgente().modificar(sql);
        }
    }
}