using bookstore.Entities;

namespace bookstore.Models
{
    public class UpdateUserRolesViewModel
    {
        public UpdateUserRolesViewModel(string id, Func<Task<IEnumerable<RolesViewModel>>> getAllRoles, Task<IEnumerable<RolesViewModel>> task)
        {
            Id = id;
            GetAllRoles = getAllRoles;
            Task = task;
        }

        public int UserId { get; set; }
        public IEnumerable<Role> AllRoles { get; set; }
        public IEnumerable<string> SelectedRoleIds { get; set; }
        public string Id { get; }
        public Func<Task<IEnumerable<RolesViewModel>>> GetAllRoles { get; }
        public Task<IEnumerable<RolesViewModel>> Task { get; }
    }
}
