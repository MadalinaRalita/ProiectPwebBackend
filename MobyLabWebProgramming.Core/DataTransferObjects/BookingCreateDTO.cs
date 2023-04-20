using MobyLabWebProgramming.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class BookingCreateDTO
{
    public string CheckInDate { get; set; } = default!;
    public string CheckOutDate { get; set; } = default!;
}
