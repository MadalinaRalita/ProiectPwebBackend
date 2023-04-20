using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;
/// <summary>
/// This DTO is used to update a booking, the properties besides the id are nullable to indicate that they may not be updated if they are null.
/// </summary>
public record BookingUpdateDTO(Guid Id, string? CheckInDate = default, string? CheckOutDate = default);
