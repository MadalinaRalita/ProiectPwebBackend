using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;
public class ReviewService : IReviewService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public ReviewService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ReviewDTO>> GetReview(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ReviewProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<ReviewDTO>.ForSuccess(result) :
            ServiceResponse<ReviewDTO>.FromError(CommonErrors.ReviewNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<ReviewDTO>>> GetReviews(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ReviewProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<ReviewDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddReview(ReviewAddDTO review, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add categories!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new ReviewSpec(review.Title), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "A Review with the same title already exists!", ErrorCodes.ReviewAlreadyExists));
        }

        await _repository.AddAsync(new Review
        {
            Title = review.Title,
            Description = review.Description,
            Rating = review.Rating,
            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateReview(ReviewUpdateDTO review, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new ReviewSpec(review.Id), cancellationToken);

        if (entity != null)
        {
            entity.Title = review.Title ?? entity.Title;
            entity.Description = review.Description ?? entity.Description;
            entity.Rating = review.Rating ?? entity.Rating;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteReview(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {

        await _repository.DeleteAsync<Review>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }
}
