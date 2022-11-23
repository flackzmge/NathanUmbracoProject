using System;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;
using QRCodeEncoderLibrary;
using System.Net.Mail;
using System.Net.Mime;
using System;
using System.IO;

namespace ColdBanana_UmbracoTest.Helpers
{
	public static class UtilityHelper
	{


        //This method sends an email, and optionally a file attachment 
        static public void SendEmail(string fromEmail, string toEmail, string subject, string body, string filename = "")
        {


            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("emailgateway524@gmail.com", "lagebrxqozhdhqhz");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromEmail);
            msg.To.Add(new MailAddress(toEmail));

            if (filename != "")
            {
                msg.Attachments.Add(new Attachment(filename, MediaTypeNames.Application.Octet));

            }

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = string.Format(body);
            client.Send(msg);


            if (filename != "")
            {
                File.Delete(filename);

            }
            


        }



        //This method is used to create a QRCode and save it to a file                                                                                                                                                                                                                                                                                                                                                                                   
        static public void CreateQRCode(string details, string filename)
        {

            var encoder = new QRCodeEncoder();
            encoder.Encode(details);
            encoder.SaveQRCodeToPngFile(filename);
        }


        //Create PDF File wirh QRCode and ticket details
        static public void CreatePDF(string title, string customerName, string ticketDetails, string QRFilename, string filename)

        {

            //GlobalFontSettings.FontResolver = new FontResolver();

            var document = new PdfDocument();
            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 20, XFontStyle.Bold);

            var textColor = XBrushes.Black;
            var layout = new XRect(5, 5, page.Width, page.Height);
            var format = XStringFormats.Center;

            const string facename = "Times New Roman";

            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi);

            XFont fontRegular = new XFont(facename, 30, XFontStyle.Regular, options);
            XFont fontBold = new XFont(facename, 30, XFontStyle.Bold, options);
            XFont fontItalic = new XFont(facename, 30, XFontStyle.Italic, options);
            XFont fontBoldItalic = new XFont(facename, 30, XFontStyle.BoldItalic, options);

            gfx.DrawString(title, fontBold, XBrushes.DarkSlateGray, 170, 40);
            gfx.DrawString(customerName, fontBold, XBrushes.DarkSlateGray, 150, 435);
            gfx.DrawString(ticketDetails, fontBold, XBrushes.DarkSlateGray, 150, 480);

            XImage image = XImage.FromFile(QRFilename);

            gfx.DrawImage(image, 160, 80, 300, 300);

            document.Save(filename);


        }



    }
}

