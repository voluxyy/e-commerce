using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> Add(Comment comment);

        Task<Comment> Update(Comment comment);

        Task<int> Delete(int id);

        Task<Comment> Get(int id);

        List<Comment> GetAll();
    }
}