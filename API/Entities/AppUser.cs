namespace API.Entities
{
    public class AppUser
    {
        public int ID { get; set; }

        public String UserName { get; set; }

    public byte[] HashPassword { get; set; }

    public byte[] HashSalt { get; set; }
    }
}