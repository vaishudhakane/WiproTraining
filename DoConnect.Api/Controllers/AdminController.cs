using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoConnect_Api.Data;
using DoConnect.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoConnect.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Get all pending questions
        [HttpGet("pending-questions")]
        public async Task<ActionResult<IEnumerable<Question>>> GetPendingQuestions()
        {
            return await _context.Questions
                                 .Where(q => q.Status == "Pending")
                                 .Include(q => q.User)
                                 .ToListAsync();
        }

        // Approve a question
        [HttpPut("approve-question/{id}")]
        public async Task<IActionResult> ApproveQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return NotFound();

            question.Status = "Approved";
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Reject a question
        [HttpPut("reject-question/{id}")]
        public async Task<IActionResult> RejectQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return NotFound();

            question.Status = "Rejected";
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //  Pending answers (similar logic)
        [HttpGet("pending-answers")]
        public async Task<ActionResult<IEnumerable<Answer>>> GetPendingAnswers()
        {
            return await _context.Answers
                                 .Where(a => a.Status == "Pending")
                                 .Include(a => a.User)
                                 .Include(a => a.Question)
                                 .ToListAsync();
        }

        [HttpPut("approve-answer/{id}")]
        public async Task<IActionResult> ApproveAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null) return NotFound();

            answer.Status = "Approved";
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("reject-answer/{id}")]
        public async Task<IActionResult> RejectAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null) return NotFound();

            answer.Status = "Rejected";
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("delete-question/{id}")]
public async Task<IActionResult> DeleteQuestion(int id) {
    var q = await _context.Questions.FindAsync(id);
    if (q == null) return NotFound();
    _context.Questions.Remove(q);
    await _context.SaveChangesAsync();
    return NoContent();
}

    }
}


// using DoConnect.Api.Data;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace DoConnect.Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     [Authorize(Roles = "Admin")]
//     public class AdminController : ControllerBase
//     {
//         private readonly AppDbContext _context;
//         public AdminController(AppDbContext context) { _context = context; }

//         [HttpGet("pending-questions")]
//         public async Task<IActionResult> PendingQuestions()
//         {
//             var questions = await _context.Questions
//                 .Where(q => q.Status == "Pending")
//                 .Include(q => q.User)
//                 .ToListAsync();
//             return Ok(questions);
//         }

//         [HttpPost("approve-question/{id}")]
//         public async Task<IActionResult> ApproveQuestion(int id)
//         {
//             var question = await _context.Questions.FindAsync(id);
//             if (question == null) return NotFound();

//             question.Status = "Approved";
//             await _context.SaveChangesAsync();
//             return Ok(question);
//         }

//         [HttpPost("reject-question/{id}")]
//         public async Task<IActionResult> RejectQuestion(int id)
//         {
//             var question = await _context.Questions.FindAsync(id);
//             if (question == null) return NotFound();

//             question.Status = "Rejected";
//             await _context.SaveChangesAsync();
//             return Ok(question);
//         }

//         [HttpGet("pending-answers")]
//         public async Task<IActionResult> PendingAnswers()
//         {
//             var answers = await _context.Answers
//                 .Where(a => a.Status == "Pending")
//                 .Include(a => a.User)
//                 .Include(a => a.Question)
//                 .ToListAsync();
//             return Ok(answers);
//         }

//         [HttpPost("approve-answer/{id}")]
//         public async Task<IActionResult> ApproveAnswer(int id)
//         {
//             var answer = await _context.Answers.FindAsync(id);
//             if (answer == null) return NotFound();

//             answer.Status = "Approved";
//             await _context.SaveChangesAsync();
//             return Ok(answer);
//         }

//         [HttpPost("reject-answer/{id}")]
//         public async Task<IActionResult> RejectAnswer(int id)
//         {
//             var answer = await _context.Answers.FindAsync(id);
//             if (answer == null) return NotFound();

//             answer.Status = "Rejected";
//             await _context.SaveChangesAsync();
//             return Ok(answer);
//         }
//     }
// }
