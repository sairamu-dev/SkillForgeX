using DevTaskFlow.Repository_pattern.Core.Enitities;
using DevTaskFlow.Repository_pattern.Core.Interfaces;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.Threading.Tasks;

namespace DevTaskFlow.Repository_pattern.Repository.Repositories
{
    public class PortalRolesRepository : IPortalRoles
    {
        private readonly AppDbContext _context;

        public PortalRolesRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<PortalRoles> GetRoles()
        {
            return _context.PortalRoles.ToList();
        }

        public List<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public List<Skills> GetSkillset()
        {
            return _context.Skills.ToList();
        }

        public List<Tasks> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        public void AddTaskForDevUsers(Tasks task)
        {
            DateTime Today = DateTime.Now;
            task.CreatedDate = Today;
            _context.Tasks.Add(task);
        }

        public void MarkLowPriorityTaskAsInQueue(int taskID)
        {
            Tasks modifiedTask = _context.Tasks.FirstOrDefault(x => x.TaskID == taskID);


            if (modifiedTask.TaskID > 0)
            {
                modifiedTask.Status = "In-Queue";
                modifiedTask.AssignedTo = 0;
                _context.SaveChanges();
            }
        }

        public List<Tasks> GetDeveloperTask(int DevID)
        {
            return _context.Tasks.Where(x => x.AssignedTo == DevID && x.Status != "Completed").ToList();
        }

        public int GetTasksCount()
        {
            return _context.Tasks.Where(x => x.CompletePercentage != 100 && x.Status != "Completed").Count();
        }

        public void UpdateDevTask(Tasks task)
        {
            Tasks currentTask = _context.Tasks.FirstOrDefault(x => x.TaskID == task.TaskID);

            if (currentTask != null)
            {
                currentTask.TaskID = task.TaskID;
                currentTask.Status = task.Status;
                currentTask.CompletePercentage = task.CompletePercentage;
                currentTask.EndDate = task.EndDate;
            }
        }

        public void OverrideTask(Tasks task)
        {
            Tasks currentTask = _context.Tasks.FirstOrDefault(x => x.TaskID == task.TaskID);

            if (currentTask != null)
            {
                currentTask.ProjectID = task.ProjectID;
                currentTask.Description = task.Description;
                currentTask.Priority = task.Priority;
                currentTask.RequiredSkills = task.RequiredSkills;
                currentTask.Status = task.Status;
                currentTask.AssignedTo = task.AssignedTo;
                currentTask.EndDate = task.EndDate;
            }
        }

        public void InsertKeywordWithSkills(KeywordWithSkills keywordWithSkills)
        {
            if (keywordWithSkills != null)
            {
                _context.KeywordWithSkills.Add(keywordWithSkills);
                _context.SaveChanges();
            }
        }

        public List<KeywordWithSkills> GetKeywordWithSkills()
        {
            return _context.KeywordWithSkills
                   .Select(x => new KeywordWithSkills
                   {
                       keyword = x.keyword,
                       skills = x.skills
                   })
                   .ToList();
        }

        public List<Feedback> GetFeedbacks()
        {
            return _context.feedbacks.ToList();
        }

        public void SaveFeedBack(Feedback feedback)
        {
            if (feedback != null)
            {
                _context.feedbacks.Add(feedback);
                _context.SaveChanges();
            }
        }

        public Api GetApiDetail()
        {
            return _context.api.First();
        }

        public void EditApiDetail(Api api)
        {
            Api existingApi = _context.api.FirstOrDefault(x => x.Id == api.Id);
            if (existingApi != null)
            {
                existingApi.Month = api.Month;
                existingApi.UsageCount = api.UsageCount;
                existingApi.IsActive = api.IsActive;

                _context.SaveChanges(); 
            }
        }



    }
}
