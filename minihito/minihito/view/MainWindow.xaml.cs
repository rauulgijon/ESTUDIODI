
using minihito.Persistence;
using minihito.domain;
using minihito.domain.reto;
using minihito.domain.talentlab;
using minihito.persistence;
using minihito.crystalreport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace minihito
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Alumnado> lstAlumnado;
        private List<Empresa> lstEmpresa;
        private List<Reto> lstReto;
        private Alumnado alumno;
        private Empresa empresa;
        private Reto reto;
        private List<Alumnado> lstAlumnosNoAsignados;
        private List<Alumnado> lstAlumnosSeleccionados;
        private List<Grupo> lstGrupos;
        private Grupo grupoSeleccionado;
        private List<TalentLab> lstTalentLabs;
        private TalentLab talentLogica;


        public ObservableCollection<Empresa> Customers { get; set; } = new ObservableCollection<Empresa>();

        public MainWindow()
        {
            InitializeComponent();
            CargarInformeDesdeMySQL();


            this.DataContext = this;
            talentLogica = new TalentLab();
            empresa = new Empresa();
            alumno = new Alumnado();
            reto = new Reto();

            
            lstEmpresa = empresa.getListaEmpresa();
            lstAlumnado = alumno.getListaPersonas();
            lstReto = reto.getListaReto();

            
            Grupo tempGrupo = new Grupo();
            lstGrupos = tempGrupo.getGrupos();

         
            dgvPersonas.ItemsSource = lstAlumnado;
            dgvEmpresa.ItemsSource = lstEmpresa;
            dgvRetos.ItemsSource = lstReto;

           
            CargarDatosTalent();

           
            CargarDatos();

            start();
        }
        public void start()
        {
            nombretxt.Text = " ";
            apellidotxt.Text = " ";
            cursotxt.Text = " ";

        }
        private void CargarDatosTalent()
        {
           
            lstTalentLabs = talentLogica.getLista();
            dgvTalent.ItemsSource = null;
            dgvTalent.ItemsSource = lstTalentLabs;

            cmbReto1.ItemsSource = lstReto;
            cmbReto2.ItemsSource = lstReto;
            cmbReto3.ItemsSource = lstReto;
            cmbTalentEmpresa.ItemsSource = lstEmpresa;
            cmbTalentGrupo.ItemsSource = lstGrupos;
        }
        private void CargarDatosRetos()
        {
          
            lstReto = reto.getListaReto();

           
            dgvRetos.ItemsSource = null;
            dgvRetos.ItemsSource = lstReto;
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void dgvPersonas_selectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void dgvRetos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
   
            if (dgvRetos.SelectedItem is Reto seleccionado)
            {
       
                idRetotxt.Text = seleccionado.Id_Reto.ToString();
                descRetotxt.Text = seleccionado.Descripcion_reto;
            }
        }

        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            int curso;
            if (!int.TryParse(cursotxt.Text, out curso) || (curso != 1 && curso != 2))
            {
                MessageBox.Show("La especialidad debe ser 1 (SCI1) o 2 (SCI2). Introduce un valor válido.");
                return;
            }
            Alumnado alumno = new Alumnado(nombretxt.Text, apellidotxt.Text, int.Parse(cursotxt.Text));
            
            alumno.insertar();
            lstAlumnado.Add(alumno);
            dgvPersonas.Items.Refresh();
            start();

        }
        private void BuscarTexto_Click(object sender, RoutedEventArgs e)
        {

            string texto = buscartxt.Text.Trim();

            if (!string.IsNullOrEmpty(texto))
            {
                
                var listaFiltrada = lstAlumnado.Where(p =>
                    p.Nombre.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Apellidos.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                dgvPersonas.ItemsSource = listaFiltrada;
            }
            else
            {
                MessageBox.Show("Introduce un texto para buscar");
            }
        }


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            Alumnado alumno = new Alumnado(((Alumnado)dgvPersonas.SelectedItem).Id);
            alumno.borrar();
            lstAlumnado.Remove((Alumnado)dgvPersonas.SelectedItem);

            dgvPersonas.Items.Refresh();
            start();
        }

        private void btnModificar_click(Object sender, RoutedEventArgs e)
        {
           
            int curso;

            if (!int.TryParse(cursotxt.Text, out curso) || (curso != 1 && curso != 2))
            {
                MessageBox.Show("La especialidad debe ser 1 (SCI1) o 2 (SCI2). Introduce un valor válido.");
                return;
            }
            Alumnado alumno = (Alumnado)dgvPersonas.SelectedItem;
            alumno.Nombre = nombretxt.Text;
            alumno.Apellidos = apellidotxt.Text;
            alumno.Especialidad = int.Parse(cursotxt.Text);
            alumno.modificar();
            dgvPersonas.Items.Refresh();
            start();
        }
        private void btnAddEmpresa_click(object sender, RoutedEventArgs e)
        {
            Empresa empresa = new Empresa(razontxt.Text, dnitxt.Text,descripciontxt.Text, int.Parse(telefonotxt.Text),direcciontxt.Text);
            empresa.insertar();
            Customers.Add(empresa);
            lstEmpresa.Add(empresa);
            dgvEmpresa.Items.Refresh();
            start();

        }
        private void BuscarTextoEmpresa_Click(object sender, RoutedEventArgs e)
        {

            string texto = buscarEmpresatxt.Text.Trim();

            if (!string.IsNullOrEmpty(texto))
            {
               
                var listaFiltrada = lstEmpresa.Where(p =>
                    p.Razon.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Dni.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                dgvEmpresa.ItemsSource = listaFiltrada;
            }
            else
            {
                MessageBox.Show("Introduce un texto para buscar");
            }
        }
        private void btnEliminarEmpresa_Click(object sender, RoutedEventArgs e)
        {

            Empresa empresa = new Empresa(((Empresa)dgvEmpresa.SelectedItem).Id);
            empresa.borrar();
            lstEmpresa.Remove((Empresa)dgvEmpresa.SelectedItem);

            dgvEmpresa.Items.Refresh();
            start();
        }

        private void btnModificarEmpresa_click(Object sender, RoutedEventArgs e)
        {
            
            Empresa empresa = (Empresa)dgvEmpresa.SelectedItem;
            empresa.Razon = razontxt.Text;
            empresa.Dni = dnitxt.Text;
            empresa.Descripcion = descripciontxt.Text;
            empresa.Telefono = int.Parse(telefonotxt.Text);
            empresa.Direccion = direcciontxt.Text;
            empresa.modificar();
            dgvEmpresa.Items.Refresh();
            start();
        }
        private void dgvEmpresa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (dgvEmpresa.SelectedItem is Empresa seleccionada)
            {
           
                razontxt.Text = seleccionada.Razon;
                dnitxt.Text = seleccionada.Dni;
                descripciontxt.Text = seleccionada.Descripcion;
                telefonotxt.Text = seleccionada.Telefono.ToString();
                direcciontxt.Text = seleccionada.Direccion;
            }
        }
        private void CargarDatos()
        {
            
            Alumnado alumno = new Alumnado();
            lstAlumnosNoAsignados = alumno.pm.LeerAlumnosSinGrupo();
            ListUnassigned.ItemsSource = lstAlumnosNoAsignados;
            ListUnassigned.DisplayMemberPath = "NombreCompleto";

            
            lstAlumnosSeleccionados = new List<Alumnado>();
            ListSelected.ItemsSource = lstAlumnosSeleccionados;
            ListSelected.DisplayMemberPath = "NombreCompleto";

           
            Grupo grupo = new Grupo();
            lstGrupos = grupo.getGrupos();
            ListGroupMembers.ItemsSource = lstGrupos;
            ListGroupMembers.DisplayMemberPath = "Nombre";

            
            GroupNameTextBox.Text = "";
            grupoSeleccionado = null;
        }

        
        private void ListGroupMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListGroupMembers.SelectedItem != null)
            {
                Grupo grupoSelec = (Grupo)ListGroupMembers.SelectedItem;
                CargarGrupoParaEditar(grupoSelec);
            }
        }

        
        private void BtnMoverDerecha_Click(object sender, RoutedEventArgs e)
        {
            if (ListUnassigned.SelectedItems.Count > 0)
            {
                List<Alumnado> alumnosAMover = new List<Alumnado>();
                foreach (Alumnado alumno in ListUnassigned.SelectedItems)
                {
                    alumnosAMover.Add(alumno);
                }

                foreach (Alumnado alumno in alumnosAMover)
                {
                    lstAlumnosNoAsignados.Remove(alumno);
                    lstAlumnosSeleccionados.Add(alumno);
                }

                ListUnassigned.Items.Refresh();
                ListSelected.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Selecciona al menos un alumno para mover", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        
        private void BtnMoverIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (ListSelected.SelectedItems.Count > 0)
            {
                List<Alumnado> alumnosAMover = new List<Alumnado>();
                foreach (Alumnado alumno in ListSelected.SelectedItems)
                {
                    alumnosAMover.Add(alumno);
                }

                foreach (Alumnado alumno in alumnosAMover)
                {
                    lstAlumnosSeleccionados.Remove(alumno);
                    lstAlumnosNoAsignados.Add(alumno);
                }

                ListUnassigned.Items.Refresh();
                ListSelected.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Selecciona al menos un alumno para mover", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

   
        private void BtnAñadirModificar_Click(object sender, RoutedEventArgs e)
        {
            string nombreGrupo = GroupNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(nombreGrupo))
            {
                MessageBox.Show("Introduce un nombre para el grupo", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (lstAlumnosSeleccionados.Count == 0)
            {
                MessageBox.Show("Debes seleccionar al menos un alumno para el grupo", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int idGrupoActual;

                if (grupoSeleccionado == null)
                {
                
                    Grupo nuevoGrupo = new Grupo(nombreGrupo);
                    nuevoGrupo.insertar();
                    idGrupoActual = nuevoGrupo.pm.ObtenerUltimoId();
                }
                else
                {
                  
                    grupoSeleccionado.Nombre = nombreGrupo;
                    grupoSeleccionado.modificar();
                    idGrupoActual = grupoSeleccionado.Id;

                    
                    Alumnado alumnoTemp = new Alumnado();
                    List<Alumnado> alumnosAntiguos = alumnoTemp.pm.LeerAlumnosPorGrupo(idGrupoActual);
                    foreach (Alumnado a in alumnosAntiguos)
                    {
                        a.desasignarGrupo();
                    }
                }

                
                foreach (Alumnado alumno in lstAlumnosSeleccionados)
                {
                    alumno.asignarGrupo(idGrupoActual);
                }

                MessageBox.Show($"Grupo '{nombreGrupo}' guardado correctamente con {lstAlumnosSeleccionados.Count} alumno(s)",
                               "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el grupo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (grupoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un grupo de la lista inferior para eliminarlo.",
                               "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show(
                $"¿Estás seguro de eliminar el grupo '{grupoSeleccionado.Nombre}'?\n\nLos alumnos volverán a la lista de no asignados.",
                "Confirmar eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    grupoSeleccionado.borrar();
                    MessageBox.Show("Grupo eliminado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el grupo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void CargarGrupoParaEditar(Grupo grupo)
        {
            grupoSeleccionado = grupo;
            GroupNameTextBox.Text = grupo.Nombre;

           
            lstAlumnosSeleccionados.Clear();
            lstAlumnosNoAsignados.Clear();

           
            Alumnado alumno = new Alumnado();
            lstAlumnosSeleccionados = alumno.pm.LeerAlumnosPorGrupo(grupo.Id);
            ListSelected.ItemsSource = lstAlumnosSeleccionados;
            ListSelected.Items.Refresh();

            lstAlumnosNoAsignados = alumno.pm.LeerAlumnosSinGrupo();
            ListUnassigned.ItemsSource = lstAlumnosNoAsignados;
            ListUnassigned.Items.Refresh();
        }
        private void NavParticipantes_Click(object sender, RoutedEventArgs e)
        {
          
            MainTabControl.SelectedIndex = 0;
        }

        private void NavGrupos_Click(object sender, RoutedEventArgs e)
        {
           
            MainTabControl.SelectedIndex = 1;
        }
        private void NavEmpresas_Click(object sender, RoutedEventArgs e)
        {
          
            MainTabControl.SelectedIndex = 2;
        }
        private void NavTalent_Lab_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 4;
        }
        private void NavRetos_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 3;
        }

        
        private void NavInformes_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 5;
        }
        private void BtnAddReto_Click(object sender, RoutedEventArgs e)
        {
            Reto reto = new Reto(descRetotxt.Text);

            reto.insertar();
            lstReto.Add(reto);
            dgvRetos.Items.Refresh();
            start();

        }
        private void BtnModificarReto_Click(object sender, RoutedEventArgs e)
        {
            if (dgvRetos.SelectedItem is Reto seleccionado)
            {
                
                seleccionado.Descripcion_reto = descRetotxt.Text;

              
                seleccionado.modificar();

                
                CargarDatosRetos();

                MessageBox.Show("Reto modificado con éxito");
            }
        }
        private void BtnEliminarReto_Click(object sender, RoutedEventArgs e)
        {

            Reto reto = new Reto(((Reto)dgvRetos.SelectedItem).Id_Reto);
            reto.borrar();
            lstReto.Remove((Reto)dgvPersonas.SelectedItem);

            dgvPersonas.Items.Refresh();
            start();
        }
        private void BuscarReto_Click(object sender, RoutedEventArgs e)
        {

            string texto = buscarRetotxt.Text.Trim();

            if (!string.IsNullOrEmpty(texto))
            {
                
                var listaFiltrada = lstReto.Where(r =>
                    r.Descripcion_reto != null &&
                    r.Descripcion_reto.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                dgvRetos.ItemsSource = listaFiltrada;

                if (listaFiltrada.Count == 0)
                {
                    MessageBox.Show("No se han encontrado retos con esa descripción.");
                }
            }
            else
            {
                dgvRetos.ItemsSource = lstReto;
                MessageBox.Show("Introduce un texto para buscar.");
            }
        }
        private void btnInsertarTalent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TalentLab t = new TalentLab();
                t.Titulo_descriptivo = txtTalentTitulo.Text;
                t.Descripcion = txtTalentDesc.Text;
                t.Coste = txtTalentCoste.Text != "" ? double.Parse(txtTalentCoste.Text) : 0;


                var r1 = (Reto)cmbReto1.SelectedItem;
                var emp = (Empresa)cmbTalentEmpresa.SelectedItem;
                var grp = (Grupo)cmbTalentGrupo.SelectedItem;

                if (r1 == null || emp == null || grp == null)
                {
                    MessageBox.Show("Por favor, selecciona al menos el Reto 1, Empresa y Grupo.");
                    return;
                }

                t.Reto1 = r1.Id_Reto;
                t.Empresa = emp.Id; 
                t.Grupo = grp.Id;   

                t.Reto2 = (cmbReto2.SelectedItem as Reto)?.Id_Reto;
                t.Reto3 = (cmbReto3.SelectedItem as Reto)?.Id_Reto;

                t.insertar();
                CargarDatosTalent();
                MessageBox.Show("Talent Lab registrado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void dgvTalent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvTalent.SelectedItem is TalentLab t)
            {
                txtTalentTitulo.Text = t.Titulo_descriptivo;
                txtTalentDesc.Text = t.Descripcion;
                txtTalentCoste.Text = t.Coste.ToString();

                cmbReto1.SelectedItem = lstReto.FirstOrDefault(x => x.Id_Reto == t.Reto1);
                cmbReto2.SelectedItem = lstReto.FirstOrDefault(x => x.Id_Reto == t.Reto2);
                cmbReto3.SelectedItem = lstReto.FirstOrDefault(x => x.Id_Reto == t.Reto3);
                cmbTalentEmpresa.SelectedItem = lstEmpresa.FirstOrDefault(x => x.Id == t.Empresa);
                cmbTalentGrupo.SelectedItem = lstGrupos.FirstOrDefault(x => x.Id == t.Grupo);
            }
        }

        private void btnModificarTalent_Click(object sender, RoutedEventArgs e)
        {
            if (dgvTalent.SelectedItem is TalentLab t)
            {
                
                if (cmbReto1.SelectedItem == null || cmbTalentEmpresa.SelectedItem == null || cmbTalentGrupo.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, asegúrate de que el Reto 1, la Empresa y el Grupo estén seleccionados.");
                    return;
                }

                t.Titulo_descriptivo = txtTalentTitulo.Text;
                t.Descripcion = txtTalentDesc.Text;

                
                t.Reto1 = ((Reto)cmbReto1.SelectedItem).Id_Reto;
                t.Empresa = ((Empresa)cmbTalentEmpresa.SelectedItem).Id;
                t.Grupo = ((Grupo)cmbTalentGrupo.SelectedItem).Id;

                
                t.Reto2 = (cmbReto2.SelectedItem as Reto)?.Id_Reto;
                t.Reto3 = (cmbReto3.SelectedItem as Reto)?.Id_Reto;

                
                t.Coste = double.TryParse(txtTalentCoste.Text, out double c) ? c : 0;

                t.modificar();
                CargarDatosTalent();
                MessageBox.Show("Actualizado correctamente.");
            }
        }

        private void btnEliminarTalent_Click(object sender, RoutedEventArgs e)
        {
            if (dgvTalent.SelectedItem is TalentLab t)
            {
                if (MessageBox.Show("¿Seguro que quieres borrarlo?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    t.borrar();
                    CargarDatosTalent();
                }
            }
        }
        private void CargarInformeDesdeMySQL()
        {
            try
            {
              
                string query = "SELECT titulo_descriptivo, descripcion, coste FROM aceptasreto.talent_lab";

               
                DataTable tablaDatos = DBBroker.obtenerAgente().leerDataTable(query);

                
                tablaDatos.TableName = "Tabla";

             
                CrystalReport1 report = new CrystalReport1();

               
                report.SetDataSource(tablaDatos);


                visor.ViewerCore.ReportSource = report;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos con DBBroker: " + ex.Message);
            }
        }

    }

}
