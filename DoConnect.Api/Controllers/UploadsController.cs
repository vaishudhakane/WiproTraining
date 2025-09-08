using DoConnect_Api.Data;
using DoConnect.Api.Dtos;
using DoConnect.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoConnect.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public UploadsController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file selected");

            var fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
            var path = Path.Combine(_env.WebRootPath, "uploads", fileName);

            using var stream = System.IO.File.Create(path);
            await dto.File.CopyToAsync(stream);

            var image = new Image { Path = $"/uploads/{fileName}" };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new { Url = $"/uploads/{fileName}" });
        }
    }
}
