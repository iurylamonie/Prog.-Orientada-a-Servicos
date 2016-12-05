using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToastSendApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string ip = "http://10.21.0.137";

        private async void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            // Recupera os usuários do serviço de usuários
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await
                httpClient.GetAsync("/1164676/api/usuario");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.User> obj = JsonConvert.
                DeserializeObject<List<Models.User>>(str);
            listUser.ItemsSource = obj;
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            // Envia a mensagem diretamente para o Microsoft Push Notification Service
            if (listUser.SelectedIndex == -1)
            {
                MessageBox.Show("Usuário não selecionado");
                return;
            }
            try
            {
                // Mensagem: toast notification
                string msg =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<wp:Notification xmlns:wp=\"WPNotification\">" +
                    "<wp:Toast>" +
                        "<wp:Text1>" + txt1.Text + "</wp:Text1>" +
                        "<wp:Text2>" + txt2.Text + "</wp:Text2>" +
                        "<wp:Param>/MsgPage.xaml?Msg1="
                            + txt1.Text + "&amp;Msg2=" + txt2.Text + "</wp:Param>" +
                    "</wp:Toast>" +
                "</wp:Notification>";

                // Codifica a mensagem a ser enviada
                byte[] msgBytes = Encoding.Default.GetBytes(msg);

                // Cria a requisição web com a notificação para a o usuário selecionado
                string uri = (listUser.SelectedItem as Models.User).Uri;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "Post";
                request.ContentType = "text/xml";
                request.ContentLength = msg.Length;
                request.Headers["X-MessageID"] = Guid.NewGuid().ToString();
                request.Headers["X-WindowsPhone-Target"] = "toast";
                request.Headers["X-NotificationClass"] = "2";

                // Envia a requisição web 
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(msgBytes, 0, msgBytes.Length);
                }
            }
            catch (Exception ex)
            {
                txtErro.Text = ex.Message.ToString();
            }
        }

        private async void btnEnviar2_Click(object sender, RoutedEventArgs e)
        {
            // Envia a mensagem para o serviço de usuários
            // O serviço de usuários envia para o Microsoft Push Notification Service
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            Models.Mensagem m = new Models.Mensagem
            {
                Uri = (listUser.SelectedItem as Models.User).Uri,
                Texto1 = txt1.Text,
                Texto2 = txt2.Text,
                Param = "MsgPage.xaml"
            };
            string s = "=" + JsonConvert.SerializeObject(m);
            var content = new StringContent(s, Encoding.UTF8,
                "application/x-www-form-urlencoded");
            await httpClient.PostAsync("/1164676/api/mensagem", content);
        }

    }
}
