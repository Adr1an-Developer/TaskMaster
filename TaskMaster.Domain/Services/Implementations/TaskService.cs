using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Services.Implementations
{
    public class TaskService : ServiceBase<Entities.Master.Task>, ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly IChangeHistoryRepository _changeHistoryRepository;

        public TaskService(ITaskRepository repository, IProjectRepository projectRepository, IChangeHistoryRepository changeHistoryRepository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _changeHistoryRepository = changeHistoryRepository ?? throw new ArgumentNullException(nameof(changeHistoryRepository));
        }

        public async Task<DataResult<TaskOutputDTO>> Create(AddTaskDTO entity, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<TaskOutputDTO>();
            try
            {
                var item = await _repository.FindByConditionAsync(x => x.Title.Trim().ToLower() == entity.Title.Trim().ToLower()
                                                                 && x.IsDeleted == false
                                                                 && x.CreateByUser == userInfo.userId);

                if (item.Any())
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"A tarefa {entity.Title} já existe"
                    };
                    return resultData;
                }

                var projectInfo = await _projectRepository.GetById(entity.ProjectId);

                if (!projectInfo.CanCreate)
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"O projeto {entity.Title} tem 20 tarefas atribuídas."
                    };
                    return resultData;
                }

                if (!Helpers.Validation.EnumValidator.IsTaskStatusValid(entity.Status))
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"Os status permitidos são  Pendente, EmAndamento, Concluído"
                    };
                    return resultData;
                }

                if (!Helpers.Validation.EnumValidator.IsTaskPriorityValid(entity.Priority))
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"As prioridades permitidas são Baixa, Média,  Alta"
                    };
                    return resultData;
                }

                var newId = Guid.NewGuid().ToString();

                var newRow = new Entities.Master.Task()
                {
                    Id = newId,
                    Title = entity.Title,
                    Status = entity.Status,
                    Priority = entity.Priority,
                    ProjectId = entity.ProjectId,
                    Description = entity.Description,
                    CreateByUser = userInfo.userId,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.UtcNow,
                };

                var result = await _repository.CreateAsync(newRow);

                await _repository.UnitOfWork.SaveAsync();

                var resultNew = new TaskOutputDTO()
                {
                    Description = newRow.Description,
                    Id = newRow.Id,
                    ProjectId = newRow.ProjectId,
                    Priority = newRow.Priority,
                    Status = newRow.Status,
                    Title = newRow.Title,
                    ChangeHistories = newRow.ChangeHistories,
                    Comments = newRow.Comments,
                    CreateByUser = newRow.CreateByUser,
                    IsDeleted = newRow.IsDeleted,
                    CreationDate = newRow.CreationDate,
                    IsActive = newRow.IsActive,
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

        public async Task<DataResult<TaskOutputDTO>> Delete(string id, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<TaskOutputDTO>();
            try
            {
                var row = await _repository.GetById(id);

                row.Delete();

                await _repository.Update(row);
                await _repository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = row.Id.ToString();
                resultData.result = null;
                resultData.messages = new List<string>()
                {
                    $"A tarefa {row.Title} excluída com sucesso"
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

        public async Task<DataResults<TaskOutputDTO>> GetAll(string projectId, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResults<TaskOutputDTO>();
            try
            {
                var rows = await _repository.GetAll(projectId);

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

                    new TaskOutputDTO
                    {
                        Description = x.Description,
                        Id = x.Id,
                        Title = x.Title,
                        ProjectId = x.ProjectId,
                        ChangeHistories = x.ChangeHistories,
                        Comments = x.Comments,
                        Priority = x.Priority,
                        Status = x.Status,
                        CreateByUser = x.CreateByUser,
                        IsDeleted = x.IsDeleted,
                        CreationDate = x.CreationDate,
                        IsActive = x.IsActive,
                        ModificationDate = x.ModificationDate,
                        UpdateByUser = x.UpdateByUser,
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

        public async Task<DataResult<TaskOutputDTO>> GetbyId(string id, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<TaskOutputDTO>();
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
                var result = new TaskOutputDTO()
                {
                    Description = row.Description,
                    Id = row.Id,
                    Status = row.Status,
                    Priority = row.Priority,
                    Comments = row.Comments,
                    ChangeHistories = row.ChangeHistories,
                    ProjectId = row.ProjectId,
                    Title = row.Title,
                    CreateByUser = row.CreateByUser,
                    IsDeleted = row.IsDeleted,
                    CreationDate = row.CreationDate,
                    IsActive = row.IsActive,
                    UpdateByUser = row.UpdateByUser,
                    ModificationDate = row.ModificationDate,
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

        public async Task<DataResult<TaskOutputDTO>> GetByTitle(string title, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<TaskOutputDTO>();
            try
            {
                var row = await _repository.GetByTitle(title);

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
                var result = new TaskOutputDTO()
                {
                    Description = row.Description,
                    Id = row.Id,
                    Status = row.Status,
                    Priority = row.Priority,
                    Comments = row.Comments,
                    ChangeHistories = row.ChangeHistories,
                    ProjectId = row.ProjectId,
                    Title = row.Title,
                    ModificationDate = row.ModificationDate,
                    UpdateByUser = row.UpdateByUser,
                    IsActive = row.IsActive,
                    CreationDate = row.CreationDate,
                    IsDeleted = row.IsDeleted,
                    CreateByUser = row.CreateByUser
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

        public async Task<DataResult<TaskOutputDTO>> Update(Entities.Master.Task entity, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<TaskOutputDTO>();
            try
            {
                if (!Helpers.Validation.EnumValidator.IsTaskStatusValid(entity.Status))
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"Os status permitidos são  Pendente, EmAndamento, Concluído"
                    };
                    return resultData;
                }

                if (!Helpers.Validation.EnumValidator.IsTaskPriorityValid(entity.Priority))
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"As prioridades permitidas são Baixa, Média,  Alta"
                    };
                    return resultData;
                }

                var oldData = await _repository.GetById(entity.Id);

                if (!oldData.Priority.Equals(entity.Priority))
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = false;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                        "Após criar a tarefa, a prioridade não pode ser modificada."
                    };
                    return resultData;
                }

                entity.ModificationDate = DateTime.UtcNow;
                entity.UpdateByUser = userInfo.userId;

                await _repository.Update(entity);
                await _repository.UnitOfWork.SaveAsync();

                var NewData = await _repository.GetById(oldData.Id);

                var ChangeHistory = String.Join("; ", NewData.CompareWith(oldData));
                if (!string.IsNullOrEmpty(ChangeHistory))
                {
                    var newId = Guid.NewGuid().ToString();

                    var history = new ChangeHistory
                    {
                        Id = newId,
                        CreateByUser = userInfo.userId,
                        ChangeDetails = ChangeHistory,
                        TaskId = oldData.Id,
                        IsActive = true,
                        IsDeleted = false,
                        CreationDate = DateTime.UtcNow,
                    };
                    await _changeHistoryRepository.CreateAsync(history);
                    await _changeHistoryRepository.UnitOfWork.SaveAsync();
                }

                var newHistory = await _changeHistoryRepository.GetAll(oldData.Id);

                var result = new TaskOutputDTO()
                {
                    Description = NewData.Description,
                    Id = NewData.Id,
                    Priority = NewData.Priority,
                    ChangeHistories = newHistory.ToList(),
                    Comments = NewData.Comments,
                    ProjectId = NewData.ProjectId,
                    Status = NewData.Status,
                    Title = NewData.Title,
                    CreateByUser = NewData.CreateByUser,
                    CreationDate = NewData.CreationDate,
                    IsDeleted = NewData.IsDeleted,
                    IsActive = NewData.IsActive,
                    UpdateByUser = NewData.UpdateByUser,
                    ModificationDate = NewData.ModificationDate
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