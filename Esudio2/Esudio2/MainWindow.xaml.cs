using Estudio1.persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Esudio2
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

        private void btnGenerarFactura_Click(object sender, RoutedEventArgs e)
        {
            // 1. VALIDACIÓN (Ya te la doy hecha)
            if (string.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                MessageBox.Show("Por favor, introduce el ID del cliente antes de generar la factura.");
                return;
            }

            try
            {
                crViewer.Owner = this;
                // =================================================================================
                // 🚀 AQUÍ EMPIEZA TU EXAMEN: TU TURNO PARA PRACTICAR
                // =================================================================================

                // PASO 1: Escribe la consulta SQL cruzando las tablas Cliente y Reparacion, 
                // filtrando (WHERE) por el número que hay en txtIdCliente.Text
                string sql = $@" 
                    select c.nombre as nombre, c.dni as dni, r.fecha as fecha, r.descripcion as descripcion, r.coste as coste
                    from cliente c 
                    join reparacion r on c.idCliente = r.idCliente
                    where c.idCliente = {txtIdCliente.Text}
                ";

                // PASO 2: Usa el DBBroker para ejecutar la consulta y guárdalo en un DataTable
                // Recuerda ponerle al DataTable el nombre exacto que le des en tu archivo .xsd

                DataTable datos = DBBroker.obtenerAgente().LeerParaReporte(sql);

                if(datos.Rows.Count == 0)
                {
                    MessageBox.Show("No se han encontrado reparaciones para este clliente");
                    return;
                }

                datos.TableName = "DatosFactura";
                // PASO 3: Haz un foreach que recorra las filas del DataTable y vaya sumando el "coste"

                decimal totalFactura = 0;

                foreach (DataRow row in datos.Rows)
                {
                    if (row["coste"] != DBNull.Value)
                    {
                        totalFactura += Convert.ToDecimal(row["coste"]);
                    }
                }

                InformeFactura miFactura = new InformeFactura();
                miFactura.SetDataSource(datos);

                miFactura.SetParameterValue("sumatotal", totalFactura);
                crViewer.ViewerCore.ReportSource = miFactura;




                // PASO 4: Instancia tu informe de Crystal Reports (FacturaTaller), pásale el 
                // DataTable y pásale la variable totalFactura al parámetro del informe.


                // PASO 5: Carga el informe en el visor de la pantalla (crViewer)


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe:\n" + ex.Message);
            }
        }
    }
}
