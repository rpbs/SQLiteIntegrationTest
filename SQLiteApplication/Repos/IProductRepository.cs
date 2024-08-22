using SQLiteApplication.DTO;

namespace SQLiteApplication.Repos;

public interface IProductRepository
{
    Task Add(ProductDTO productDTO);

    Task<List<ProductDTO>> GetAll();
}
