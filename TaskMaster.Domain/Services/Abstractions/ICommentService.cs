using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Services.Abstractions
{
    public interface ICommentService : IServiceBase<Comment>
    {
        Task<DataResults<CommentOutputDTO>> GetAll(string taskId, UserLogged userInfo);

        Task<DataResult<CommentOutputDTO>> GetbyId(string id, UserLogged userInfo);

        Task<DataResult<CommentOutputDTO>> Create(AddCommentDTO entity, UserLogged userInfo);

        Task<DataResult<CommentOutputDTO>> Update(Comment entity, UserLogged userInfo);

        Task<DataResult<CommentOutputDTO>> Delete(string id, UserLogged userInfo);
    }
}