using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Users
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string DisplayName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler (DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _context = context;
            }

            public async Task<User> Handle (Command request, CancellationToken cancellationToken)
            {
                if (_context.Users.Where (user => user.Email == request.Email).Any ())
                {
                    throw new RestException (HttpStatusCode.BadRequest, new { Email = "Email Already Exists" });
                }
                
                if (_context.Users.Where (user => user.UserName == request.UserName).Any ())
                {
                    throw new RestException (HttpStatusCode.BadRequest, new { Username = "UserName already Exists" });
                }

                var newUser = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var result = await _userManager.CreateAsync(newUser, request.Password);
                
                if(result.Succeeded)
                {
                    return new User
                    {
                        DisplayName = newUser.UserName,
                        Username = newUser.UserName,
                        Image = null,
                        Token = _jwtGenerator.CreateToken(newUser),
                        
                    };
                }

                throw new Exception ("Problem Registering User");

            }
        }
    }
}