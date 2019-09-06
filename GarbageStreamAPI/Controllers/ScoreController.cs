using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarbageStreamAPI.Exceptions;
using GarbageStreamAPI.Model;
using GarbageStreamAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace GarbageStreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private StreamScoreService StreamScoreService { get; set; }

        public ScoreController(StreamScoreService streamScoreService)
        {
            StreamScoreService = streamScoreService;
        }

        // GET api/score?stream=...
        [HttpGet]
        public StreamScoreResponse Get([FromQuery] string stream)
        {
            try
            {
                int score = StreamScoreService.Score(stream);

                return new StreamScoreResponse()
                {
                    Success = true,
                    Score = score,
                };
            }
            catch(StreamScoreException e)
            {
                return new StreamScoreResponse()
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }
    }
}
