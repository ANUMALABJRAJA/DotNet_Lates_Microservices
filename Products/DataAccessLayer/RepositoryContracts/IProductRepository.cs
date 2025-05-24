using System.Linq.Expressions;
using DataAccessLayer.Entities;
using Org.BouncyCastle.Crypto.Operators;

namespace DataAccessLayer.RepositoryContracts{
    public interface IProductRepository{
        Task<IEnumerable<Product>> GetProducts();

       Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product,bool>> condition);

       Task<Product?> GetProductByCondition(Expression<Func<Product,bool>> condition);

       Task<Product> AddProduct(Product product);

       Task<Product?> UpdateProduct(Product product);

       Task<bool> DeleteProduct(Guid ProductId);
    }
}