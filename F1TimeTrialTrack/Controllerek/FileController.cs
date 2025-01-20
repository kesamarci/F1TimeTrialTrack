using F1TimeTrialTrack.Logic.Logic;
using Microsoft.AspNetCore.Mvc;

namespace F1TimeTrialTrack.Controllerek
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileLogic _fileService;

        public FileController(FileLogic fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] string trackName)
        {
            if (string.IsNullOrWhiteSpace(trackName))
                return BadRequest("Track name is required.");

            try
            {
                var uploadedFile = await _fileService.UploadFileAsync(file, trackName);
                return Ok(uploadedFile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("track/{trackName}")]
        public IActionResult GetFilesByTrack(string trackName)
        {
            var files = _fileService.GetFilesByTrack(trackName);

            if (files == null || !files.Any())
                return NotFound("No files found for the specified track.");

            return Ok(files);
        }
    }
}
