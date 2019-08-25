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
        public void CreateActivities(string name)
        {
            
        }
    }
}
