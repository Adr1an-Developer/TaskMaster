using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
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

        public TaskService(ITaskRepository repository, IProjectRepository projectRepository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<DataResult<TaskOutputDTO>> Create(AddTaskDTO entity, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<TaskOutputDTO>();
            try
            {
                var item = await _repository.FindByConditionAsync(x => x.Title.Trim().ToLower() == entity.Title.Trim().ToLower()
                                                                 && x.IsDeleted == false
                                                                 && x.CreateByUser == userInfo.UserId);

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
                    CreateByUser = userInfo.UserId,
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
                    Comments = newRow.Comments
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
                        Status = x.Status
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
                    Title = row.Title
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
                    Title = row.Title
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
                        "Dados não encontrados."
                    };
                    return resultData;
                }

                entity.ModificationDate = DateTime.Now;
                entity.UpdateByUser = userInfo.UserId;

                await _repository.Update(entity);
                await _repository.UnitOfWork.SaveAsync();

                var result = new TaskOutputDTO()
                {
                    Description = entity.Description,
                    Id = entity.Id,
                    Priority = entity.Priority,
                    ChangeHistories = entity.ChangeHistories,
                    Comments = entity.Comments,
                    ProjectId = entity.ProjectId,
                    Status = entity.Status,
                    Title = entity.Title
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