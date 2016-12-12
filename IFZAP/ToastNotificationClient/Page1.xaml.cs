using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ToastNotificationClient
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           // textBlockAviso.Text = "Navigated here from " + this.NavigationContext.QueryString["NavigatedFrom"];
            textBoxTeste.Text = "Navigated here from " + this.NavigationContext.QueryString["NavigatedFrom"];
        }
    }
}