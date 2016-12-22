using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WhatsApp
{
    public partial class AddMembro : PhoneApplicationPage
    {
        public AddMembro()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GerenciadorGrupo.xaml", UriKind.Relative));
        }

        private void btnAdicionarMembro_Click(object sender, EventArgs e)
        {

        }

        private async void btnListarUsuarios_Click(object sender, EventArgs e)
        {
            Models.Usuario u = new Models.Usuario();
            List<Models.Usuario> obj = await u.Listar();
            listUsuarios.ItemsSource = obj;
        }
    }
}