using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Master;

namespace TaskMaster.Domain.Services.Implementations
{
    public class ProjectService : ServiceBase<Project>, IProjectService
    {
        private readonly IProjectRepository _repository;

        private string _serviceName;

        public ProjectService(IProjectRepository repository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<DataResult<ProjectOutputDTO>> Create(AddProjectDTO entity, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<ProjectOutputDTO>();
            try
            {
                var item = await _repository.FindByConditionAsync(x => x.Name.Trim().ToLower() == entity.Name.Trim().ToLower()
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
                       $"O projeto {entity.Name} já existe"
                    };
                    return resultData;
                }

                var newId = Guid.NewGuid().ToString();

                var newRow = new Project()
                {
                    Id = newId,
                    Name = entity.Name,
                    Description = entity.Description,
                    CreateByUser = userInfo.userId,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.UtcNow,
                };

                var result = await _repository.CreateAsync(newRow);

                await _repository.UnitOfWork.SaveAsync();

                var resultNew = new ProjectOutputDTO()
                {
                    AllCompleted = newRow.AllCompleted,
                    CanCreate = newRow.CanCreate,
                    Description = newRow.Description,
                    Id = newRow.Id,
                    Name = newRow.Name,
                    Tasks = newRow.Tasks,
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

        public async Task<DataResult<ProjectOutputDTO>> Delete(string id, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<ProjectOutputDTO>();
            try
            {
                var row = await _repository.GetById(id);

                if (!row.AllCompleted)
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"O projeto {row.Name} não pode ser excluído se todas as suas tarefas não forem concluídas. Conclua as tarefas ou exclua-as antes de excluir o projeto."
                    };
                    return resultData;
                }

                row.Delete();

                await _repository.Update(row);
                await _repository.UnitOfWork.SaveAsync();

                var result = new ProjectOutputDTO()
                {
                    AllCompleted = true,
                    CanCreate = true,
                    Description = row.Description,
                    Id = row.Id,
                    Name = row.Name,
                    Tasks = row.Tasks,
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
                    $"Projeto {row.Name} excluído com sucesso"
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

        public async Task<DataResults<ProjectOutputDTO>> GetAll(UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResults<ProjectOutputDTO>();
            try
            {
                var rows = await _repository.GetAll();

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

                    new ProjectOutputDTO
                    {
                        AllCompleted = x.AllCompleted,
                        CanCreate = x.CanCreate,
                        Description = x.Description,
                        Id = x.Id,
                        Name = x.Name,
                        Tasks = x.Tasks,
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

        public async Task<DataResult<ProjectOutputDTO>> GetbyId(string id, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<ProjectOutputDTO>();
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
                var result = new ProjectOutputDTO()
                {
                    AllCompleted = row.AllCompleted,
                    CanCreate = row.CanCreate,
                    Description = row.Description,
                    Id = row.Id,
                    Name = row.Name,
                    Tasks = row.Tasks,
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

        public async Task<DataResult<ProjectOutputDTO>> GetByName(string name, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<ProjectOutputDTO>();
            try
            {
                var row = await _repository.GetByName(name);

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
                var result = new ProjectOutputDTO()
                {
                    AllCompleted = row.AllCompleted,
                    CanCreate = row.CanCreate,
                    Description = row.Description,
                    Id = row.Id,
                    Name = row.Name,
                    Tasks = row.Tasks,
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

        public async Task<DataResult<ProjectOutputDTO>> Update(Project entity, UserLogged userInfo)
        {
            _repository.SetLoggedUserInfo(userInfo);
            var resultData = new DataResult<ProjectOutputDTO>();
            try
            {
                entity.ModificationDate = DateTime.UtcNow;
                entity.UpdateByUser = userInfo.userId;

                await _repository.Update(entity);
                await _repository.UnitOfWork.SaveAsync();

                var result = new ProjectOutputDTO()
                {
                    AllCompleted = entity.AllCompleted,
                    CanCreate = entity.CanCreate,
                    Description = entity.Description,
                    Id = entity.Id,
                    Name = entity.Name,
                    Tasks = entity.Tasks,
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