using System;
using System.Data;
using System.Windows;
using TuProyecto.persistence;

namespace SimulacroFinal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // No te olvides de poner arriba los using necesarios:
        // using System.Data;
        // using TuNamespaceDondeEsteElDBBroker;
        // using TuNamespaceDondeEsteElInforme;

        private void btnInformeFinanciero_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Le decimos al visor que esta ventana es su dueña
                crViewer.Owner = this;

                // 1. LA CONSULTA SQL (El JOIN triple)
                // OJO: Comprueba que tus campos en MySQL se llamen exactamente así (idGuest, idParcel, etc.)
                string sql = @"
            SELECT g.nombre as nombreGuest, p.tamañoParcela as numParcela, r.fechaEntrada as fechaEntrada, r.costeTotal as precio 
            FROM reserva r 
            INNER JOIN huesped g ON r.idHuesped = g.idHuesped 
            INNER JOIN parcela p ON r.idParcela = p.idParcela";

                // 2. PEDIMOS LOS DATOS AL MOTOR
                DataTable datos = DBBroker.obtenerAgente().LeerParaReporte(sql);

                // Si la base de datos está vacía, cortamos por lo sano
                if (datos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay reservas registradas en el sistema.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // 3. ¡EL PASO CRÍTICO! EL NOMBRE EXACTO DEL DATATABLE DEL .XSD
                datos.TableName = "ReporteFinanciero";

                // 4. EL BUCLE PARA SUMAR EL DINERO (Cálculo en C#)
                decimal totalGanado = 0;
                foreach (DataRow fila in datos.Rows)
                {
                    // Siempre blindamos comprobando que no sea nulo en la BD
                    if (fila["precio"] != DBNull.Value)
                    {
                        totalGanado += Convert.ToDecimal(fila["precio"]);
                    }
                }

                // 5. LA CONDICIÓN DEL "JEFE FINAL" (El reto)
                if (totalGanado > 10000)
                {
                    MessageBox.Show("¡Objetivo mensual cumplido! El camping va viento en popa.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

                // 6. INYECTAR LOS DATOS AL INFORME
                InformeFinanciero miInforme = new InformeFinanciero();
                miInforme.SetDataSource(datos);

                // "TotalGanado" DEBE ser idéntico al nombre del parámetro que creaste en Crystal Reports
                miInforme.SetParameterValue("TotalGanado", totalGanado);

                // Lo mostramos en pantalla
                crViewer.ViewerCore.ReportSource = miInforme;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe: \n" + ex.Message, "Error crítico", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

}
