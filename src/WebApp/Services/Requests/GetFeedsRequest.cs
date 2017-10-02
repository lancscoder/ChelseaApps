using MediatR;
using System.Collections.Generic;
using WebApp.Models.Responses;

namespace WebApp.Services.Requests
{
    public class GetFeedsRequest : IRequest<List<Feed>>
    {
        public int[] Ids { get; set; }
    }
}
