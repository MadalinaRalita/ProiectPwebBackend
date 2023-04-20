using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;
public interface IReviewService
{
    public Task<ServiceResponse<ReviewDTO>> GetReview(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<ReviewDTO>>> GetReviews(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddReview(ReviewAddDTO review, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateReview(ReviewUpdateDTO review, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteReview(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
