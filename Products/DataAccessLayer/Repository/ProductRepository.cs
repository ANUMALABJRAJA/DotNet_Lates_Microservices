using System.Linq.Expressions;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
           await  _dbContext.SaveChangesAsync();
           return product;

        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            Product? product = await _dbContext.Products.FirstOrDefaultAsync(item => item.ProductId == productId);
            if(product == null)
            {
                return false;
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> condition)
        {
            return await _dbContext.Products.Where(condition).ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            Product? exisitingProduct = await _dbContext.Products.FirstOrDefaultAsync(item => item.ProductId == product.ProductId);
            if(exisitingProduct == null)
            {
                return null;
            }
            exisitingProduct.ProductName = product.ProductName;
            exisitingProduct.QuantityInStock = product.QuantityInStock;
            exisitingProduct.Category = product.Category;
            exisitingProduct.UnitPrice = product.UnitPrice;

            await _dbContext.SaveChangesAsync();

            return product;
        }
    }
}