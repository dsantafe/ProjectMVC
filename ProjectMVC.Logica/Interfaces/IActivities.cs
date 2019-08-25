using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Logica.Interfaces
{
    public interface IActivities
    {
        void CreateActivity(string name);

        void UpdateActivity(int id, string name, bool active);

        void DeleteActivity(int id);
    }
}
