using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using ColdBanana_UmbracoTest.Models;
using ColdBanana_UmbracoTest.Helpers;
using System;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ColdBanana_UmbracoTest.Controllers
{
    public class EventSurfaceController : SurfaceController
    {
        public EventSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult SubmitForm(EventModel model)
        {
            if (!ModelState.IsValid)
            {

                return CurrentUmbracoPage();
            }



            //return RedirectToCurrentUmbracoPage();

            return View("~/Views/ThanksForOrderPage.cshtml");

        }

        [IgnoreAntiforgeryToken]
        public JsonResult BuyTicket(string customerName,string eventTitle, string ticketDetails, string customerEmail)
        {

            string eventSubject = "Tickets for " + eventTitle;
            string QRCodeFilename = "QRCodeImage.png";
            string PDFFileName = "EventTickets.pdf";
            string eventMailMessage = "<p>Hi " + customerName + "</p><p>Thank you for purchaing your ticket for the " + eventTitle + ". Your tickets are attached as a PDF in this email</p>";
            string gatewayEmailAddress = "emailgateway524@gmail.com";

            UtilityHelper.CreateQRCode(eventTitle + " " + customerName + " " + ticketDetails, QRCodeFilename);

            UtilityHelper.CreatePDF(eventTitle, customerName, ticketDetails, QRCodeFilename, PDFFileName);
            UtilityHelper.SendEmail(gatewayEmailAddress, customerEmail, eventSubject, eventMailMessage, PDFFileName);
            
            return Json("{result: 'success'}");
        }


    }

}

