using DevStreamAPI.DataGrabber;
using Microsoft.AspNetCore.Mvc;

namespace DevStreamAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class StreamData : Controller
    {
        [HttpGet("/search")]
        public ActionResult Search(string url, string proxyurl)
        {            
            return Ok(x1337.GetSearchResult(url, proxyurl));
        }
    }
}
