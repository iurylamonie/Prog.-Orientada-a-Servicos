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
            List<Models.TUsuario> listUser = new List<Models.TUsuario>();
            listUser.Add(new Models.TUsuario { Nome = "Iury", Descrição = "Aluno" });
            listUser.Add(new Models.TUsuario { Nome = "Gilbert", Descrição = "Professor" });
            listUser.Add(new Models.TUsuario { Nome = "George", Descrição = "Professor" });
            listUsuarios.ItemsSource = listUser;
            listUsuarios.SelectedItem = 1;
        }
    }
}