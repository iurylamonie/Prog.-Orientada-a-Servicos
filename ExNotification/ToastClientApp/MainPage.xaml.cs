﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ToastClientApp.Resources;
using Microsoft.Phone.Notification;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace ToastClientApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        // Identificação do canal de notificação
        private string channelName = "channelIfrn";

        private void btnUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Recupera o canal da notificação se existir
            HttpNotificationChannel httpChannel = HttpNotificationChannel.Find(channelName);

            try
            {
                // Verifica se o canal de notificação existe
                if (httpChannel == null)
                {
                    // Canal de notificação não existe, instancia novo canal.
                    httpChannel = new HttpNotificationChannel(channelName);

                    // Delegates para atualização, erro e recebimento de mensagem
                    httpChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(httpChannel_ChannelUriUpdated);
                    httpChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(httpChannel_ErrorOccurred);
                    httpChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(httpChannel_ShellToastNotificationReceived);

                    // Abre o canal de notificação com o Microsoft Push Notification Service
                    httpChannel.Open();

                    // Efetiva a ligação com o canal de notificação
                    httpChannel.BindToShellToast();
                }
                else
                {
                    // Canal existe
                    
                    // Delegates para atualização, erro e recebimento de mensagem
                    httpChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(httpChannel_ChannelUriUpdated);
                    httpChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(httpChannel_ErrorOccurred);
                    httpChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(httpChannel_ShellToastNotificationReceived);
                    
                    // Mostra dados do canal
                    System.Diagnostics.Debug.WriteLine(httpChannel.ChannelUri.ToString());
                    // MessageBox.Show(String.Format("O canal URI é {0}", httpChannel.ChannelUri.ToString()));
                    
                    // Registra o usuário no serviço de usuários
                    atualizarUsuario(txtUsuario.Text, httpChannel.ChannelUri.ToString());
                }
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void httpChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                // Mostra dados do canal
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                // MessageBox.Show(String.Format("O canal URI é {0}", e.ChannelUri.ToString()));

                // Registra o usuário no serviço de usuários
                atualizarUsuario(txtUsuario.Text, e.ChannelUri.ToString());
            });
        }

        private void httpChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Mostra dados do erro
            Dispatcher.BeginInvoke(() => MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData)));
        }

        private void httpChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            // Notificação recebida com o aplicativo aberto
            // Este método é chamado e os dados vem no parâmetro e.Collection (NotificationEventArgs)

            // Página destino
            string relativeUri = string.Empty;

            // Mensagem apresentada ao usuário
            StringBuilder msg = new StringBuilder();
            msg.AppendFormat("Toast Recebido {0}:\n", DateTime.Now.ToShortTimeString());

            // Recupera dados da notificação
            foreach (string key in e.Collection.Keys)
            {
                msg.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                // Página destino
                if (string.Compare(key, "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0) {
                    relativeUri = e.Collection[key];
                }
            }

            // Mostra dados da notificação
            Dispatcher.BeginInvoke(() =>
            {
                // Mensagem apresentada ao usuário
                MessageBox.Show(msg.ToString());
                // Atualiza lista de mensagens
                listMsg.Items.Add(e.Collection["wp:Text1"] + ": " + e.Collection["wp:Text2"]);
            });
        }

        private async void atualizarUsuario(string nome, string uri)
        {
            // Registra o usuário no serviço de usuários
            string ip = "http://10.21.0.137";
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            // Dados do usuário
            Models.User user = new Models.User
            {
                Nome = nome,
                Uri = uri
            };
            string s = "=" + JsonConvert.SerializeObject(user);
            var content = new StringContent(s, Encoding.UTF8,
                "application/x-www-form-urlencoded");
            // Chamada ao serviço de usuários
            await httpClient.PostAsync("/1164676/api/usuario", content);
            MessageBox.Show("Usuário registrado");
        }
    }
}