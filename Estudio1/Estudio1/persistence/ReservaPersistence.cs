using System;
using System.Collections.Generic;
using Estudio1.domain;

namespace Estudio1.persistence
{
    public class ReservaPersistence
    {
        public static List<Reserva> leerReservas()
        {
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT idReserva, idHuesped, idParcela, fechaEntrada, fechaSalida, costeTotal, estado FROM reserva;");
            List<Reserva> reservas = new List<Reserva>();

            foreach (List<Object> fila in aux)
            {
                Reserva r = new Reserva(Convert.ToInt32(fila[0]), Convert.ToInt32(fila[1]), Convert.ToInt32(fila[2]), fila[3].ToString().Split(' ')[0], fila[4].ToString().Split(' ')[0], float.Parse(fila[5].ToString()), fila[6].ToString());
                reservas.Add(r);
            }
            return reservas;
        }

        public void insertarReserva(Reserva r)
        {
            string sql = $"INSERT INTO reserva (idHuesped, idParcela, fechaEntrada, fechaSalida, costeTotal, estado) VALUES ({r.idHuesped}, {r.idParcela}, '{r.fechaEntrada}', '{r.fechaSalida}', {r.costeTotal.ToString().Replace(',', '.')}, '{r.estado}');";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void actualizarReserva(Reserva r)
        {
            string sql = $"UPDATE reserva SET idHuesped = {r.idHuesped}, idParcela = {r.idParcela}, fechaEntrada = '{r.fechaEntrada}', fechaSalida = '{r.fechaSalida}', costeTotal = {r.costeTotal.ToString().Replace(',', '.')}, estado = '{r.estado}' WHERE idReserva = {r.idReserva};";
            DBBroker.obtenerAgente().modificar(sql);
        }

        public void eliminarReserva(int id)
        {
            string sql = $"DELETE FROM reserva WHERE idReserva = {id};";
            DBBroker.obtenerAgente().modificar(sql);
        }
    }
}