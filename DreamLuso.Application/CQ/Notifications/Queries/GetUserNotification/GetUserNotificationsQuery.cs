using Azure.Core;
using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Queries.Retrieve;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Notifications.Queries.GetUserNotification;

public class GetUserNotificationsQuery : IRequest<Result<List<GetUserNotificationsQueryResponse>, Success, Error>>
{
    public Guid UserId { get; set; }
    public bool UnreadOnly { get; set; }
}
