using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface ICommentService
    {
        Task<CommentDto> Add(CommentDto dto);

        Task<CommentDto> Update(CommentDto dto);

        Task<int> Delete(int id);

        Task<CommentDto> Get(int id);

        List<CommentDto> GetAll();
    }
}