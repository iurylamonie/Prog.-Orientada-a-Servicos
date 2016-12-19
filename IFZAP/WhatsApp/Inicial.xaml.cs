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
        public Inicial()
        {
            InitializeComponent();
            textBlockNome.Text = Models.UsuarioConectado.Nome;
            List<Models.TUsuario> listUser = new List<Models.TUsuario>();
            listUser.Add(new Models.TUsuario { Imagem = "/Assets/Iury.png", Nome = "Iury", Descricao = "Aluno" });
            listUser.Add(new Models.TUsuario { Imagem = "/Assets/Gilbert.png", Nome = "Gilbert", Descricao = "Professor" });
            listUser.Add(new Models.TUsuario { Imagem = "/Assets/George.png", Nome = "George", Descricao = "Professor" });
            longListUser.ItemsSource = listUser;
        }

 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var str = longListUser.SelectedItem.ToString();
            textBlockNome.Text = str;
        }
    }
}