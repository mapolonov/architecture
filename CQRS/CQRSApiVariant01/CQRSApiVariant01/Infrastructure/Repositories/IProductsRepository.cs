using CQRSApiVariant01.Infrastructure.MsSql;
using CQRSApiVariant01.Models;

namespace CQRSApiVariant01.Infrastructure.Repositories
{
	public interface IProductsRepository : IRepository<Product>
	{
		//Task<IEnumerable<Product>> Get(int? take, int skip = 0, FilterDefinition<Product> filter = null,
		//	SortDefinition<Product> sort = null);
	}
}
