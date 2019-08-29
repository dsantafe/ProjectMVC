using System.Collections.Generic;

namespace ProjectMVC.Logica.Interfaces
{
    public interface IActivities
    {
        void CreateActivity(string name);

        void UpdateActivity(int id, string name, bool active);

        void DeleteActivity(int id);

        List<Models.DB.Activities> GetActivities();
    }
}
