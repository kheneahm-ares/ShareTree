using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<UserRoot> UserRoots { get; set; } //users can belong to many trees
        public virtual ICollection<Photo> Photos { get; set; }

    }
}