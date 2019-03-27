using System.Collections.Generic;
using System.Linq;

namespace ProjectMVC.Logica.BL
{
    public class Tenants
    {
        /// <summary>
        /// GET TENANTS BY USER
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Models.DB.Tenants> GetTenants(string userId)
        {
            DAL.Models.ProjectMVCEntities db = new DAL.Models.ProjectMVCEntities();

            var listTenants = (from _tenants in db.Tenants
                               join _users in db.AspNetUsers on _tenants.Id equals _users.TenantId
                               where _users.Id.Equals(userId)
                               select new Models.DB.Tenants
                               {
                                   Id = _tenants.Id,
                                   Name = _tenants.Name,
                                   Plan = _tenants.Plan,
                                   CreatedAt = _tenants.CreatedAt,
                                   UpdatedAt = _tenants.UpdatedAt
                               }).ToList();

            return listTenants;
        }
    }
}
