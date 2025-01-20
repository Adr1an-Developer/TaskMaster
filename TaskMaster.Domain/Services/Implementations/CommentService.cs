using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Services.Implementations
{
    public class CommentService : ServiceBase<Comment>, ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IChangeHistoryRepository _changeHistoryRepository;

        private string _serviceName;

        public CommentService(ICommentRepository repository, IChangeHistoryRepository changeHistoryRepository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _changeHistoryRepository = changeHistoryRepository ?? throw new ArgumentNullException(nameof(changeHistoryRepository));
        }

        public async Task<DataResult<CommentOutputDTO>> Create(AddCommentDTO entity, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<CommentOutputDTO>();
            try
            {
                var newId = Guid.NewGuid().ToString();

                var newRow = new Comment()
                {
                    Id = newId,
                    TaskId = entity.TaskId,
                    Description = entity.Description,
                    CreateByUser = userInfo.userId,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.UtcNow,
                };

                var result = await _repository.CreateAsync(newRow);

                await _repository.UnitOfWork.SaveAsync();

                var resultNew = new CommentOutputDTO()
                {
                    TaskId = newRow.TaskId,
                    Description = newRow.Description,
                    Id = newRow.Id,
                    ModificationDate = newRow.ModificationDate,
                    CreateByUser = newRow.CreateByUser,
                    UpdateByUser = newRow.UpdateByUser,
                    CreationDate = newRow.CreationDate,
                    IsActive = newRow.IsActive,
                    IsDeleted = newRow.IsDeleted,
                };

                resultData.messageType = nameof(MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = newId;
                resultData.result = resultNew;
                resultData.messages = new List<string>()
                {
                    "Dados salvos com sucesso"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                resultData.messageType = nameof(MessageTypeResultEnum.Error);
                resultData.error = true;
                resultData.result = null;
                resultData.messages = new List<string>()
                {
                    ex.Message
                };
                return resultData;
            }
        }

        public async Task<DataResult<CommentOutputDTO>> Delete(string id, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<CommentOutputDTO>();
            try
            {
                var row = await _repository.GetById(id);

                row.Delete();

                await _repository.Update(row);
                await _repository.UnitOfWork.SaveAsync();

                var result = new CommentOutputDTO()
                {
                    Description = row.Description,
                    Id = row.Id,
                    TaskId = row.TaskId,
                    ModificationDate = row.ModificationDate,
                    CreateByUser = row.CreateByUser,
                    UpdateByUser = row.UpdateByUser,
                    CreationDate = row.CreationDate,
                    IsActive = row.IsActive,
                    IsDeleted = row.IsDeleted,
                };

                resultData.messageType = nameof(MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = row.Id.ToString();
                resultData.result = result;
                resultData.messages = new List<string>()
                {
                    $"Comentario excluído com sucesso"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                resultData.messageType = nameof(MessageTypeResultEnum.Error);
                resultData.error = true;
                resultData.result = null;
                resultData.messages = new List<string>()
                {
                    ex.Message
                };
                return resultData;
            }
        }

        public async Task<DataResults<CommentOutputDTO>> GetAll(string taskId, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResults<CommentOutputDTO>();
            try
            {
                var rows = await _repository.GetAll(taskId);

                if (!rows.Any())
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = false;
                    resultData.results = null;
                    resultData.messages = new List<string>()
                    {
                        "Dados não encontrados."
                    };
                    return resultData;
                }

                var results = rows.Select(x =>

                    new CommentOutputDTO
                    {
                        TaskId = x.TaskId,
                        Description = x.Description,
                        Id = x.Id,
                        ModificationDate = x.ModificationDate,
                        CreateByUser = x.CreateByUser,
                        UpdateByUser = x.UpdateByUser,
                        CreationDate = x.CreationDate,
                        IsActive = x.IsActive,
                        IsDeleted = x.IsDeleted,
                    }
                ).ToList();

                resultData.totalRecords = rows.Count();
                resultData.messageType = nameof(MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.results = results;
                resultData.messages = new List<string>()
                {
                    $"{rows.Count()} Dados encontrados"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                resultData.messageType = nameof(MessageTypeResultEnum.Error);
                resultData.error = true;
                resultData.results = null;
                resultData.messages = new List<string>()
                {
                    ex.Message
                };
                return resultData;
            }
        }

        public async Task<DataResult<CommentOutputDTO>> GetbyId(string id, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<CommentOutputDTO>();
            try
            {
                var row = await _repository.GetById(id);

                if (row == null)
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = false;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                        "Dados não encontrados."
                    };
                    return resultData;
                }
                var result = new CommentOutputDTO()
                {
                    TaskId = row.TaskId,
                    Description = row.Description,
                    Id = row.Id,
                    ModificationDate = row.ModificationDate,
                    CreateByUser = row.CreateByUser,
                    UpdateByUser = row.UpdateByUser,
                    CreationDate = row.CreationDate,
                    IsActive = row.IsActive,
                    IsDeleted = row.IsDeleted,
                };
                resultData.messageType = nameof(MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.result = result;
                resultData.messages = new List<string>()
                {
                    "Dado encontrado"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                resultData.messageType = nameof(MessageTypeResultEnum.Error);
                resultData.error = true;
                resultData.result = null;
                resultData.messages = new List<string>()
                {
                    ex.Message
                };
                return resultData;
            }
        }

        public async Task<DataResult<CommentOutputDTO>> Update(Comment entity, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<CommentOutputDTO>();
            try
            {
                entity.ModificationDate = DateTime.Now;
                entity.UpdateByUser = userInfo.userId;

                await _repository.Update(entity);
                await _repository.UnitOfWork.SaveAsync();

                var newId = Guid.NewGuid().ToString();

                var history = new ChangeHistory
                {
                    Id = newId,
                    CreateByUser = userInfo.userId,
                    ChangeDetails = $"Um comentário foi adicionado à tarefa.",
                    TaskId = entity.Id,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.UtcNow,
                };
                await _changeHistoryRepository.CreateAsync(history);
                await _changeHistoryRepository.UnitOfWork.SaveAsync();

                var result = new CommentOutputDTO()
                {
                    TaskId = entity.TaskId,
                    Description = entity.Description,
                    Id = entity.Id,
                    ModificationDate = entity.ModificationDate,
                    CreateByUser = entity.CreateByUser,
                    UpdateByUser = entity.UpdateByUser,
                    CreationDate = entity.CreationDate,
                    IsActive = entity.IsActive,
                    IsDeleted = entity.IsDeleted,
                };

                resultData.messageType = nameof(MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = entity.Id;
                resultData.result = result;
                resultData.messages = new List<string>()
                {
                    "Dados atualizados com sucesso"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                resultData.messageType = nameof(MessageTypeResultEnum.Error);
                resultData.error = true;
                resultData.result = null;
                resultData.messages = new List<string>()
                {
                    ex.Message
                };
                return resultData;
            }
        }
    }
}