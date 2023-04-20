using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record ReviewUpdateDTO(Guid Id, string? Title = default, string? Description = default, string? Rating = default);
