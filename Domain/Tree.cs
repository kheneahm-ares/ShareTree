using System;
using System.Collections.Generic;

namespace Domain
{
    public class Tree
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<UserRoot> UserRoots { get; set; } //users that have permissions to this tree, includes planter
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual Photo Image { get; set; }

    }
}