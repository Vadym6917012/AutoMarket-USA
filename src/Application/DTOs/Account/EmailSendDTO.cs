namespace Application.DTOs.Account
{
    public class EmailSendDTO
    {
        public EmailSendDTO(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
