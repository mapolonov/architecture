using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using CQRSApiVariant01.Infrastructure.Repositories;
using CQRSApiVariant01.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CQRSApiVariant01.Features.Queries
{
    public class GetProductsQuery : IRequest<DataWithTotal<Product>>
    {
        [DefaultValue(10)]
        [FromQuery]
        public int PageSize { get; set; } = 10;

        [DefaultValue(0)]
        [FromQuery]
        public int PageIndex { get; set; } = 0;

        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, DataWithTotal<Product>>
        {
            private readonly ILogger<GetProductsQueryHandler> _logger;
            private readonly IProductsRepository _productsRepository;

            public GetProductsQueryHandler(IProductsRepository productsRepository,
                ILogger<GetProductsQueryHandler> logger)
            {
                _productsRepository = productsRepository;
                _logger = logger;
            }


            public async Task<DataWithTotal<Product>> Handle(GetProductsQuery query,
                CancellationToken cancellationToken)
            {
                var products =
                    await _productsRepository.GetAll().ToListAsync(cancellationToken: cancellationToken);
                long total = products.Count;

                return new DataWithTotal<Product>(products, (int)total);
            }
        }

        //public class GetProductsQueryValidator : PagingValidator<GetProductsQuery>
        //{
        //}
    }
}
