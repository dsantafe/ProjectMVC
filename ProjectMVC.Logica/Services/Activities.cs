using System;

using System.Data;
using System.Data.SqlClient;

namespace ProjectMVC.Logica.Services
{
    public class Activities : Interfaces.IActivities
    {
        private SqlConnection connection = null;
        private SqlCommand command = null;

        /// <summary>
        /// METODO QUE CREA LA ACTIVIDAD
        /// </summary>
        /// <param name="name">NOMBRE DE LA ACTIVIDAD</param>
        public void CreateActivity(string name)
        {
            try
            {
                connection = Data.ConnectionDB.GetConnection();
                command = new SqlCommand("CreateActivity");
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@name",name));

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateActivity(int id, string name, bool active)
        {
            throw new NotImplementedException();
        }
    }
}
