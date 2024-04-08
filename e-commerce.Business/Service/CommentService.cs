using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<CommentDto> Add(CommentDto dto)
        {
            Comment comment = DtoToModel(dto);
            await commentRepository.Add(comment);
            CommentDto commentDto = ModelToDto(comment);

            return commentDto;
        }

        public async Task<CommentDto> Update(CommentDto dto)
        {
            Comment comment = DtoToModel(dto);
            await commentRepository.Update(comment);
            CommentDto commentDto = ModelToDto(comment);

            return commentDto;
        }

        public async Task<int> Delete(int id)
        {
            return await commentRepository.Delete(id);
        }

        public async Task<CommentDto> Get(int id)
        {
            return ModelToDto(await commentRepository.Get(id));
        }

        public List<CommentDto> GetAll()
        {
            List<Comment> comments = commentRepository.GetAll();
            List<CommentDto> commentsDtos = ListModelToDto(comments);
            return commentsDtos;
        }

        private List<CommentDto> ListModelToDto(List<Comment> comments)
        {
            List<CommentDto> commentsDtos = new List<CommentDto>();
            foreach (Comment comment in comments)
            {
                commentsDtos.Add(ModelToDto(comment));
            }
            return commentsDtos;
        }

        private CommentDto ModelToDto(Comment comment)
        {
            CommentDto commentDto = new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Description = comment.Description,
                UserId = comment.UserId,
                ProductId = comment.ProductId
            };

            return commentDto;
        }

        private Comment DtoToModel(CommentDto commentDto)
        {
            Comment comment = new Comment
            {
                Id = commentDto.Id,
                Title = commentDto.Title,
                Description = commentDto.Description,
                UserId = commentDto.UserId,
                ProductId = commentDto.ProductId
            };

            return comment;
        }
    }
}