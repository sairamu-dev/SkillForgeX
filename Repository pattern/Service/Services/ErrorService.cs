using SkillForgeX.Repository_pattern.Core.Enitities;
using SkillForgeX.Repository_pattern.Repository.Repositories;

namespace SkillForgeX.Repository_pattern.Service.Services
{
    public class ErrorService
    {
        private readonly ErrorLogRepository _errorLogRepository;
        private readonly ILogger<ErrorService> _logger;

        public ErrorService(ErrorLogRepository errorLogRepository, ILogger<ErrorService> logger)
        {
            _errorLogRepository = errorLogRepository;
            _logger = logger;
        }

        public List<ErrorLog> GetErrorLogs()
        {
            return _errorLogRepository.GetErrorLogs();
        }

        public void AddError(string error)
        {
            ErrorLog errorLog = new ErrorLog
            {
                Error = error
            };

            _logger.LogError($"Error occured in - {error} - @{DateTime.Now}");
            _errorLogRepository.AddError(errorLog);
        }
    }
}
