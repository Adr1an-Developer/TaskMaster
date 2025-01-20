using Microsoft.AspNetCore.Mvc;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Master;
using TaskMaster.Entities.Security;

namespace TaskMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
        private IValidateUserService _validateUserService;

        public ProjectController(IProjectService projectService, IValidateUserService validateUserService)
        {
            _projectService = projectService;
            _validateUserService = validateUserService;
        }

        /// <summary>
        ///  Obter todos os projetos do usuário
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>todos os projetos do usuário</returns>
        [HttpGet]
        [Route("getAll/{userId}")]
        public async Task<IActionResult> GetAll(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[]
                    {
                        "userId não pode estar vazio"
                    }
                };
                return Unauthorized(notFound);
            }

            var valid = await _validateUserService.ValidateUserId(userId);

            if (valid.error)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = valid.messages
                };

                return Unauthorized(notFound);
            }

            var results = await _projectService.GetAll(valid.result);

            return Ok(results);
        }

        /// <summary>
        ///  Obtém o projeto específico do usuário
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>o projeto específico do usuário</returns>
        [HttpGet]
        [Route("get/{id}/user/{userId}")]
        public async Task<IActionResult> Get(string id, string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(id))
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[]
                    {
                        "userId ou id não pode estar vazio"
                    }
                };
                return Unauthorized(notFound);
            }

            var valid = await _validateUserService.ValidateUserId(userId);

            if (valid.error)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = valid.messages
                };

                return Unauthorized(notFound);
            }

            var results = await _projectService.GetbyId(id, valid.result);

            return Ok(results);
        }

        /// <summary>
        ///  criar projeto de usuário
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>o projeto específico do usuário</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddProjectDTO project)
        {
            if (project == null || !ModelState.IsValid) // Check if the model is valid
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[]
                    {
                        "os parâmetros obrigatórios não podem estar vazios"
                    }
                };
                return Unauthorized(notFound);
            }

            var valid = await _validateUserService.ValidateUserId(project.userId);

            if (valid.error)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = valid.messages
                };

                return Unauthorized(notFound);
            }

            if (project == null) return BadRequest();

            var result = await _projectService.Create(project, valid.result);

            return Ok(result);
        }

        /// <summary>
        ///  Atualizar o projeto do usuário
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>o projeto específico do usuário</returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(string userId, Project projectdata)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || projectdata == null || !ModelState.IsValid) // Check if the model is valid
                {
                    var notFound = new ResultNotFound()
                    {
                        messageType = nameof(MessageTypeResultEnum.Warning),
                        error = true,
                        messages = new string[]
                        {
                        "userId ou id não pode estar vazio"
                        }
                    };
                    return Unauthorized(notFound);
                }

                var valid = await _validateUserService.ValidateUserId(userId);

                if (valid != null)
                {
                    var notFound = new ResultNotFound()
                    {
                        messageType = nameof(MessageTypeResultEnum.Warning),
                        error = true,
                        messages = valid.messages
                    };

                    return Unauthorized(notFound);
                }
                if (projectdata == null) return BadRequest();

                var result = await _projectService.Update(projectdata, valid.result);

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Excluir o projeto do usuário
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>o projeto específico do usuário</returns>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(id))
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[]
                    {
                        "userId ou id não pode estar vazio"
                    }
                };
                return Unauthorized(notFound);
            }

            var valid = await _validateUserService.ValidateUserId(userId);

            if (valid.error)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = valid.messages
                };

                return Unauthorized(notFound);
            }

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _projectService.Delete(id, valid.result);

            return Ok(result);
        }
    }
}