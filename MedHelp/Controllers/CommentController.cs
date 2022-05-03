using MedHelp.Services;
using MedHelp.Services.Model;
using Microsoft.AspNetCore.Mvc;

namespace MedHelp.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
            => _commentService = commentService;

        [HttpPost]
        public async Task<ActionResult<int>> AddComment(Comment comment)
        {
            return await _commentService.AddComment(comment);
        }

        [HttpGet("doctor/{id}")]
        public async Task<ActionResult<List<Comment>>> GetCommentsByDoctor(int id)
        {
            return await _commentService.GetCommentsByDoctor(id);
        }

        [HttpGet("patient/{id}")]
        public async Task<ActionResult<List<Comment>>> GetCommentsByPatient(int id)
        {
            return await _commentService.GetCommentsByPatient(id);
        }
    }
}
