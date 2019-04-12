using System.Collections.Generic;
using System.Linq;

namespace ProjectMVC.Logica.BL
{
    public class UserProjects
    {
        /// <summary>
        /// GET USERS PROJECT
        /// </summary>
        /// <param name="projectId"></param>
        public List<Models.DB.UserProjects> GetUsersProject(int? projectId)
        {
            var _context = new DAL.Models.ProjectMVCEntities();

            var listUsersProject = (from _userProjects in _context.UserProjects
                                    join _aspNetUsers in _context.AspNetUsers on _userProjects.UserId equals _aspNetUsers.Id
                                    where _userProjects.ProjectId == projectId
                                    select new Models.DB.UserProjects
                                    {
                                        Id = _userProjects.Id,
                                        ProjectId = _userProjects.ProjectId,
                                        UserId = _userProjects.UserId,
                                        AspNetUsers = new Models.DB.AspNetUsers
                                        {
                                            Email = _aspNetUsers.Email,
                                            UserName = _aspNetUsers.UserName
                                        },
                                        CreatedAt = _userProjects.CreatedAt,
                                        UpdatedAt = _userProjects.UpdatedAt
                                    }).ToList();

            return listUsersProject;
        }
    }
}
