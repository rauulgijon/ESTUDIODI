using ExamenVeterinaria.persistence;
using System;
using System.Data;
using System.Windows;

namespace ExamenVeterinaria
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerarHistorial_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdMascota.Text))
            {
                MessageBox.Show("Por favor, introduce el ID de la mascota.");
                return;
            }

            try
            {
                crViewer.Owner = this;

                // ==========================================
                // 🚀 TU EXAMEN EMPIEZA AQUÍ
                // ==========================================

                // PASO 1: Consulta SQL (JOIN entre Mascota y Tratamiento filtrado por txtIdMascota.Text)
                string sql = "select m.nombre as nombreMascota, m.especie as especie, t.fecha as fechaTratamiento, t.descripcion as descripcion, t.coste as costeTratamiento " +
             "from mascota m " +
             "inner join tratamiento t on t.idMascota = m.idMascota " +
             "where m.idMascota = " + txtIdMascota.Text;

                // PASO 2: DBBroker y DataTable
                DataTable datos = DBBroker.obtenerAgente().LeerParaReporte(sql);
                if (datos.Rows.Count == 0)
                {
                    MessageBox.Show("No se han encontrado tratamientos para esta mascota.");
                    return;
                }
                datos.TableName = "DatosHistorial";

                // PASO 3: Foreach para sumar el coste de los tratamientos
                decimal totalGastado = 0;
                foreach (DataRow dr in datos.Rows)
                {
                    if (dr["costeTratamiento"] != DBNull.Value)
                    {
                        totalGastado += Convert.ToDecimal(dr["costeTratamiento"]);
                    }
                }


                // PASO 4: Instanciar el informe, inyectar el DataTable y el parámetro
                InformeHistorial informe = new InformeHistorial();
                informe.SetDataSource(datos);
                informe.SetParameterValue("TotalGastado", totalGastado);

                // PASO 5: Mostrar en el crViewer
                crViewer.ViewerCore.ReportSource = informe;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe:\n" + ex.Message);
            }
        }
    }
}