using Microsoft.AspNetCore.Mvc;
using SQLiteApplication.Repos;

namespace SQLiteApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductRepository userRepository) : ControllerBase
{

    readonly IProductRepository _productRepository = userRepository;

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        List<DTO.ProductDTO> value = await _productRepository.GetAll();
        return Ok(value);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProductParameters userParameter)
    {
        _productRepository.Add(new DTO.ProductDTO
        {
            Description = userParameter.Description,
            Value = userParameter.Value
        });

        return Ok(userParameter);
    }
}
public record ProductParameters(string Description, double Value);
