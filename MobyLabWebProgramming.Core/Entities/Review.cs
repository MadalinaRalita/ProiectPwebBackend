using MobyLabWebProgramming.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Review : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Rating { get; set; } = default!;

    // Many-to-one relation with "Property"
    /*public Guid PropertyId { get; set; }
    public Property Property { get; set; } = default!;*/

    // Many-to-one relation with "User"
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
