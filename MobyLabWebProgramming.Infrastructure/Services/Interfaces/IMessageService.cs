using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;
public interface IMessageService
{
    public Task<ServiceResponse<MessageDTO>> GetMessage(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<MessageDTO>>> GetMessages(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddMessage(MessageAddDTO message, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateMessage(MessageUpdateDTO message, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteMessage(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
