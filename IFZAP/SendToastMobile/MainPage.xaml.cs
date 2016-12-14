using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SendToastMobile.Resources;
using System.IO;
using System.Text;

namespace SendToastMobile
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private  async void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {      
                 string subscriptionUri = textBoxUri.Text.ToString();
                 HttpWebRequest sendNotificationRequest = (HttpWebRequest)WebRequest.Create(subscriptionUri);

                 string msg = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<wp:Notification xmlns:wp=\"WPNotification\">" +
                    "<wp:Toast>" +
                            "<wp:Text1>" + textBoxText1.Text.ToString() + "</wp:Text1>" +
                            "<wp:Text2>" + textBox2Text2.Text.ToString() + "</wp:Text2>" +
                            "<wp:Param>/Page1.xaml?NavigatedFrom=Toast Notification</wp:Param>" +
                       "</wp:Toast> " +
                    "</wp:Notification>";

                byte[] msgByte = Encoding.UTF8.GetBytes(msg);
                //.UTF8.GetBytes(toastMessage);

                sendNotificationRequest.Method = "Post";
                sendNotificationRequest.ContentType = "text/xml";
                sendNotificationRequest.ContentLength = msg.Length;
                sendNotificationRequest.Headers["X-MessageID"] = Guid.NewGuid().ToString();
                sendNotificationRequest.Headers["X-WindowsPhone-Target"] = "toast";
                sendNotificationRequest.Headers["X-NotificationClass"] = "2";
                //Set the web request content length.
                /*
                sendNotificationRequest.ContentLength = notificationMessage.Length;
                 sendNotificationRequest.ContentType = "text/xml";
                 sendNotificationRequest.Headers["X-WindowsPhone-Target"] = "toast";
                 sendNotificationRequest.Headers["X-NotificationClass"] = "2";
                 */
                using (Stream requestStream = await sendNotificationRequest.GetRequestStreamAsync())
                 {
                      requestStream.Write(msgByte, 0, msgByte.Length);
                 }
            }
            catch (Exception)
            {

                MessageBox.Show("Erro");
            }
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