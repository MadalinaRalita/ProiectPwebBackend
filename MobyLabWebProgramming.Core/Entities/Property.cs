using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Property : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Address { get; set; } = default!;

    // One-to-many relation with "Booking"
    //public ICollection<Booking> Bookings { get; set; } = default!;

    // One-to-many relation with "Review"
    /*public ICollection<Review> Reviews { get; set; } = default!;*/

    // Many-to-one relationship with "User"
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
