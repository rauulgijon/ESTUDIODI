using ExamenManuelDI2Recuperacion.controller; 
using ExamenManuelDI2Recuperacion.model;
using ExamenManuelDI2Recuperacion.view;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ExamenManuelDI2Recuperacion
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
            dgGuests.ItemsSource = GuestPersistence.leerGuests();
            dgParcels.ItemsSource = ParcelPersistence.leerParcels();
            dgReservations.ItemsSource = ReservationPersistence.leerReservations();
            dgVehicleTypes.ItemsSource = VehicleTypePersistence.leerVehicleTypes();
        }
        
        #region EVENTOS DE SELECCIÓN EN DATAGRIDS (Para rellenar los formularios)

        private void dgGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgGuests.SelectedItem is Guest g)
            {
                txtGuestId.Text = g.IdGuest.ToString();
                txtGuestName.Text = g.Name;
                txtGuestPassport.Text = g.Passport.ToString();
                txtGuestTelephone.Text = g.Telephone.ToString();
                txtGuestVehicleId.Text = g.VehicleId.ToString();
                txtGuestLicensePlate.Text = g.LicencePlate;
            }
        }

        private void dgParcels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgParcels.SelectedItem is Parcel p)
            {
                txtParcelId.Text = p.IdParcel.ToString();
                txtParcelSize.Text = p.ParcelSize;
                chkParcelLight.IsChecked = p.Light == 1; 
                chkParcelWater.IsChecked = p.Water == 1;
                txtParcelCol.Text = p.Parcelcol.ToString();
            }
        }

        private void dgReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReservations.SelectedItem is Reservation r)
            {
                txtResId.Text = r.IdReservation.ToString();
                txtResGuestId.Text = r.GuestId.ToString();
                txtResParcelId.Text = r.ParcelId.ToString();
                dpResCheckIn.SelectedDate = DateTime.Parse(r.CheckInDate);
                dpResCheckOut.SelectedDate = DateTime.Parse(r.CheckOutDate);
                txtResTotalCost.Text = r.TotalCost.ToString();
                txtResStatus.Text = r.Status;
            }
        }

        private void dgVehicleTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVehicleTypes.SelectedItem is VehicleType v)
            {
                txtVehId.Text = v.IdVehicleType.ToString();
                txtVehType.Text = v.Type;
            }
        }

        #endregion

        #region CRUD GUEST

        private void btnGuestAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest g = new Guest(0, txtGuestName.Text, int.Parse(txtGuestPassport.Text), int.Parse(txtGuestTelephone.Text), int.Parse(txtGuestVehicleId.Text), txtGuestLicensePlate.Text);
                new GuestPersistence().insertarGuest(g);
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
                Guest g = new Guest(int.Parse(txtGuestId.Text), txtGuestName.Text, int.Parse(txtGuestPassport.Text), int.Parse(txtGuestTelephone.Text), int.Parse(txtGuestVehicleId.Text), txtGuestLicensePlate.Text);
                new GuestPersistence().actualizarGuest(g);
                CargarDatos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnGuestDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGuestId.Text)) return;
                new GuestPersistence().eliminarGuest(int.Parse(txtGuestId.Text));
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

        #region CRUD PARCEL

        private void btnParcelAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int luz = chkParcelLight.IsChecked == true ? 1 : 0;
                int agua = chkParcelWater.IsChecked == true ? 1 : 0;
                Parcel p = new Parcel(0, txtParcelSize.Text, luz, agua, float.Parse(txtParcelCol.Text));
                new ParcelPersistence().insertarParcel(p);
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
                Parcel p = new Parcel(int.Parse(txtParcelId.Text), txtParcelSize.Text, luz, agua, float.Parse(txtParcelCol.Text));
                new ParcelPersistence().actualizarParcel(p);
                CargarDatos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnParcelDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtParcelId.Text)) return;
                new ParcelPersistence().eliminarParcel(int.Parse(txtParcelId.Text));
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

        #region CRUD RESERVATION

        private void btnResAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string checkIn = dpResCheckIn.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd");
                string checkOut = dpResCheckOut.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                Reservation r = new Reservation(0, int.Parse(txtResGuestId.Text), int.Parse(txtResParcelId.Text), checkIn, checkOut, float.Parse(txtResTotalCost.Text), txtResStatus.Text);
                new ReservationPersistence().insertarReservation(r);
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

                Reservation r = new Reservation(int.Parse(txtResId.Text), int.Parse(txtResGuestId.Text), int.Parse(txtResParcelId.Text), checkIn, checkOut, float.Parse(txtResTotalCost.Text), txtResStatus.Text);
                new ReservationPersistence().actualizarReservation(r);
                CargarDatos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnResDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtResId.Text)) return;
                new ReservationPersistence().eliminarReservation(int.Parse(txtResId.Text));
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

        #region CRUD VEHICLE TYPE

        private void btnVehAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VehicleType v = new VehicleType(0, txtVehType.Text);
                new VehicleTypePersistence().insertarVehicleType(v);
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
                VehicleType v = new VehicleType(int.Parse(txtVehId.Text), txtVehType.Text);
                new VehicleTypePersistence().actualizarVehicleType(v);
                CargarDatos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnVehDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVehId.Text)) return;
                new VehicleTypePersistence().eliminarVehicleType(int.Parse(txtVehId.Text));
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

        private void GenerateReport(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbReservasMes.IsChecked == true)
                {
                    string sql = @"
                SELECT g.name AS GuestName, g.licencePlate AS LicencePlate, 
                       v.vehicleType AS VehicleType, r.ParcelID, r.checkInDate AS CheckInDate 
                FROM reservation r
                JOIN guest g ON r.guestID = g.idGUEST
                JOIN vehicletype v ON g.vehicleID = v.idvehicleType
                ORDER BY r.checkInDate";

                    System.Data.DataTable datos = DBBroker.obtenerAgente().LeerParaReporte(sql);
                    datos.TableName = "ReporteReservasMes";
                    InformeReservas miReporte = new InformeReservas();
                    miReporte.SetDataSource(datos);

                    crViewer.ViewerCore.ReportSource = miReporte;
                }
                else if (rbReporteFinanciero.IsChecked == true)
                {
                    string sql = "SELECT checkInDate AS CheckInDate, totalCost AS TotalCost FROM reservation ORDER BY checkInDate";

                    System.Data.DataTable datos = DBBroker.obtenerAgente().LeerParaReporte(sql);
                    datos.TableName = "ReporteFinanciero";
                    InformeFinanciero miReporte = new InformeFinanciero();
                    miReporte.SetDataSource(datos);

                    crViewer.ViewerCore.ReportSource = miReporte;
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;

                if (ex.InnerException != null)
                {
                    mensajeError += "\n\nError en:\n" + ex.InnerException.Message;
                }

                MessageBox.Show("Error al generar el reporte:\n" + mensajeError);
            }
        }
    }
}