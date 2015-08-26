namespace NancyDemo.Areas.Areas.Admin.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return (Username ?? "").ToLowerInvariant() == "larry" &&
                   (Password ?? "").ToLowerInvariant() == "ken sent me";
        }
    }
}