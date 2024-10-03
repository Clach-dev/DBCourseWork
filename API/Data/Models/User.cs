namespace API.Data.Models
{
    public partial class User
    {
        public int usId { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public int urId { get; set; }

        public virtual UserRole? UserRole { get; set; }
    }
}
