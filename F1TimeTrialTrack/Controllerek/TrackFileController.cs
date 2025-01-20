using F1TimeTrialTrack.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace F1TimeTrialTrack.Controllerek
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackFileController : ControllerBase
    {
        [HttpPost("képfeltöltés")] //api/main/uploadfile (nagyon próba, lehet benne hagyom lehet nem)
        public IActionResult UploadFile(IFormFile file)
        {

            return Ok(new UploadHandler().Upload(file));
        }
    }
}
