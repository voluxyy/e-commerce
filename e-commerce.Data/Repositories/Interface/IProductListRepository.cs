using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IProductListRepository
    {
        Task<ProductList> Add(ProductList productList);

        Task<ProductList> Update(ProductList productList);

        Task<int> Delete(int id);
        
        Task<int> DeleteFromProduct(int id);

        Task<ProductList> Get(int id);

        Task<List<ProductList>> GetFromShoppingCart(int id);

        List<ProductList> GetAll();
    }
}