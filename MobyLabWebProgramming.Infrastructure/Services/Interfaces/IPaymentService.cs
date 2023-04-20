using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;
public interface IPaymentService
{
    public Task<ServiceResponse<PaymentDTO>> GetPayment(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<PaymentDTO>>> GetPayments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddPayment(PaymentAddDTO payment, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdatePayment(PaymentUpdateDTO payment, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeletePayment(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
