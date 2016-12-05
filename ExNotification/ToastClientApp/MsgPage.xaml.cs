using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ToastClientApp
{
    public partial class MsgPage : PhoneApplicationPage
    {
        public MsgPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Notificação recebida com o aplicativo fechado
            // Este método é chamado e os dados vem na QueryString
            var dic = NavigationContext.QueryString;
            // Atualiza lista de mensagens
            if (dic.ContainsKey("Msg1")) listMsg.Items.Add(dic["Msg1"]);
            if (dic.ContainsKey("Msg2")) listMsg.Items.Add(dic["Msg2"]);
        }
    }
}