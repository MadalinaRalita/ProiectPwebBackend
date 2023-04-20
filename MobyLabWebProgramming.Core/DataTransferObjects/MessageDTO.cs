using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class MessageDTO
{
    public Guid Id { get; set; }
    public string Text { get; set; } = default!;
}
