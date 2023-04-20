using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IBookingService
{
    /// <summary>
    /// GetBooking will provide the information about a booking given its booking Id.
    /// </summary>
    public Task<ServiceResponse<BookingDTO>> GetBooking(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetBookings returns page with booking information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<BookingDTO>>> GetBookings(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// CreateBooking creates a new booking for a property and verifies if requesting user has permissions to add one.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> CreateBooking(BookingCreateDTO booking, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateBooking updates a booking and verifies if requesting user has permissions to update it.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateBooking(BookingUpdateDTO booking, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// CancelBooking cancels a booking and verifies if requesting user has permissions to cancel it.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> CancelBooking(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
