using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        [WebMethod]
        public string GetAgeInDays(int agesInDays)
        {
            return "Años - Meses - Dias";
        }

        [WebMethod]
        public int GetAge(int yearOfBirthDay)
        {
            return DateTime.Now.Year - yearOfBirthDay;
        }

        [WebMethod]
        public string GetName()
        {
            return "David Santafe";
        }
    }
}
