using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Entities.DTOs.Common;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.DTOs.Reports;
using TaskMaster.Entities.Enums;

namespace TaskMaster.Domain.Services.Implementations
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository ?? throw new ArgumentNullException(nameof(reportsRepository));
        }

        public async Task<DataResults<StatusTaskbyProjectOutputDTO>> GetAllStatusTaskbyProject(int lastDays = 15)
        {
            var resultData = new DataResults<StatusTaskbyProjectOutputDTO>();
            try
            {
                var results = await _reportsRepository.GetAllStatusTaskbyProject(lastDays);

                if (!results.Any())
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

                resultData.totalRecords = results.Count();
                resultData.messageType = nameof(MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.results = results;
                resultData.messages = new List<string>()
                {
                    $"{results.Count()} Dados encontrados"
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
    }
}