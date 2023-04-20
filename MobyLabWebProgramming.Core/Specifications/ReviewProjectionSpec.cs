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
public sealed class ReviewProjectionSpec : BaseSpec<ReviewProjectionSpec, Review, ReviewDTO>
{
    protected override Expression<Func<Review, ReviewDTO>> Spec => e => new()
    {
        Id = e.Id,
        Title = e.Title,
        Description = e.Description,
        Rating = e.Rating
    };

    public ReviewProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ReviewProjectionSpec(Guid id) : base(id)
    {
    }

    public ReviewProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Title, searchExpr));
    }
}