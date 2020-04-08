using System;

namespace Domain
{
    /// <summary>
    /// This is a JXN table that ties a Tree's Roots and Users 
    /// </summary>
    public class UserRoot
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid TreeId { get; set; }
        public virtual Tree Tree { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsPlanter { get; set; }
    }
}