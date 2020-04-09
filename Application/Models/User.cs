namespace Application.Models
{
    /// <summary>
    /// This class is returned to the client
    /// </summary>
    public class User
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
    }
}