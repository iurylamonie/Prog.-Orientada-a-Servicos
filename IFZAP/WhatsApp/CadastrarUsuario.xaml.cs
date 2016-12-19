using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Notification;
using System.Text;

namespace WhatsApp
{
    public partial class CadastrarUsuario : PhoneApplicationPage
    {
        private string nomeCanal = "whatsCanal";
        
        public CadastrarUsuario()
        {
            InitializeComponent();
        }

        private void buttonCadastrar_Click(object sender, RoutedEventArgs e)
        {
            HttpNotificationChannel canalPush = HttpNotificationChannel.Find(nomeCanal);

            try
            {
               
                if (canalPush == null)
                {
                    canalPush = new HttpNotificationChannel(nomeCanal);
                    canalPush.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(AtualizarUriCanal);
                    canalPush.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(CanalPush_ErrorOccurred);
                    canalPush.Open();
                    canalPush.BindToShellToast();
                }
                else
                {
                    canalPush.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(AtualizarUriCanal);
                    canalPush.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(CanalPush_ErrorOccurred);
                    //System.Diagnostics.Debug.WriteLine(canalPush.ChannelUri.ToString());
                    // Registra o usuário no serviço de usuários
                    //atualizarUsuario(txtUsuario.Text, canalPush.ChannelUri.ToString());
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void CanalPush_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                    e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData))
                    );
        }

        private void AtualizarUriCanal(object sender, NotificationChannelUriEventArgs e)
        {
            Dispatcher.BeginInvoke(
                () => {
                    System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                    Models.Usuario u = new Models.Usuario();
                    u.CriarUsuario(textBoxNome.Text, e.ChannelUri.ToString());              
                }
                );
        }
    }
}