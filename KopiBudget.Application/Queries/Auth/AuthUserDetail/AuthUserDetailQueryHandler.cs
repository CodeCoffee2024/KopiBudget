using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Auth.AuthUserDetail
{
    public class AuthUserDetailQueryHandler(
        IUserRepository _userRepository,
        IMapper _mapper
    ) : IRequestHandler<AuthUserDetailQuery, Result<UserDetailDto>>
    {
        #region Public Methods
        public async Task<Result<UserDetailDto>> Handle(AuthUserDetailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return Result.Failure<UserDetailDto>(Error.Notfound("User"));
            }

            var dto = _mapper.Map<UserDetailDto>(user);
            return Result.Success(dto);
        }

        #endregion Public Methods
    }
}