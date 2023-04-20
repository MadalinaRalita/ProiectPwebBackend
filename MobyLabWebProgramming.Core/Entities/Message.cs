using MobyLabWebProgramming.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;
public class Message : BaseEntity
{
    public string Text { get; set; } = default!;

    // Many-to-one relationship with "User"
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
