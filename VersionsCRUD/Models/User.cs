using System;
using System.Collections.Generic;

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
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }

        public virtual ICollection<AccessLog> AccessLogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
