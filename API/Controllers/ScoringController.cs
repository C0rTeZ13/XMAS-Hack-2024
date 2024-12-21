using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Models;
using ServiceLayer.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class ScoringController : ControllerBase
    {
        private readonly ScoringService _scoringService;

        public ScoringController(ScoringService scoringService)
        {
            _scoringService = scoringService;
        }

        [HttpPost]
        public async Task<ScoringResponce> GetScoringResponceAsync(ScoringRequest request)
        {
            return await _scoringService.GetScoringRequestAsync(request);
        }
    }
}
