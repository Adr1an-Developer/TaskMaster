using Microsoft.AspNetCore.Mvc;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Master;

namespace TaskMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _CommentService;
        private IValidateUserService _validateUserService;

        public CommentController(ICommentService CommentService, IValidateUserService validateUserService)
        {
            _CommentService = CommentService;
            _validateUserService = validateUserService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll/{taskId}/user/{userId}")]
        public async Task<IActionResult> GetAll(string taskId, string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(taskId))
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[]
                    {
                        "userId ou taskId não pode estar vazio"
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

            var results = await _CommentService.GetAll(taskId, valid.result);

            return Ok(results);
        }

        /// <summary>
        ///
        /// </summary>
        ///  /// <remarks>
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

            var results = await _CommentService.GetbyId(id, valid.result);

            return Ok(results);
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddCommentDTO comment)
        {
            if (comment == null || !ModelState.IsValid)
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

            var valid = await _validateUserService.ValidateUserId(comment.userId);

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

            var result = await _CommentService.Create(comment, valid.result);

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
        /// <param name="commentData"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(string userId, Comment commentData)
        {
            if (string.IsNullOrEmpty(userId) || commentData == null || !ModelState.IsValid)
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
            if (commentData == null) return BadRequest();

            var result = await _CommentService.Update(commentData, valid.result);

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

            var result = await _CommentService.Delete(id, valid.result);

            return Ok(result);
        }
    }
}