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
public sealed class BookingProjectionSpec : BaseSpec<BookingProjectionSpec, Booking, BookingDTO>
{
    protected override Expression<Func<Booking, BookingDTO>> Spec => e => new()
    {
        Id = e.Id,
        CheckInDate = e.CheckInDate,
        CheckOutDate = e.CheckOutDate
    };

    public BookingProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public BookingProjectionSpec(Guid id) : base(id)
    {
    }

    public BookingProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.CheckInDate, searchExpr));
    }
}