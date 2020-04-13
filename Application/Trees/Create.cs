using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Trees
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsPrivate { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler (DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle (Command request, CancellationToken cancellationToken)
            {

                var newTree = new Tree
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    IsPrivate = request.IsPrivate,
                    CreatedAt = DateTime.Now
                };

                var userName = _userAccessor.GetCurrentUsername ();
                var currentUser = await _context.Users.SingleOrDefaultAsync (u => u.UserName == userName);

                var newTreeRoot = new UserRoot
                {
                    AppUser = currentUser,
                    Tree = newTree,
                    IsPlanter = true,
                    DateJoined = DateTime.Now
                };

                newTree.UserRoots = new List<UserRoot> { newTreeRoot };

                _context.Trees.Add (newTree);

                //logic for adding here 
                var IsSuccessful = await _context.SaveChangesAsync () > 0;

                if (IsSuccessful) return Unit.Value;

                throw new Exception ("Problem Creating New Activity");

            }
        }
    }
}