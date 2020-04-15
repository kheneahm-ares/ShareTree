using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.Trees
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var treeToDelete = await _context.Trees.FindAsync(request.Id);
                if (treeToDelete == null) throw new RestException(HttpStatusCode.NotFound, new {Tree = "Not Found"});
                
                //get userroot for current user
                var currentUserRoot = treeToDelete.UserRoots.Where(ur => ur.AppUser.UserName == _userAccessor.GetCurrentUsername()).SingleOrDefault();
                if(currentUserRoot == null || !currentUserRoot.IsPlanter) throw new RestException(HttpStatusCode.Unauthorized, new {Tree = "Unauthorized to Delete Tree"});

                _context.Trees.Remove(treeToDelete);

                //logic for adding here 
                var IsSuccessful = await _context.SaveChangesAsync() > 0;

                if (IsSuccessful) return Unit.Value;

                throw new Exception("Problem Deleting Tree");

            }
        }
    }
}