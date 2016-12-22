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
    public partial class CriarGrupo : PhoneApplicationPage
    {
        private Models.Grupo grupo;
        public CriarGrupo()
        {
            InitializeComponent();
        }

        private void buttonCriar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Grupo grupo = new Models.Grupo { Descricao = textBoxNome.Text, IdAdm = Models.UsuarioConectado.Id };
                grupo.Criar(grupo);
                NavigationService.Navigate(new Uri("/Inicial.xaml", UriKind.Relative));
            }
            catch (Exception)
            {

                MessageBox.Show("Aceita o erro!");
            }
            
        }
    }
}