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
    public partial class Inicial : PhoneApplicationPage
    {
        Models.Usuario u;
        Models.Grupo g;
        public Inicial()
        {
            InitializeComponent();
            textBlockNome.Text = Models.UsuarioConectado.Nome;
            
            /*List<Models.TUsuario> listUser = new List<Models.TUsuario>();
            listUser.Add(new Models.TUsuario { Imagem = "/Assets/Iury.png", Nome = "Iury", Descricao = "Aluno" });
            listUser.Add(new Models.TUsuario { Imagem = "/Assets/Gilbert.png", Nome = "Gilbert", Descricao = "Professor" });
            listUser.Add(new Models.TUsuario { Imagem = "/Assets/George.png", Nome = "George", Descricao = "Professor" });
            listBoxAmigos.ItemsSource = listUser;*/

        }    

        private async void btnMsgUsuario_Click(object sender, EventArgs e)
        {
            
           
        }

        private async void btnListar_Click(object sender, EventArgs e)
        {
            u = new Models.Usuario();
            List<Models.Usuario> obj = await u.Listar();
            listBoxAmigos.ItemsSource = obj;
        }

        private void btnCriarGrupo_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CriarGrupo.xaml", UriKind.Relative));
        }

        private void buttonDeletar_Click(object sender, RoutedEventArgs e)
        {
            g = new Models.Grupo();
            g.Deletar(listBoxGrupo.SelectedItem.ToString());
        }

        private async void buttonListar_Click(object sender, RoutedEventArgs e)
        {
            g = new Models.Grupo();
            List<Models.Grupo> obj = await g.Listar();
            listBoxGrupo.ItemsSource = obj;
        }

        private void buttonGerenciador_Click(object sender, RoutedEventArgs e)
        {
            Models.GrupoEscolhido.Descricao = listBoxGrupo.SelectedItem.ToString();
            NavigationService.Navigate(new Uri("/GerenciadorGrupo.xaml", UriKind.Relative));
        }
    }
}