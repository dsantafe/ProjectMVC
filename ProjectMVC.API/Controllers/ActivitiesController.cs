using System;
using System.Web.Http;

namespace ProjectMVC.API.Controllers
{
    [RoutePrefix("api/Activities")]
    public class ActivitiesController : ApiController
    {
        [Route("GetActivities")]
        public IHttpActionResult GetActivities()
        {
            try
            {
                Logica.Services.Activities activities = new Logica.Services.Activities();
                var listActivities = activities.GetActivities();

                return Ok(listActivities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("CreateActivity")]
        public IHttpActionResult CreateActivity(Logica.Models.BindingModels.ActivitiesCreateBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Logica.Services.Activities activities = new Logica.Services.Activities();
                activities.CreateActivity(model.Name);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
