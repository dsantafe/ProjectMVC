using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ProjectMVC.Logica.Data
{
    public class ConnectionDB
    {
        private ConnectionDB()
        {

        }

        private static SqlConnection connection = null;

        /// <summary>
        /// METODO QUE CIERRA LA CONEXION
        /// </summary>
        public static void CloseConnection() {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// METODO QUE ESTABLECE LA CONEXION
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            try
            {
                if (connection == null)
                {
                    string cnx = ConfigurationManager.ConnectionStrings["Cnx"].ToString();
                    connection = new SqlConnection(cnx);
                    connection.Open();
                }

                return connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
