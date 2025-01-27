﻿using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Security;

namespace TaskMaster.Domain.Services.Implementations
{
    public class ValidateUserService : IValidateUserService
    {
        private IValidateUserRepository _repository;

        public ValidateUserService(IValidateUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<DataResult<UserLogged>> ValidateUserId(string id)
        {
            var resultData = new DataResult<UserLogged>();
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                        "Usuário não registrado"
                    };
                    return resultData;
                }

                var row = await _repository.ValidateExternalUserAsync(id.Trim());

                if (row == null)
                {
                    resultData.messageType = nameof(MessageTypeResultEnum.Warning);
                    resultData.error = true;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                        "Usuário não registrado"
                    };
                    return resultData;
                }

                resultData.messageType = nameof(MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.result = row;
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
    }
}