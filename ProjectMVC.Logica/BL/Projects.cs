using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectMVC.Logica.BL
{
    public class Projects
    {
        /// <summary>
        /// GET PROJECTS BY ID OR TENANT OR USER
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Models.DB.Projects> GetProjects(int? id,
            int? tenantId,
            string userId = null)
        {
            DAL.Models.ProjectMVCEntities db = new DAL.Models.ProjectMVCEntities();

            var listProjectsEF = (from _projects in db.Projects
                                  select _projects);

            if (id != null)
                listProjectsEF = listProjectsEF.Where(x => x.Id == id);
            if (tenantId != null)
                listProjectsEF = listProjectsEF.Where(x => x.TenantId == tenantId);
            if (!string.IsNullOrEmpty(userId))
                listProjectsEF = (from _projects in listProjectsEF
                                  join _userProjects in db.UserProjects on _projects.Id equals _userProjects.ProjectId
                                  where _userProjects.Id.Equals(userId)
                                  select _projects);

            var listProjects = (from _projects in listProjectsEF
                                select new Models.DB.Projects
                                {
                                    Id = _projects.Id,
                                    Title = _projects.Title,
                                    Details = _projects.Details,
                                    ExpectedCompletionDate = _projects.ExpectedCompletionDate,
                                    TenantId = _projects.TenantId,
                                    CreatedAt = _projects.CreatedAt,
                                    UpdatedAt = _projects.UpdatedAt
                                }).ToList();

            return listProjects;
        }

        /// <summary>
        /// CREATE PROJECTS
        /// </summary>
        /// <param name="title"></param>
        /// <param name="details"></param>
        /// <param name="expectedCompletionDate"></param>
        /// <param name="tenantId"></param>
        public void CreateProjects(string title,
            string details,
            DateTime? expectedCompletionDate,
            int? tenantId)
        {
            DAL.Models.ProjectMVCEntities db = new DAL.Models.ProjectMVCEntities();

            db.Projects.Add(new DAL.Models.Projects
            {
                Title = title,
                Details = details,
                ExpectedCompletionDate = expectedCompletionDate,
                TenantId = tenantId,
                CreatedAt = DateTime.Now
            });

            db.SaveChanges();

            /*
            INSERT INTO Projects(Title, Details, ExpectedCompletionDate, TenantId, CreatedAt)
            VALUES('Project Management', 'Details...','2019-03-30',2,'2019-03-18')
            */
        }

        /// <summary>
        /// UPDATE PROJECT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="details"></param>
        /// <param name="expectedCompletionDate"></param>
        public void UpdateProjects(int id,
            string title,
            string details,
            DateTime? expectedCompletionDate)
        {
            DAL.Models.ProjectMVCEntities db = new DAL.Models.ProjectMVCEntities();

            var project = db.Projects.FirstOrDefault(x => x.Id == id);

            project.Title = title;
            project.Details = details;
            project.ExpectedCompletionDate = expectedCompletionDate;
            project.UpdatedAt = DateTime.Now;

            db.SaveChanges();
        }

        /// <summary>
        /// DELETE PROJECTS
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProjects(int? id)
        {
            DAL.Models.ProjectMVCEntities db = new DAL.Models.ProjectMVCEntities();

            var project = db.Projects.FirstOrDefault(x => x.Id == id);
            db.Projects.Remove(project);

            db.SaveChanges();
        }
    }
}
