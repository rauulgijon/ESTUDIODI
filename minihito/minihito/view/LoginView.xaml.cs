using minihito; 
using minihito.persistence;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using  System.Linq;
namespace WPF_LoginForm.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            string userIntroducido = txtUser.Text.Trim();
            string passIntroducida = txtPass.Password.Trim();

            
            Alumnado logicaAlumnado = new Alumnado();
            List<Alumnado> lista = logicaAlumnado.getListaPersonas();

            
            if (lista == null || lista.Count == 0)
            {
                MessageBox.Show("Error: ¡La lista de alumnos está vacía! El programa no está leyendo la base de datos.");
                return;
            }
            

            
            var usuarioValido = lista.FirstOrDefault(u =>
                u.Nombre != null && u.Nombre.Trim().Equals(userIntroducido, StringComparison.OrdinalIgnoreCase) &&
                u.Apellidos != null && u.Apellidos.Trim().Equals(passIntroducida, StringComparison.OrdinalIgnoreCase));

            if (usuarioValido != null)
            {
               
                MainWindow principal = new MainWindow();
                principal.Show();
                this.Close();
            }
            else
            {

                MessageBox.Show("Usuario o contraseña incorrectos", "Error de Autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPass.Clear();
                txtUser.Focus();
            }
        }
    }
}
