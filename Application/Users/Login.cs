using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users
{
    public class Login
    {
        public class Query : IRequest<User>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManger;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler (UserManager<AppUser> userManager, SignInManager<AppUser> signInManger, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _signInManger = signInManger;
                _userManager = userManager;

            }
            public async Task<User> Handle (Query request, CancellationToken cancellationToken)
            {

                var loggingInUser = await _userManager.FindByEmailAsync (request.Email);

                if (loggingInUser == null) throw new RestException (HttpStatusCode.Unauthorized); //don't give details

                var result = await _signInManger.CheckPasswordSignInAsync (loggingInUser, request.Password, false);

                if (result.Succeeded)
                {
                    return new User
                    {
                        DisplayName = loggingInUser.UserName,
                        Username = loggingInUser.UserName,
                        Image = null,
                        Token = _jwtGenerator.CreateToken (loggingInUser),
                    };
                }
                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}