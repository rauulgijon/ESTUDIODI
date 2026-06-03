using Estudio1.domain;
using Estudio1.persistence;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Estudio1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            dgGuests.ItemsSource = HuespedPersistence.leerHuespedes();
            dgParcels.ItemsSource = ParcelaPersistence.leerParcelas();
            dgReservations.ItemsSource = ReservaPersistence.leerReservas();
            dgVehicleTypes.ItemsSource = TipoVehiculoPersistence.leerTiposVehiculo();
        }

        #region EVENTOS DE SELECCIÓN EN DATAGRIDS

        private void dgGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgGuests.SelectedItem is Huesped h)
            {
                txtGuestId.Text = h.idHuesped.ToString();
                txtGuestName.Text = h.nombre;
                txtGuestPassport.Text = h.dniPasaporte.ToString();
                txtGuestTelephone.Text = h.telefono.ToString();
                txtGuestVehicleId.Text = h.idTipoVehiculo.ToString();
                txtGuestLicensePlate.Text = h.matricula;
            }
        }

        private void dgParcels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgParcels.SelectedItem is Parcela p)
            {
                txtParcelId.Text = p.idParcela.ToString();
                txtParcelSize.Text = p.tamanoParcela;
                chkParcelLight.IsChecked = p.luz == 1;
                chkParcelWater.IsChecked = p.agua == 1;
                txtParcelCol.Text = p.precioNoche.ToString();
            }
        }

        private void dgReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReservations.SelectedItem is Reserva r)
            {
                txtResId.Text = r.idReserva.ToString();
                txtResGuestId.Text = r.idHuesped.ToString();
                txtResParcelId.Text = r.idParcela.ToString();
                dpResCheckIn.SelectedDate = DateTime.Parse(r.fechaEntrada);
                dpResCheckOut.SelectedDate = DateTime.Parse(r.fechaSalida);
                txtResTotalCost.Text = r.costeTotal.ToString();
                txtResStatus.Text = r.estado;
            }
        }

        private void dgVehicleTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVehicleTypes.SelectedItem is TipoVehiculo v)
            {
                txtVehId.Text = v.idTipoVehiculo.ToString();
                txtVehType.Text = v.tipoVehiculoNombre;
            }
        }

        #endregion

        #region CRUD HUESPED

        private void btnGuestAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Huesped h = new Huesped(0, txtGuestName.Text, int.Parse(txtGuestPassport.Text), int.Parse(txtGuestTelephone.Text), int.Parse(txtGuestVehicleId.Text), txtGuestLicensePlate.Text);
                new HuespedPersistence().insertarHuesped(h);
                CargarDatos();
                btnGuestClear_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnGuestUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGuestId.Text)) return;
                Huesped h = new Huesped(int.Parse(txtGuestId.Text), txtGuestName.Text, int.Parse(txtGuestPassport.Text), int.Parse(txtGuestTelephone.Text), int.Parse(txtGuestVehicleId.Text), txtGuestLicensePlate.Text);
                new HuespedPersistence().actualizarHuesped(h);
                CargarDatos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnGuestDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGuestId.Text)) return;
                new HuespedPersistence().eliminarHuesped(int.Parse(txtGuestId.Text));
                CargarDatos();
                btnGuestClear_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnGuestClear_Click(object sender, RoutedEventArgs e)
        {
            txtGuestId.Text = ""; txtGuestName.Text = ""; txtGuestPassport.Text = "";
            txtGuestTelephone.Text = ""; txtGuestVehicleId.Text = ""; txtGuestLicensePlate.Text = "";
            dgGuests.SelectedItem = null;
        }

        #endregion

        #region CRUD PARCELA

        private void btnParcelAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int luz = chkParcelLight.IsChecked == true ? 1 : 0;
                int agua = chkParcelWater.IsChecked == true ? 1 : 0;
                Parcela p = new Parcela(0, txtParcelSize.Text, luz, agua, float.Parse(txtParcelCol.Text));
                new ParcelaPersistence().insertarParcela(p);
                CargarDatos();
                btnParcelClear_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnParcelUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtParcelId.Text)) return;
                int luz = chkParcelLight.IsChecked == true ? 1 : 0;
                int agua = chkParcelWater.IsChecked == true ? 1 : 0;
                Parcela p = new Parcela(int.Parse(txtParcelId.Text), txtParcelSize.Text, luz, agua, float.Parse(txtParcelCol.Text));
                new ParcelaPersistence().actualizarParcela(p);
                CargarDatos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnParcelDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtParcelId.Text)) return;
                new ParcelaPersistence().eliminarParcela(int.Parse(txtParcelId.Text));
                CargarDatos();
                btnParcelClear_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnParcelClear_Click(object sender, RoutedEventArgs e)
        {
            txtParcelId.Text = ""; txtParcelSize.Text = ""; txtParcelCol.Text = "";
            chkParcelLight.IsChecked = false; chkParcelWater.IsChecked = false;
            dgParcels.SelectedItem = null;
        }

        #endregion

        #region CRUD RESERVA

        private void btnResAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string checkIn = dpResCheckIn.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd");
                string checkOut = dpResCheckOut.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                Reserva r = new Reserva(0, int.Parse(txtResGuestId.Text), int.Parse(txtResParcelId.Text), checkIn, checkOut, float.Parse(txtResTotalCost.Text), txtResStatus.Text);
                new ReservaPersistence().insertarReserva(r);
                CargarDatos();
                btnResClear_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnResUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtResId.Text)) return;
                string checkIn = dpResCheckIn.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd");
                string checkOut = dpResCheckOut.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                Reserva r = new Reserva(int.Parse(txtResId.Text), int.Parse(txtResGuestId.Text), int.Parse(txtResParcelId.Text), checkIn, checkOut, float.Parse(txtResTotalCost.Text), txtResStatus.Text);
                new ReservaPersistence().actualizarReserva(r);
                CargarDatos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnResDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtResId.Text)) return;
                new ReservaPersistence().eliminarReserva(int.Parse(txtResId.Text));
                CargarDatos();
                btnResClear_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnResClear_Click(object sender, RoutedEventArgs e)
        {
            txtResId.Text = ""; txtResGuestId.Text = ""; txtResParcelId.Text = "";
            txtResTotalCost.Text = ""; txtResStatus.Text = "";
            dpResCheckIn.SelectedDate = null; dpResCheckOut.SelectedDate = null;
            dgReservations.SelectedItem = null;
        }

        #endregion

        #region CRUD TIPO VEHICULO

        private void btnVehAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TipoVehiculo v = new TipoVehiculo(0, txtVehType.Text);
                new TipoVehiculoPersistence().insertarTipoVehiculo(v);
                CargarDatos();
                btnVehClear_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnVehUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVehId.Text)) return;
                TipoVehiculo v = new TipoVehiculo(int.Parse(txtVehId.Text), txtVehType.Text);
                new TipoVehiculoPersistence().actualizarTipoVehiculo(v);
                CargarDatos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnVehDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVehId.Text)) return;
                new TipoVehiculoPersistence().eliminarTipoVehiculo(int.Parse(txtVehId.Text));
                CargarDatos();
                btnVehClear_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnVehClear_Click(object sender, RoutedEventArgs e)
        {
            txtVehId.Text = ""; txtVehType.Text = "";
            dgVehicleTypes.SelectedItem = null;
        }

        #endregion

        #region GENERACIÓN DE INFORMES

        private void GenerateReport(object sender, RoutedEventArgs e)
        {
            try
            {
                crViewer.Owner = this; // Evita el fallo de WindowInteropHelper

                if (rbReservasMes.IsChecked == true)
                {
                    /*
                     * // Los alias (AS ...) cuadran EXACTAMENTE con los nombres de tu DataSet
                    string sql = @"
                        SELECT g.nombre AS nombreHuesped, 
                               g.matricula AS matricula, 
                               v.tipoVehiculo AS tipoVehiculo, 
                               r.idParcela AS idParcela, 
                               r.fechaEntrada AS fechaEntrada 
                        FROM RESERVA r
                        JOIN HUESPED g ON r.idHuesped = g.idHuesped
                        JOIN TIPOVEHICULO v ON g.idTipoVehiculo = v.idTipoVehiculo
                        ORDER BY r.fechaEntrada";

                    System.Data.DataTable datos = DBBroker.obtenerAgente().LeerParaReporte(sql);

                    // El nombre tiene que ser idéntico al del archivo XSD
                    datos.TableName = "ReportereservasMes";

                    InformeReservas miReporte = new InformeReservas();
                    miReporte.SetDataSource(datos);

                    crViewer.ViewerCore.ReportSource = miReporte;
                    */
                }
                else if (rbReporteFinanciero.IsChecked == true)
                {
                    // 1. Calculamos el coste total exacto: (Días de diferencia) * (Precio por noche)
                    // Usamos DATEDIFF para sacar los días y hacemos un JOIN con PARCELA para tener el precio
                    string sql = @"
        SELECT r.fechaEntrada AS fechaEntrada, 
               (DATEDIFF(r.fechaSalida, r.fechaEntrada) * p.precioNoche) AS costeTotal 
        FROM RESERVA r
        JOIN PARCELA p ON r.idParcela = p.idParcela
        ORDER BY r.fechaEntrada";

                    System.Data.DataTable datos = DBBroker.obtenerAgente().LeerParaReporte(sql);
                    datos.TableName = "ReporteFinanciero";

                    // 2. Sumamos nosotros mismos el dinero en C# (ahora con el coste correcto)
                    decimal sumaCsharp = 0;
                    foreach (System.Data.DataRow fila in datos.Rows)
                    {
                        // Comprobamos que el cálculo no haya devuelto nulo
                        if (fila["costeTotal"] != DBNull.Value)
                        {
                            sumaCsharp += Convert.ToDecimal(fila["costeTotal"]);
                        }
                    }

                    // 3. Preparamos el informe
                    InformeFinanciero miReporteFinanciero = new InformeFinanciero();
                    miReporteFinanciero.SetDataSource(datos);

                    // 4. Inyectamos la suma total correcta al parámetro de Crystal Reports
                    miReporteFinanciero.SetParameterValue("SumaTotal", sumaCsharp);

                    // 5. Mostramos el informe
                    crViewer.ViewerCore.ReportSource = miReporteFinanciero;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte:\n" + ex.Message);
            }
        }

        #endregion
    }
}