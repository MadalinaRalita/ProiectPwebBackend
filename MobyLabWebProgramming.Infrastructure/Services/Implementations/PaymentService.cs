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
public class PaymentService : IPaymentService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public PaymentService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<PaymentDTO>> GetPayment(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new PaymentProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<PaymentDTO>.ForSuccess(result) :
            ServiceResponse<PaymentDTO>.FromError(CommonErrors.PaymentNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<PaymentDTO>>> GetPayments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new PaymentProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<PaymentDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddPayment(PaymentAddDTO payment, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add categories!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new PaymentSpec(payment.Amount), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "A payment with the same amount has already been made!", ErrorCodes.PaymentAlreadyExists));
        }

        await _repository.AddAsync(new Payment
        {
            Amount = payment.Amount,
            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdatePayment(PaymentUpdateDTO payment, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new PaymentSpec(payment.Id), cancellationToken);

        if (entity != null)
        {
            entity.Amount = payment.Amount ?? entity.Amount;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeletePayment(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {

        await _repository.DeleteAsync<Payment>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }
}
