using Microsoft.AspNet.Identity;
using project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace project_sem_3.Utils
{
    public class MailUtil
    {
        public void SendMail(string email, string body, string subject)
        {
            var emailService = new EmailService();

            IdentityMessage message = new IdentityMessage()
            {
                Destination = email,
                Subject = subject,
                Body = body,
            };
            // todo trungnq handle error
            emailService.SendAsync(message);
        }

        public void SendMails(List<string> listEmail, string body, string subject)
        {
            for (int i = 0; i < listEmail.Count; i++)
            {
                var email = listEmail[i];
                SendMail(email, body, subject);
            }
        }

        public string CreateBodyAds(Product product)
        {

            var body = "";
            body += "<div style='background-color: #ffecb3; display:block; margin-left:50px; margin-right: 50px; font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif;'>";
            body += "<div style='padding: 30px'>";
            body += "<h2 style='color: #997300'>** Explore Our Fashional Products **</h2>";
            body += "<p>";
            body += "<img src=" + product.GetDefaultThumbnail() + " alt=" + product.Name + " style='float:left; width: 100px; margin-right: 20px' >";
            body += "<strong>";
            body += product.Name;
            body += "</strong>";
            body += "</p>";
            body += "<p>";
            if (product.Discount != null)
            {
                body += "Price <span style = 'text-decoration: line-through; color: #8c8c5a'>" + product.Price +
                    "</span>$. Now you can get it with only<strong>" + product.Discount + "$</strong>";
            }
            else
            {
                body += "Price <span style = 'color: #8c8c5a'>" + product.Price +
                    "</span>$";
            }

            body += "</p>";
            body += "<p>";
            body += "<button href = '" + ConfigurationManager.AppSettings["BaseUri"].ToString() + "' style = 'background-color: #ffecb3; border: 1px solid #e6ac00; color: #e6ac00 ; border-radius: 10px; padding: 5px'>";
            body += "Buy now";
            body += "</button >";
            body += "</p>";
            body += "</div>";
            body += "<div style = 'background-color: #f5f5ef; padding: 30px; font-size: 12px'>";
            body += "<h3 style = 'font-size: 15px' > ALIMENSWEAR </h3>";
            body += "<p>3-5 Business Days Delivery</p>";
            body += "<p>+1235 2355 98</p>";
            body += "<p> youremail@email.com </p>";
            body += "</div>";
            body += "</div>";
            return body;
        }

        public string CreateBodyAds2(List<Product> products, string header)
        {
            var body = "";
            body += "<div style='background-color: #ffecb3; display:block; margin-left:50px; margin-right: 50px; font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif;'>";
            body += "<div style='padding: 30px'>";
            body += "<h3 style='color: #997300'>" + header + "<h3>";
            for (int i = 0; i < products.Count; i++)
            {
                body += "<p>";
                body += "<img src=" + products[i].GetDefaultThumbnail() + " alt=" + products[i].Name + " style='float:left; width: 100px; margin-right: 20px' >";
                body += products[i].Name;
                body += "</p>";
                body += "<p>";
                if (products[i].Discount != null)
                {
                    body += "Price <span style = 'text-decoration: line-through; color: #8c8c5a'>" + products[i].Price +
                        "</span>$. Now you can get it with only<strong>" + products[i].Discount + "$</strong>";
                }
                else
                {
                    body += "Price <span style = 'color: #8c8c5a'>" + products[i].Price +
                        "</span>$";
                }
                body += "</p>";
                body += "<p>";
                body += "<button href = '" + ConfigurationManager.AppSettings["BaseUri"].ToString() + "' style = 'background-color: #ffecb3; border: 1px solid #e6ac00; color: #e6ac00 ; border-radius: 10px; padding: 5px'>";
                body += "Buy now";
                body += "</button >";
                body += "</p>";
            }
            body += "</div>";
            body += "<div style = 'background-color: #f5f5ef; padding: 30px; font-size: 12px'>";
            body += "<h3 style = 'font-size: 15px' > ALIMENSWEAR </h3>";
            body += "<p>3-5 Business Days Delivery</p>";
            body += "<p>+1235 2355 98</p>";
            body += "<p> youremail@email.com </p>";
            body += "</div>";
            body += "</div>";
            return body;
        }

        //public string CreateBodyAds(Product product)
        //{
        //    var body = "";
        //    body += "<div style='background-color: #ffecb3; display:block; margin-left:50px; margin-right: 50px; font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif;'>";
        //    body += "<div style='padding: 30px'>";
        //    body += "<h3 style='color: #997300'>Explore our discount products<h3>";
        //    body += "<p>";
        //    body += "<img src=" + product.GetDefaultThumbnail() + " alt=" + product.Name + " style='float:left; width: 100px; margin-right: 20px' >";
        //    body += product.Name;
        //    body += "</p>";
        //    body += "<p>";
        //    body += "Price <span style = 'text-decoration: line-through; color: #8c8c5a'>" + product.Price +
        //            "</span> Now you can get it with only<strong>" + product.Discount + "</strong>";
        //    body += "</p>";
        //    body += "<p>";
        //    body += "<button href = '#' style = 'background-color: #ffecb3; border: 1px solid #e6ac00; color: #e6ac00 ; border-radius: 10px; padding: 5px'>";
        //    body += "Buy now";
        //    body += "</button >";
        //    body += "</p>";
        //    body += "</div>";
        //    body += "<div style = 'background-color: #f5f5ef; padding: 30px; font-size: 12px'>";
        //    body += "<h3 style = 'font-size: 15px' > ALIMENSWEAR </h3>";
        //    body += "<p>3-5 Business Days Delivery</p>";
        //    body += "<p>+1235 2355 98</p>";
        //    body += "<p> youremail@email.com </p>";
        //    body += "</div>";
        //    body += "</div>";
        //    return body;
        //}

        //public string CreateBodyAds2(List<Product> products, string header)
        //{
        //    var body = "";
        //    body += "<div style='background-color: #ffecb3; display:block; margin-left:50px; margin-right: 50px; font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif;'>";
        //    body += "<div style='padding: 30px'>";
        //    body += "<h3 style='color: #997300'>" + header + "<h3>";
        //    for (int i = 0; i < products.Count; i++)
        //    {
        //        body += "<p>";
        //        body += "<img src=" + products[i].GetDefaultThumbnail() + " alt=" + products[i].Name + " style='float:left; width: 100px; margin-right: 20px' >";
        //        body += products[i].Name;
        //        body += "</p>";
        //        body += "<p>";
        //        body += "Price <span style = 'text-decoration: line-through; color: #8c8c5a'>" + products[i].Price +
        //                "</span> Now you can get it with only<strong>" + products[i].Discount + "</strong>";
        //        body += "</p>";
        //        body += "<p>";
        //        body += "<button href = '#' style = 'background-color: #ffecb3; border: 1px solid #e6ac00; color: #e6ac00 ; border-radius: 10px; padding: 5px'>";
        //        body += "Buy now";
        //        body += "</button >";
        //        body += "</p>";
        //    }
        //    body += "</div>";
        //    body += "<div style = 'background-color: #f5f5ef; padding: 30px; font-size: 12px'>";
        //    body += "<h3 style = 'font-size: 15px' > ALIMENSWEAR </h3>";
        //    body += "<p>3-5 Business Days Delivery</p>";
        //    body += "<p>+1235 2355 98</p>";
        //    body += "<p> youremail@email.com </p>";
        //    body += "</div>";
        //    body += "</div>";
        //    return body;
        //}

    }
}