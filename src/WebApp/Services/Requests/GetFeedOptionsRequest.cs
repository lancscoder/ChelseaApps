using MediatR;
using System.Collections.Generic;
using WebApp.Models.Responses;

namespace WebApp.Services.Requests
{
    public class GetFeedOptionsRequest : IRequest<List<FeedOption>>
    {
    }
}
