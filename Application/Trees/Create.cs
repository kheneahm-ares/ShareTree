using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
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

            public Handler (DataContext context)
            {
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

                //logic for adding here 
                var IsSuccessful = await _context.SaveChangesAsync () > 0;

                if (IsSuccessful) return Unit.Value;

                throw new Exception ("Problem");

            }
        }
    }
}