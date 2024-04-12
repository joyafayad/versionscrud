namespace VersionsCRUD.Models
{
    public partial class User
    {
        public User()
        {
            AccessLogs = new HashSet<AccessLog>();
            Comments = new HashSet<Comment>();
            Permissions = new HashSet<Permission>();
            Subscriptions = new HashSet<Subscription>();
            Tags = new HashSet<Tag>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Isloggedin { get; set; }
        public DateOnly? Lastlogin { get; set; }
        public DateOnly? Lastlogout { get; set; }

        public virtual ICollection<AccessLog> AccessLogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
