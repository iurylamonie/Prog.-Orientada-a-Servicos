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

        private  void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {      
                 string subscriptionUri = textBoxUri.Text.ToString();
                 HttpWebRequest sendNotificationRequest = (HttpWebRequest)WebRequest.Create(subscriptionUri);
                 sendNotificationRequest.Method = "POST";

                 string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<wp:Notification xmlns:wp=\"WPNotification\">" +
                    "<wp:Toast>" +
                            "<wp:Text1>" + textBoxText1.Text.ToString() + "</wp:Text1>" +
                            "<wp:Text2>" + textBox2Text2.Text.ToString() + "</wp:Text2>" +
                            "<wp:Param>/Page1.xaml?NavigatedFrom=Toast Notification</wp:Param>" +
                       "</wp:Toast> " +
                    "</wp:Notification>";

                byte[] notificationMessage = Encoding.UTF8.GetBytes(toastMessage);

                 //Set the web request content length.
                 sendNotificationRequest.ContentLength = notificationMessage.Length;
                 sendNotificationRequest.ContentType = "text/xml";
                 sendNotificationRequest.Headers["X-WindowsPhone-Target"] = "toast";
                 sendNotificationRequest.Headers["X-NotificationClass"] = "2";

                 using (Stream requestStream = sendNotificationRequest.GetRequestStreamAsync().Result)
                 {
                      requestStream.Write(notificationMessage, 0, notificationMessage.Length);
                 }
            }
            catch (Exception)
            {

                throw;
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