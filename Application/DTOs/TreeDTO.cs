using System;
using System.Collections.Generic;
using Domain;

namespace Application.DTOs
{
    public class TreeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<UserRootDTO> UserRoots { get; set; } //users that have permissions to this tree, includes planter
        public ICollection<Branch> Branches { get; set; }
        public Photo Image { get; set; }
    }
}