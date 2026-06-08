using IronFit.persistence;
using IronFit.domain;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace IronFit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RecargarSocios();
            RecargarClases();
        }

        private void RecargarSocios()
        {
            dgSocios.ItemsSource = Socio.leerTodos();
        }

        private void RecargarClases()
        {
            dgClasesExtra.ItemsSource = ClaseExtra.leerTodas();
        }

        // =======================================================
        // SELECCIÓN EN LAS TABLAS
        // =======================================================
        private void dgSocios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSocios.SelectedItem != null)
            {
                Socio seleccionado = (Socio)dgSocios.SelectedItem;
                txtIdSocio.Text = seleccionado.IdSocio.ToString();
                txtNombre.Text = seleccionado.Nombre;
                txtDni.Text = seleccionado.Dni;
                txtCuotaMensual.Text = seleccionado.CuotaMensual.ToString();
            }
        }

        private void dgClases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClasesExtra.SelectedItem != null)
            {
                ClaseExtra seleccionada = (ClaseExtra)dgClasesExtra.SelectedItem;
                txtIdClase.Text = seleccionada.IdClase.ToString();
                txtIdSocioClase.Text = seleccionada.IdSocio.ToString();
                DatePickerFecha.SelectedDate = seleccionada.Fecha;
                txtNombreClase.Text = seleccionada.NombreClase;
                txtCoste.Text = seleccionada.Coste.ToString();
            }
        }

        // =======================================================
        // CRUD SOCIOS
        // =======================================================
        private void btnSocioAdd_Click(object sender, RoutedEventArgs e)
        {
            // 1. COMPROBAR CAMPOS VACÍOS
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDni.Text) ||
                string.IsNullOrWhiteSpace(txtCuotaMensual.Text))
            {
                MessageBox.Show("Por favor, rellena Nombre, DNI y Cuota Mensual.", "Faltan datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Corta la ejecución para que no explote
            }

            try
            {
                Socio s = new Socio(
                    0,
                    txtNombre.Text,
                    txtDni.Text,
                    Convert.ToDouble(txtCuotaMensual.Text)
                );

                s.insertar();
                RecargarSocios();
                btnSocioClear_Click(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Asegúrate de que la Cuota sea un número válido.", "Datos incorrectos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSocioUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdSocio.Text)) return;

            // 1. COMPROBAR CAMPOS VACÍOS
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDni.Text) ||
                string.IsNullOrWhiteSpace(txtCuotaMensual.Text))
            {
                MessageBox.Show("Por favor, no dejes campos vacíos al modificar.", "Faltan datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Socio s = new Socio(
                    Convert.ToInt32(txtIdSocio.Text),
                    txtNombre.Text,
                    txtDni.Text,
                    Convert.ToDouble(txtCuotaMensual.Text)
                );

                s.modificar();
                RecargarSocios();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Asegúrate de que la Cuota sea un número válido.", "Datos incorrectos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSocioDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdSocio.Text)) return;

            Socio s = new Socio { IdSocio = Convert.ToInt32(txtIdSocio.Text) };
            s.borrar();

            RecargarSocios();
            RecargarClases();
            btnSocioClear_Click(null, null);
        }

        private void btnSocioClear_Click(object sender, RoutedEventArgs e)
        {
            txtIdSocio.Text = "";
            txtNombre.Text = "";
            txtDni.Text = "";
            txtCuotaMensual.Text = "";
            dgSocios.SelectedItem = null;
        }

        // =======================================================
        // CRUD CLASES EXTRA
        // =======================================================
        private void btnClaseExtraAdd_Click(object sender, RoutedEventArgs e)
        {
            // 1. COMPROBAR CAMPOS VACÍOS (Incluyendo que haya una fecha seleccionada)
            if (string.IsNullOrWhiteSpace(txtIdSocioClase.Text) ||
                string.IsNullOrWhiteSpace(txtNombreClase.Text) ||
                string.IsNullOrWhiteSpace(txtCoste.Text) ||
                DatePickerFecha.SelectedDate == null)
            {
                MessageBox.Show("Por favor, rellena todos los datos y selecciona una Fecha.", "Faltan datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ClaseExtra c = new ClaseExtra(
                    0,
                    Convert.ToInt32(txtIdSocioClase.Text),
                    DatePickerFecha.SelectedDate.Value, // Como ya comprobamos que no es null, sacar el .Value es seguro
                    txtNombreClase.Text,
                    Convert.ToDouble(txtCoste.Text)
                );

                c.insertar();
                RecargarClases();
                btnClaseExtraClear_Click(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Revisa que el ID del Socio y el Coste sean números correctos.", "Datos incorrectos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClaseExtraUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdClase.Text)) return;

            // 1. COMPROBAR CAMPOS VACÍOS
            if (string.IsNullOrWhiteSpace(txtIdSocioClase.Text) ||
                string.IsNullOrWhiteSpace(txtNombreClase.Text) ||
                string.IsNullOrWhiteSpace(txtCoste.Text) ||
                DatePickerFecha.SelectedDate == null)
            {
                MessageBox.Show("Por favor, no dejes campos vacíos al modificar.", "Faltan datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ClaseExtra c = new ClaseExtra(
                    Convert.ToInt32(txtIdClase.Text),
                    Convert.ToInt32(txtIdSocioClase.Text),
                    DatePickerFecha.SelectedDate.Value,
                    txtNombreClase.Text,
                    Convert.ToDouble(txtCoste.Text)
                );

                c.modificar();
                RecargarClases();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Revisa que el ID del Socio y el Coste sean números correctos.", "Datos incorrectos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClaseExtraDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdClase.Text)) return;

            ClaseExtra c = new ClaseExtra { IdClase = Convert.ToInt32(txtIdClase.Text) };
            c.borrar();

            RecargarClases();
            btnClaseExtraClear_Click(null, null);
        }

        private void btnClaseExtraClear_Click(object sender, RoutedEventArgs e)
        {
            txtIdClase.Text = "";
            txtIdSocioClase.Text = "";
            DatePickerFecha.SelectedDate = null;
            txtNombreClase.Text = "";
            txtCoste.Text = "";
            dgClasesExtra.SelectedItem = null;
        }

        // =======================================================
        // INFORME CRYSTAL REPORTS
        // =======================================================
        private void btnGenerarFactura_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtidSocio.Text))
            {
                MessageBox.Show("Por favor, introduce el ID del socio.");
                return;
            }

            try
            {
                crViewer.Owner = this;

                string sql = "SELECT s.nombre as nombre, s.dni as dni, s.cuotaMensual as cuotaBase, " +
                             "c.fecha as fecha, c.nombreClase as nombreClase, c.coste as coste " +
                             "FROM socio s " +
                             "INNER JOIN claseextra c ON s.idSocio = c.idSocio " +
                             "WHERE s.idSocio = " + txtidSocio.Text;

                DataTable datos = DBBroker.obtenerAgente().LeerParaReporte(sql);

                if (datos.Rows.Count == 0)
                {
                    MessageBox.Show("Este socio no existe o no tiene clases extra este mes.");
                    return;
                }

                datos.TableName = "Tabla"; // El nombre de tu DataTable en el xsd

                decimal sumaClases = 0;
                decimal cuotaBase = Convert.ToDecimal(datos.Rows[0]["cuotaBase"]);

                foreach (DataRow fila in datos.Rows)
                {
                    if (fila["coste"] != DBNull.Value)
                    {
                        sumaClases += Convert.ToDecimal(fila["coste"]);
                    }
                }

                decimal totalAPagar = cuotaBase + sumaClases;

                InformeGimnasio miRecibo = new InformeGimnasio();
                miRecibo.SetDataSource(datos);
                miRecibo.SetParameterValue("costeTotal", totalAPagar); // Asegúrate de que se llame así en el rpt
                crViewer.ViewerCore.ReportSource = miRecibo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }
    }
}