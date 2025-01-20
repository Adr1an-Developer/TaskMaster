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
        /// -UserId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -UserId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="taskId"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll/{taskId}/user/{userID}")]
        public async Task<IActionResult> GetAll(string taskId, string userID)
        {
            var valid = await _validateUserService.ValidateUserId(userID);

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
        /// -UserId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -UserId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{id}/user/{userId}")]
        public async Task<IActionResult> Get(string id, string userId)
        {
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
        /// -UserId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -UserId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddCommentDTO Comment)
        {
            var valid = await _validateUserService.ValidateUserId(Comment.UserId);

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

            if (Comment == null) return BadRequest();

            var result = await _CommentService.Create(Comment, valid.result);

            return Ok(result);
        }

        /// <summary>
        ///  Atualizar o projeto do usuário
        /// </summary>
        /// <remarks>
        /// -UserId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -UserId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="userID"></param>
        /// <returns>o projeto específico do usuário</returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(string userId, Comment Commentdata)
        {
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
            if (Commentdata == null) return BadRequest();

            var result = await _CommentService.Update(Commentdata, valid.result);

            return Ok(result);
        }

        /// <summary>
        ///  Excluir o projeto do usuário
        /// </summary>
        /// <remarks>
        /// -UserId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -UserId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="userID"></param>
        /// <returns>o projeto específico do usuário</returns>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id, string userId)
        {
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