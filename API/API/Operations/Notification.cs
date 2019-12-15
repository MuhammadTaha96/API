using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.IO;
using Telesign;

namespace API.Operations
{
    public class Notification
    {
        public static bool TWILIOSMS(string templateName, UserLogin user, Book book, Reservation reserve)
        {
            try
            {
                string accountSid = ConfigurationManager.AppSettings["accountSid"].ToString();
                string authToken = ConfigurationManager.AppSettings["authToken"].ToString();
                string filePath = @"C:\Users\@dm1n\Desktop\GitRepository\API\API\API\Content\templates\ReserveACopy.txt";
                string templateContent = File.ReadAllText(filePath);

                TwilioClient.Init(accountSid, authToken);

                templateContent = Notification.TemplateParser(templateContent, user, book, reserve);

                var message = MessageResource.Create(
                    body: templateContent,
                    from: new Twilio.Types.PhoneNumber("+16024599778"),
                    to: new Twilio.Types.PhoneNumber(user.PhoneNumber)
                );
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public static void SendTeleSignSMS(string templateName, UserLogin user, Book book, Reservation reserve)
        {

            string filePath = @"C:\Users\@dm1n\Desktop\GitRepository\API\API\API\Content\templates\ReserveACopy.txt";
            string templateContent = File.ReadAllText(filePath);
            string customerId = "A7B64D9C-9375-433A-87FE-90454D6F2429";
            string apiKey = "sDfY84uHPfzViXCLXejqkuP0uAzlUKrC1DbwKoNYrBXj6qmzb/WOxvyO1xjGtfnJA9YOzaa9ZTSZeKz2pTzhMA==";

            templateContent = Notification.TemplateParser(templateContent, user, book, reserve);


            string message = templateContent;
            string messageType = "ARN";

            try
            {
                MessagingClient messagingClient = new MessagingClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = messagingClient.Message(user.PhoneNumber, message, messageType);
            }
            catch (Exception e)
            {
               
            }

        }

        private static string TemplateParser(string templateContent, UserLogin user, Book book, Reservation reserve)
        {

            if (user != null && book != null && reserve != null && !string.IsNullOrEmpty(templateContent))
            {
                return templateContent.Replace("[UserName]", user.FullName).Replace("[BookName]", book.Title).Replace("[Date]", reserve.EndDateTime.ToString());
            }
            else
                return string.Empty;
        }
    }
}