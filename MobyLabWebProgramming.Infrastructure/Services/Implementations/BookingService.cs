using System.Net;
using MobyLabWebProgramming.Core.Constants;
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

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public BookingService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<BookingDTO>> GetBooking(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new BookingProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ?
            ServiceResponse<BookingDTO>.ForSuccess(result) :
            ServiceResponse<BookingDTO>.FromError(CommonErrors.BookingNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<BookingDTO>>> GetBookings(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new BookingProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<BookingDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> CreateBooking(BookingCreateDTO booking, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add categories!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new BookingSpec(booking.CheckInDate), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "A booking for the selected check-in date has already been placed!", ErrorCodes.BookingAlreadyExists));
        }

        await _repository.AddAsync(new Booking
        {
            CheckInDate = booking.CheckInDate,
            CheckOutDate = booking.CheckOutDate,
            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateBooking(BookingUpdateDTO booking, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new BookingSpec(booking.Id), cancellationToken);

        if (entity != null) // Verify if the booking is not found, you cannot update an non-existing entity.
        {
            entity.CheckInDate = booking.CheckInDate ?? entity.CheckInDate;
            entity.CheckOutDate = booking.CheckOutDate ?? entity.CheckOutDate;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> CancelBooking(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {

        await _repository.DeleteAsync<Booking>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }
}

