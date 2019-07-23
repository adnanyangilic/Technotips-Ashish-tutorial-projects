using HowToSendEmail.Controllers.Classes.Email;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace HowToSendEmail.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /// Vizsgálat, hogy volt-e már bejelentkezett felhasználó,
            /// mert akkor a bejelentkeztetett Index-et jelenítjük meg
            /// a User-nak
            if (Session["UserID"] != null)
            {
                return RedirectToAction("../Example/Index");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        ///     HTTP GET Metódus. Elküld a beleégetett E-mail címre
        ///     egy beleégetett konstans szöveges üzenetet
        /// </summary>
        /// <returns>TRUE - Ha sikeres; False - Ha sikertelen</returns>
        public JsonResult SendEmailToUser()
        {
            bool result = false;
            
            result = SendEmail();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Végrehajtja az E-mail küldés metódusát
        /// </summary>
        /// <returns>TRUE - Ha sikeres; False - Ha sikertelen</returns>
        private bool SendEmail()
        {
            EmailProperties emailProperties = new EmailProperties();

            try
            {
                SmtpClient client = CreateSmtpClient(emailProperties);

                /// Üzenet elküldése az SMTP Kliens használatával, amely egy reprezentált
                /// e-mail üzenetet küld el (MailMessage)
                client.Send(CreateMailMessage(emailProperties));

                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("\n\n" + ex);

                return false;
            }
        }

        /// <summary>
        ///     Elkészíti a szükséges SMTP Kliens-t amely az üzenetküldéshez szükséges.
        ///     Tartalmazza az alábbi beállításokat: 
        ///         SSL Engedélyezés; Timeout; Network Delivery Method; A Default Hitelesítési
        ///         adatok helyett, saját Hitelesítési adatok beállítása
        /// </summary>
        /// <param name="emailProperties">Az e-mail-ek megfelelő beállításaihoz szükséges tulajdonságok</param>
        /// <returns>Egy elkészített SMTP Kliens amely az üzenetküldéshez szükséges</returns>
        private SmtpClient CreateSmtpClient(EmailProperties emailProperties)
        {
            /// Kiszolgáló és Port beállítása, amelyet az üzenetküldő szolgáltatás biztosít.
            /// (Használt SMTP Kiszolgáló a Gmail)
            return new SmtpClient(emailProperties.SMTPHost, emailProperties.SMTPPort)
            {
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,      
                /// Hitelesítési adatok beállítása: A Küldő Email címe és Küldő Email címének jelszava
                Credentials = new NetworkCredential(emailProperties.SenderEmailAdress, emailProperties.SenderEmailPassword)
            };
        }

        /// <summary>
        ///     Egy e-mail üzenet reprezentálása
        /// </summary>
        /// <param name="emailProperties">Az e-mail-ek megfelelő beállításaihoz szükséges tulajdonságok</param>
        /// <returns>Egy e-mail üzenet reprezentálása</returns>
        private MailMessage CreateMailMessage(EmailProperties emailProperties)
        {
            return new MailMessage(emailProperties.SenderEmailAdress, emailProperties.ToEmailAdress, emailProperties.EmailSubject, string.Format(emailProperties.EmailHTMLBody))
            {
                IsBodyHtml = true
            };
        }
    }
}