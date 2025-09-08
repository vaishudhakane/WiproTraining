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
    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public QuestionsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ Users: Get only approved questions
        [HttpGet("approved")]
        [AllowAnonymous] // anyone can see approved questions
        public async Task<ActionResult<IEnumerable<Question>>> GetApprovedQuestions()
        {
            return await _context.Questions
                                 .Where(q => q.Status == "Approved")
                                 .Include(q => q.User)
                                 .Include(q => q.Images)
                                 .Include(q => q.Answers)
                                 .ToListAsync();
        }

        // ✅ Users: Ask a question with optional image(s) → goes into Pending state
        [HttpPost]
        [Authorize(Roles = "User")] // only Users can ask
        public async Task<IActionResult> AskQuestion([FromForm] CreateQuestionDto dto)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "id").Value);

            var question = new Question
            {
                UserId = userId,
                Title = dto.Title,
                Text = dto.Text,
                Status = "Pending" // must be approved by Admin
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            // ✅ Handle image uploads if provided
            if (dto.Images != null && dto.Images.Any())
            {
                var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                foreach (var file in dto.Images)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(uploadFolder, fileName);

                    using var stream = System.IO.File.Create(path);
                    await file.CopyToAsync(stream);

                    _context.Images.Add(new Image
                    {
                        QuestionId = question.Id,
                        Path = $"/uploads/{fileName}"
                    });
                }
                await _context.SaveChangesAsync();
            }

            return Ok(question);
        }

        // ✅ Get single approved question by Id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                .Include(q => q.Images)
                .FirstOrDefaultAsync(q => q.Id == id && q.Status == "Approved");

            if (question == null) return NotFound();
            return Ok(question);
        }

        // ✅ Search questions (title/text) only among Approved ones
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var questions = await _context.Questions
                .Where(q => q.Status == "Approved" &&
                            (q.Title.Contains(query) || q.Text.Contains(query)))
                .Include(q => q.User)
                .Include(q => q.Answers)
                .Include(q => q.Images)
                .ToListAsync();

            return Ok(questions);
        }
    }
}


// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using DoConnect.Api.Data;
// using DoConnect.Api.Entities;
// using Microsoft.EntityFrameworkCore;

// namespace DoConnect.Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class QuestionsController : ControllerBase
//     {
//         private readonly AppDbContext _context;

//         public QuestionsController(AppDbContext context)
//         {
//             _context = context;
//         }

//         // ✅ Users: Get only approved questions
//         [HttpGet("approved")]
//         [AllowAnonymous] // anyone can see approved questions
//         public async Task<ActionResult<IEnumerable<Question>>> GetApprovedQuestions()
//         {
//             return await _context.Questions
//                                  .Where(q => q.Status == "Approved")
//                                  .Include(q => q.User)
//                                  .ToListAsync();
//         }

//         // ✅ Users: Ask a question (default "Pending")
//         [HttpPost]
//         [Authorize(Roles = "User")]
//         public async Task<ActionResult<Question>> AskQuestion(Question question)
//         {
//             question.Status = "Pending"; // must be reviewed by Admin
//             _context.Questions.Add(question);
//             await _context.SaveChangesAsync();
//             return CreatedAtAction(nameof(GetApprovedQuestions), new { id = question.Id }, question);
//         }
//     }
// }


// using DoConnect.Api.Data;
// using DoConnect.Api.Dtos;
// using DoConnect.Api.Entities;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace DoConnect.Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class QuestionsController : ControllerBase
//     {
//         private readonly AppDbContext _context;
//         private readonly IWebHostEnvironment _env;

//         public QuestionsController(AppDbContext context, IWebHostEnvironment env)
//         {
//             _context = context;
//             _env = env;
//         }

//         [HttpPost]
//         [Authorize(Roles = "User,Admin")]
//         public async Task<IActionResult> Create([FromForm] CreateQuestionDto dto)
//         {
//             var userId = int.Parse(User.Claims.First(c => c.Type == "id").Value);

//             var question = new Question
//             {
//                 UserId = userId,
//                 Title = dto.Title,
//                 Text = dto.Text,
//                 Status = "Pending"
//             };

//             _context.Questions.Add(question);
//             await _context.SaveChangesAsync();

//             // Handle file uploads
//             if (dto.Images != null)
//             {
//                 foreach (var file in dto.Images)
//                 {
//                     var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
//                     var path = Path.Combine(_env.WebRootPath, "uploads", fileName);
//                     using var stream = System.IO.File.Create(path);
//                     await file.CopyToAsync(stream);

//                     _context.Images.Add(new Image
//                     {
//                         QuestionId = question.Id,
//                         Path = $"/uploads/{fileName}"
//                     });
//                 }
//                 await _context.SaveChangesAsync();
//             }

//             return Ok(question);
//         }

//         [HttpGet]
//         public async Task<IActionResult> GetAll()
//         {
//             var questions = await _context.Questions
//                 .Include(q => q.User)
//                 .Include(q => q.Answers)
//                 .Include(q => q.Images)
//                 .Where(q => q.Status == "Approved")
//                 .ToListAsync();
//             return Ok(questions);
//         }

//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetById(int id)
//         {
//             var question = await _context.Questions
//                 .Include(q => q.User)
//                 .Include(q => q.Answers)
//                 .Include(q => q.Images)
//                 .FirstOrDefaultAsync(q => q.Id == id && q.Status == "Approved");

//             if (question == null) return NotFound();
//             return Ok(question);
//         }

//         [HttpGet("search")]
//         public async Task<IActionResult> Search([FromQuery] string query)
//         {
//             var questions = await _context.Questions
//                 .Where(q => q.Status == "Approved" &&
//                             (q.Title.Contains(query) || q.Text.Contains(query)))
//                 .Include(q => q.User)
//                 .Include(q => q.Answers)
//                 .ToListAsync();
//             return Ok(questions);
//         }
//     }
// }
