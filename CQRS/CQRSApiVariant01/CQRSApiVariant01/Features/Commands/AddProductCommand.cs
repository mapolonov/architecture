using System;
using System.Threading;
using System.Threading.Tasks;
using CQRSApiVariant01.Infrastructure.Repositories;
using CQRSApiVariant01.Models;
using FluentValidation;
using MediatR;

namespace CQRSApiVariant01.Features.Commands
{
    public class AddProductCommand : IRequest<Product>
    {
        public string Alias { get; set; }

        public string Name { get; set; }

        public ProductType Type { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
        {
            private readonly IProductsRepository _productsRepository;

            public AddProductCommandHandler(IProductsRepository productsRepository) 
                => _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));

            public async Task<Product> Handle(AddProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product {Alias = command.Alias, Name = command.Name, Type = command.Type};

                await _productsRepository.Add(product);
                return product;
            }
        }

        public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
        {
            public AddProductCommandValidator()
            {
                RuleFor(c => c.Name).NotEmpty();
                RuleFor(c => c.Alias).NotEmpty();
            }
        }
    }
}
