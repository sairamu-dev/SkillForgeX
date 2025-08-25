using DevTaskFlow.Repository_pattern.Core.Enitities;

namespace DevTaskFlow.Repository_pattern.Core.Interfaces
{
    public interface IError
    {
        List<ErrorLog> GetErrorLogs();
        void AddError(ErrorLog error);
    }
}
