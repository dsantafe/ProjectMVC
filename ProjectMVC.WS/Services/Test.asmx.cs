using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;

namespace ProjectMVC.WS.Services
{
    /// <summary>
    /// Descripción breve de Test
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Test : System.Web.Services.WebService
    {
        /// <summary>
        /// METODO QUE CREA LA ACTIVIDAD
        /// </summary>
        /// <param name="name">NOMBRE DE LA ACTIVIDAD</param>
        [WebMethod]
        public void CreateActivity(string name)
        {
            Logica.BL.Utils utils = new Logica.BL.Utils();
            string nameDecode = utils.Base64Decode(name);

            Logica.Services.Activities activities = new Logica.Services.Activities();
            activities.CreateActivity(nameDecode);
        }

        [WebMethod]
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        [WebMethod]
        public string Base64Decode(string base64EndodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EndodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        [WebMethod]
        public List<Logica.Models.DB.Activities> GetActivities()
        {
            var listActivities = new List<Logica.Models.DB.Activities>();

            Logica.Services.Activities activities = new Logica.Services.Activities();
            listActivities = activities.GetActivities();

            return listActivities;
        }
    }
}
