using ErrorHandlingProblemDetails.Data.Context;
using ErrorHandlingProblemDetails.Data.Models;
using ErrorHandlingProblemDetails.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErrorHandlingProblemDetails.Services
{
    public class ProductService : IProductService
    {
        protected readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllPrpoducts() => await _context.Products.ToListAsync();

        public async Task<Product> GetProductById(int id) => await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product> GetProductByName(string name) => await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

        public async Task CreateNewProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}
