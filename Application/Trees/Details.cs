using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exceptions;
using MediatR;
using Persistence;

namespace Application.Trees
{
    public class Details
    {
        public class Query : IRequest<TreeDTO>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TreeDTO>
        {
            private readonly DataContext _context;

            public Handler (DataContext context)
            {
                _context = context;

            }
            public async Task<TreeDTO> Handle (Query request, CancellationToken cancellationToken)
            {
                var tree = await _context.Trees.FindAsync(request.Id);
                if(tree == null) throw new RestException(HttpStatusCode.NotFound, new {Tree = "Not Found"});

                var treeDTO = new TreeDTO
                {
                    Id = tree.Id,
                    Name = tree.Name,
                    Description = tree.Description,
                    IsPrivate = tree.IsPrivate,
                    CreatedAt = tree.CreatedAt,
                    UserRoots = null, //need to map this 
                    Branches = tree.Branches,
                    Image = tree.Image
                };
                return treeDTO;
            }
        }
    }
}