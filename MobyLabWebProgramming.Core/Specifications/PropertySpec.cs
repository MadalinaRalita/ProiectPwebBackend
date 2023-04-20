using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class PropertySpec : BaseSpec<PropertySpec, Property>
{
    public PropertySpec(Guid id) : base(id)
    {
    }

    public PropertySpec(string title)
    {
        Query.Where(e => e.Title == title);
    }
}
