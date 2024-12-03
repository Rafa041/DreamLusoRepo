using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Users.Commands.UpdateAccess;

public class UpdateAccessCommand : IRequest<Result<UpdateAccessResponse, Success, Error>>
{
    public Guid UserId { get; set; }
    public Access Access { get; set; }
}
public class UpdateAccessResponse
{
    public Guid UserId { get; set; }
    public string Access { get; set; }
}
public class UpdateAccessCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateAccessCommand, Result<UpdateAccessResponse, Success, Error>>
{


    public async Task<Result<UpdateAccessResponse, Success, Error>> Handle(UpdateAccessCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId);

            if (user == null)
                return Error.NotFound;

            user.Access = Access.Blocked;

            await unitOfWork.UserRepository.UpdateAccessAsync(user.Id, user.Access, cancellationToken);
            await unitOfWork.CommitAsync();

            var response = new UpdateAccessResponse
            {
                UserId = user.Id,
                Access = user.Access.ToString()
            };

            return Success.Ok;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
            return Error.NotFound;
        }
    }
}