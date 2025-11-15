using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Users.Commands.CreateUser;
#nullable disable
public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository; 
    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User newUser = new User
        {
            Name = request.Name, 
            Email = request.Email
        }; 
        await _userRepository.AddAsync(newUser);
        return newUser.Id;
    }
}