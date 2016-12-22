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
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/AddMembro.xaml", UriKind.Relative));
        }
    }
}