using System;
using CQRSApiVariant01.Infrastructure.MsSql;
using CQRSApiVariant01.Models;

namespace CQRSApiVariant01.Infrastructure.Repositories
{
    public class ProductsRepository : MsSqlDBRepository<Product>, IProductsRepository
    {
        public ProductsRepository(MsSqlDBContext context) : base(context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
        }
    }
}
