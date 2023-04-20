using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class PaymentDTO
{
    public Guid Id { get; set; }
    public string Amount { get; set; } = default!;
}
