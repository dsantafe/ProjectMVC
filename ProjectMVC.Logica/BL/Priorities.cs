using System.Collections.Generic;
using System.Linq;

namespace ProjectMVC.Logica.BL
{
    public class Priorities
    {
        /// <summary>
        /// GET PRIORITIES
        /// </summary>
        /// <returns></returns>
        public List<Models.DB.Priorities> GetPriorities()
        {
            DAL.Models.ProjectMVCEntities _context = new DAL.Models.ProjectMVCEntities();
            var listPriorities = (from _priorities in _context.Priorities
                                  where _priorities.Active == true
                                  select new Models.DB.Priorities
                                  {
                                      Id = _priorities.Id,
                                      Name = _priorities.Name,
                                      Active = _priorities.Active
                                  }).ToList();

            return listPriorities;
        }
    }
}
