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
public sealed class PaymentProjectionSpec : BaseSpec<PaymentProjectionSpec, Payment, PaymentDTO>
{
    protected override Expression<Func<Payment, PaymentDTO>> Spec => e => new()
    {
        Id = e.Id,
        Amount = e.Amount
    };

    public PaymentProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public PaymentProjectionSpec(Guid id) : base(id)
    {
    }

    public PaymentProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Amount, searchExpr));
    }
}