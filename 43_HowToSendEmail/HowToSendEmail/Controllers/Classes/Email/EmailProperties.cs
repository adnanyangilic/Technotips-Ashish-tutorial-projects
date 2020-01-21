namespace HowToSendEmail.Controllers.Classes.Email
{
    public class EmailProperties
    {
        public string EmailHTMLBody =>
            "<p> Üdvözöllek,<br><br> " +
            "Ezt az e-mail - t azért kaptad mert azt az alkalmazást futtattad, amelyet " +
            "ASP.NET MVC-ben készítettem el neked, segédletként! Az alkalmazás megfelelően " +
            "lefutott, mivel megkaptad az e-mailt!<br><br> " +
            "Üdvözlettel: Az alkalmazás futtatója!</p>";

        public string ToEmailAdress => "testemail@testemail.com";

        public string EmailSubject => "Test email send";

        public string SenderEmailAdress => System.Configuration.ConfigurationManager.AppSettings["SenderEmail"];

        public string SenderEmailPassword => System.Configuration.ConfigurationManager.AppSettings["SenderPassword"];

        public string SMTPHost => "smtp.gmail.com";

        public int SMTPPort => 587;
    }
}