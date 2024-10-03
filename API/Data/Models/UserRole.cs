namespace API.Data.Models
{
    public partial class UserRole
    {
        public int urId { get; set; }

        public string role { get; set; }

        public virtual ICollection<User>? Users { get; set; }
    }
}
