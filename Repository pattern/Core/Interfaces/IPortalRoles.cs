using DevTaskFlow.Repository_pattern.Core.Enitities;


namespace DevTaskFlow.Repository_pattern.Core.Interfaces
{
    public interface IPortalRoles
    {
        List<PortalRoles> GetRoles();
        List<Project> GetProjects();
        
        List<Skills> GetSkillset();
        List<Tasks> GetTasks();
        int GetTasksCount();
        List<Tasks> GetDeveloperTask(int DevID);
        void AddTaskForDevUsers(Tasks task);
        void MarkLowPriorityTaskAsInQueue(int taskID);
    }
}
