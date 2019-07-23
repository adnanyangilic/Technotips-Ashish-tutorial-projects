namespace HowToSendEmail.Controllers.Classes.Email
{
    public class EmailProperties
    {
        /// <summary>
        ///     Visszatéríti az üzenet törzsét HTML formátumban
        /// </summary>
        public string EmailHTMLBody
        {
            get
            {
                return "<p> Üdvözöllek,<br><br> " +
                     "Ezt az e-mail - t azért kaptad mert azt az alkalmazást futtattad, amelyet " +
                     "ASP.NET MVC-ben készítettem el neked, segédletként! Az alkalmazás megfelelően " +
                     "lefutott, mivel megkaptad az e-mailt!<br><br> " +
                     "Üdvözlettel: Az alkalmazás futtatója!</p>";
            }
        }

        /// <summary>
        ///     Visszatéríti az üzenet címzettjét
        /// </summary>
        public string ToEmailAdress
        {
            get { return "testemail@testemail.com"; }
        }

        /// <summary>
        ///     Visszatéríti az üzenet Tárgyát
        /// </summary>
        public string EmailSubject
        {
            get { return "Test email send"; }
        }

        /// <summary>
        ///     Visszatéríti az üzenet küldő E-mail címét, amelyet a Web.config fájlból olvas fel
        /// </summary>
        public string SenderEmailAdress
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString(); }
        }

        /// <summary>
        ///     Visszatéríti az üzenet küldő E-mail címének jelszavát, amelyet a Web.config fájlból olvas fel
        /// </summary>
        public string SenderEmailPassword
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString(); }
        }

        /// <summary>
        ///     Visszatéríti az üzenet küldő SMTP Host-ját
        /// </summary>
        public string SMTPHost
        {
            get { return "smtp.gmail.com"; }
        }

        /// <summary>
        ///     Visszatéríti az üenet küldő Portját
        /// </summary>
        public int SMTPPort
        {
            get { return 587; }
        }
    }
}