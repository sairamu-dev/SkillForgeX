using DevTaskFlow.Repository_pattern.Core.Enitities;
using DevTaskFlow.Repository_pattern.Core.Interfaces;

namespace DevTaskFlow.Repository_pattern.Repository.Repositories
{
    public class ErrorLogRepository : IError
    {
        private readonly AppDbContext _context;
        public ErrorLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ErrorLog> GetErrorLogs()
        {
            return _context.errorLogs.ToList();
        }

        public void AddError(ErrorLog error) 
        {
            _context.errorLogs.Add(error);
            _context.SaveChanges();
        }
    }
}
