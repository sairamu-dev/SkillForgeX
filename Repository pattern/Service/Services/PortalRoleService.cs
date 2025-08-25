using DevTaskFlow.Repository_pattern.Core.Enitities;
using DevTaskFlow.Repository_pattern.Repository.Repositories;

namespace DevTaskFlow.Repository_pattern.Service.Services
{
    public class PortalRoleService
    {
        private readonly PortalRolesRepository _portalRoleRepository;

        public PortalRoleService (PortalRolesRepository portalRoleRepository)
        {
            _portalRoleRepository = portalRoleRepository;
        }

        public Api GetApiDetail()
        {
            return _portalRoleRepository.GetApiDetail();
        }

        public void UpdateApiDetail(Api api)
        {
            _portalRoleRepository.EditApiDetail(api);
        }

        public IEnumerable<PortalRoles> GetPortalRoles()
        {
           return _portalRoleRepository.GetRoles();
        }

        public IEnumerable<Project> GetProjects()
        {
           return _portalRoleRepository.GetProjects();
        }

        public IEnumerable<Skills> GetSkillset()
        {
           return _portalRoleRepository.GetSkillset();
        }

        public IEnumerable<Tasks> GetTasks()
        {
            return _portalRoleRepository.GetTasks();
        }

        public void AddTaskForDevUsers(Tasks task)
        {
            _portalRoleRepository.AddTaskForDevUsers(task);
        }

        public void MarkLowPriorityTaskAsInQueue(int taskId)
        {

            _portalRoleRepository.MarkLowPriorityTaskAsInQueue(taskId);
        }

        public List<Tasks> GetDeveloperTask(int DevID)
        {
            return _portalRoleRepository.GetDeveloperTask(DevID);
        }

        public int GetTaskCount()
        {
            return _portalRoleRepository.GetTasksCount();
        }

        public void UpdateDevTask(Tasks task)
        {
            _portalRoleRepository.UpdateDevTask(task);
        }

        public void OverrideTask(Tasks task)
        {
            _portalRoleRepository.OverrideTask(task);
        }

        public void InsertKeywordWithSkills(KeywordWithSkills keywordWithSkills)
        {
            _portalRoleRepository.InsertKeywordWithSkills(keywordWithSkills);
        }

        public List<KeywordWithSkills> GetKeywordWithSkills()
        {
            return _portalRoleRepository.GetKeywordWithSkills();
        }

        public List<Feedback> GetFeedbacks()
        {
            return _portalRoleRepository.GetFeedbacks();
        }

        public void UpdateFeedback(Feedback feedback) 
        {
            _portalRoleRepository.SaveFeedBack(feedback);
        }
    }
}
