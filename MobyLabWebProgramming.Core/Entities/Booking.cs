using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Booking : BaseEntity
{
    public string CheckInDate { get; set; } = default!;
    public string CheckOutDate { get; set; } = default!;

    // Many-to-one relationship with Property
    /*public Guid PropertyId { get; set; }
    public Property Property { get; set; } = default!;*/

    // Many-to-one relationship with User
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
