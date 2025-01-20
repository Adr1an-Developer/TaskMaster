using Microsoft.AspNetCore.Mvc;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Security;

namespace TaskMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _TaskService;
        private IValidateUserService _validateUserService;

        public TaskController(ITaskService TaskService, IValidateUserService validateUserService)
        {
            _TaskService = TaskService;
            _validateUserService = validateUserService;
        }

        /// <summary>
        /// obtém todas as tarefas em um projeto.
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll/{projectId}/user/{userId}")]
        public async Task<IActionResult> GetAll(string projectId, string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(projectId))
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[]
                    {
                        "userId ou projectId não pode estar vazio"
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

            var results = await _TaskService.GetAll(projectId, valid.result);

            return Ok(results);
        }

        /// <summary>
        /// obter tarefa por id.
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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

            var results = await _TaskService.GetbyId(id, valid.result);

            return Ok(results);
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddTaskDTO task)
        {
            if (task == null || !ModelState.IsValid)
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

            var valid = await _validateUserService.ValidateUserId(task.userId);

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

            if (task == null) return BadRequest();

            var result = await _TaskService.Create(task, valid.result);

            return Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="taskData"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(string userId, Entities.Master.Task taskData)

        {
            if (string.IsNullOrEmpty(userId) || taskData == null || !ModelState.IsValid)
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
            if (taskData == null) return BadRequest();

            var result = await _TaskService.Update(taskData, valid.result);

            return Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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

            var result = await _TaskService.Delete(id, valid.result);

            return Ok(result);
        }
    }
}