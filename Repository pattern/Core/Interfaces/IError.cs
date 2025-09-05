using SkillForgeX.Repository_pattern.Core.Enitities;

namespace SkillForgeX.Repository_pattern.Core.Interfaces
{
    public interface IError
    {
        List<ErrorLog> GetErrorLogs();
        void AddError(ErrorLog error);
    }
}
