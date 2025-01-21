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
    public class ReportsController : ControllerBase
    {
        private IReportsService _ReportsService;
        private IValidateUserService _validateUserService;

        public ReportsController(IReportsService ReportsService, IValidateUserService validateUserService)
        {
            _ReportsService = ReportsService;
            _validateUserService = validateUserService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// -userId = 123e4567-e89b-12d3-a456-426655440000 = usuario;
        /// -userId = 123e4567-e89b-12d3-a456-426655440001 = manager;
        /// </remarks>
        /// <param name="lastDays"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllStatusTaskbyProject/{lastDays}/user/{userId}")]
        public async Task<IActionResult> GetAllStatusTaskbyProject(int lastDays, string userId)
        {
            if (lastDays == null) lastDays = 15;

            if (string.IsNullOrEmpty(userId) || lastDays == null)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[]
                    {
                        "userId ou lastDays não pode estar vazio"
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

            if (!valid.result.IsManager)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[]
                    {
                        "Somente usuários com perfil de Manager têm acesso a este relatório."
                    }
                };
                return Unauthorized(notFound);
            }

            var results = await _ReportsService.GetAllStatusTaskbyProject(lastDays);

            return Ok(results);
        }
    }
}