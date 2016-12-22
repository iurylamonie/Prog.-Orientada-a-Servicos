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
    public partial class GerenciadorGrupo : PhoneApplicationPage
    {
        public GerenciadorGrupo()
        {
            InitializeComponent();
            Grupo.Text = Models.GrupoEscolhido.Descricao;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/AddMembro.xaml", UriKind.Relative));
        }

        private async void btnListarMembros_Click(object sender, EventArgs e)
        {
            Models.Membro membro = new Models.Membro();
            var obj = await membro.Listar(Models.GrupoEscolhido.Descricao);
            listMembros.ItemsSource = obj;
        }
    }
}