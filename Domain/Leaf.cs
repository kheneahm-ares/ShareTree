using System;
using System.Collections.Generic;

namespace Domain
{
    public class Leaf
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual AppUser Creator { get; set; }
        public virtual Category Category { get; set; }
        public virtual Photo Image { get; set; }
        
    }
}