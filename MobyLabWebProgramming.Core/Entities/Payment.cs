using MobyLabWebProgramming.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Payment : BaseEntity
{
    public string Amount { get; set; } = default!;
    // Many-to-one realtionship with User
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
