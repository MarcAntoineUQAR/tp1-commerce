using TPcommerce.Models;
using TPcommerce.Models.DTO;

namespace TPcommerce.Repository;

public class BaseRepository
{
    private ProductRepository _productRepository;

    public BaseRepository(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void PopulateDbContext()
    {
    }
}