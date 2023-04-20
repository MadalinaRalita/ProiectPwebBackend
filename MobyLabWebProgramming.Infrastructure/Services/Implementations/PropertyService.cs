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
public class PropertyService : IPropertyService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public PropertyService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<PropertyDTO>> GetProperty(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new PropertyProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<PropertyDTO>.ForSuccess(result) :
            ServiceResponse<PropertyDTO>.FromError(CommonErrors.PropertyNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<PropertyDTO>>> GetProperties(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new PropertyProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<PropertyDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddProperty(PropertyAddDTO Property, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add categories!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new PropertySpec(Property.Title), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "A Property with the same title already exists!", ErrorCodes.PropertyAlreadyExists));
        }

        await _repository.AddAsync(new Property
        {
            Title = Property.Title,
            Description = Property.Description,
            Address = Property.Address,
            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProperty(PropertyUpdateDTO Property, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new PropertySpec(Property.Id), cancellationToken);

        if (entity != null)
        {
            entity.Title = Property.Title ?? entity.Title;
            entity.Description = Property.Description ?? entity.Description;
            entity.Address = Property.Address ?? entity.Address;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProperty(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {

        await _repository.DeleteAsync<Property>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }
}
