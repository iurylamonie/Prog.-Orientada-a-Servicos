using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ToastNotificationClient.Resources;
using System.Text;
using Microsoft.Phone.Notification;

namespace ToastNotificationClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string aidentro;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            HttpNotificationChannel pushChannel;
            string nomeCanal = "toastSampleChannel";

            pushChannel = HttpNotificationChannel.Find(nomeCanal);
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(nomeCanal);
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(AtualizarUriCanal);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                pushChannel.Open();
                pushChannel.BindToShellToast();
            }
            else
            {
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(AtualizarUriCanal);
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
              //  MessageBox.Show(String.Format("Canal Uri é {0}", pushChannel.ChannelUri.ToString()));
               // textBoxUri.Text = pushChannel.ChannelUri.ToString();
            }
           
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                    e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData))
                    );
        }

        private void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Mensagem Recebida às {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                if (string.Compare(
                    key,
                    "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                }
                Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));
            }
        }

        private void AtualizarUriCanal(object sender, NotificationChannelUriEventArgs e)
        {
            Dispatcher.BeginInvoke(
                () => {
                    System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                    MessageBox.Show(String.Format("{0}", e.ChannelUri.ToString()));
                    aidentro = e.ChannelUri.ToString();
                }
                );
        }

        private void btnNavegar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml?NavigatedFrom="+aidentro, UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}