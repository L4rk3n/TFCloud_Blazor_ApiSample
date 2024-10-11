using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFCloud_Blazor_ApiSample.Models.DTOs;
using TFCloud_Blazor_ApiSample.Repos;
using TFCloud_Blazor_ApiSample.Tools;

namespace TFCloud_Blazor_ApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameRepo GameRepo;


        public GameController(GameRepo GameRepo)
        {
            this.GameRepo = GameRepo;

        }


        [HttpPost("AjoutJeux")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminRequired")]

        public IActionResult AjoutJeux([FromBody] GameForm GameForm)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (GameRepo.AjoutJeux(GameForm.Title, GameForm.ReleaseYear, GameForm.Synopsis)) 
            {
                return Ok("Enregistrement réussi");
            }
            return BadRequest("t'as du merder");
        }
        [HttpGet ("ById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get(int Id)
        {
            return Ok(GameRepo.Get(Id));
        }

        [HttpGet("All")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAll()
        {
            return Ok(GameRepo.GetAll());
        }
    }
}
