using System.Collections.Generic;
using System.Linq;

namespace ProjectMVC.Logica.BL
{
    public class AspNetUsers
    {
        /// <summary>
        /// GET USERS
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<Models.DB.AspNetUsers> GetUsers(int? tenantId)
        {
            var _context = new DAL.Models.ProjectMVCEntities();

            var listUsersProject = (from _aspNetUsers in _context.AspNetUsers
                                    where _aspNetUsers.TenantId == tenantId &&
                                    _aspNetUsers.EmailConfirmed == true
                                    select new Models.DB.AspNetUsers
                                    {
                                        Id = _aspNetUsers.Id,
                                        UserName = _aspNetUsers.UserName,
                                        Email = _aspNetUsers.Email
                                    }).ToList();

            return listUsersProject;
        }
    }
}
