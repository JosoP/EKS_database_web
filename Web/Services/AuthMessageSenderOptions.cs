namespace Web.Services
{
    /// <summary>
    ///     Service for storing of e-mail server login information secretly.
    /// </summary>
    public class AuthMessageSenderOptions
    {
        /// <summary>
        ///     Username for gmail account used as e-mail service.
        /// </summary>
        public string GmailUser { get; set; }
        
        /// <summary>
        ///     Password for gmail account used as e-mail service.
        /// </summary>
        public string GmailKey { get; set; }
    }
}