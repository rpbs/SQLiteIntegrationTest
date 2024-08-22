using Microsoft.EntityFrameworkCore;
using SQLiteApplication.DTO;
using SQLiteApplication.Entity;

namespace SQLiteApplication.Repos
{
    public class ProductRepository(ApplicationContext conserviceContext) : IProductRepository
    {
        public ApplicationContext _conserviceContext = conserviceContext;

        public async Task Add(ProductDTO productDTO)
        {
            _conserviceContext.Products.Add(new Product(productDTO.Description, productDTO.Value));

            await _conserviceContext.SaveChangesAsync();
        }

        public async Task<List<ProductDTO>> GetAll() => (await _conserviceContext.Products.ToListAsync()).Select(x => new ProductDTO
        {
            Id = x.Id,
            Value = x.Value,
            Description = x.Description,
        }).ToList();
    }
}
