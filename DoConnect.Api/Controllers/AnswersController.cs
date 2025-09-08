using DoConnect_Api.Data;
using DoConnect.Api.Dtos;
using DoConnect.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoConnect.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AnswersController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromForm] CreateAnswerDto dto)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "id").Value);

            var answer = new Answer
            {
                QuestionId = dto.QuestionId,
                UserId = userId,
                Text = dto.Text,
                Status = "Pending"
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            // File upload
            if (dto.Images != null)
            {
                foreach (var file in dto.Images)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(_env.WebRootPath, "uploads", fileName);
                    using var stream = System.IO.File.Create(path);
                    await file.CopyToAsync(stream);

                    _context.Images.Add(new Image
                    {
                        AnswerId = answer.Id,
                        Path = $"/uploads/{fileName}"
                    });
                }
                await _context.SaveChangesAsync();
            }

            return Ok(answer);
        }

        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetAnswers(int questionId)
        {
            var answers = await _context.Answers
                .Where(a => a.QuestionId == questionId && a.Status == "Approved")
                .Include(a => a.User)
                .Include(a => a.Images)
                .ToListAsync();

            return Ok(answers);
        }
    }
}
