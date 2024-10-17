using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFCloud_Blazor_ApiSample.Models.DTOs;
using TFCloud_Blazor_ApiSample.Repos;

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


        [HttpPost("Ajout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public IActionResult Ajout([FromBody] GameForm GameForm)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (GameRepo.Ajout(GameForm.Title, GameForm.ReleaseYear, GameForm.Synopsis)) 
            {
                return Ok("Enregistrement réussi");
            }
            return BadRequest("t'as du merder");
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] GameForm GameForm, int Id)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (GameRepo.Update(Id,GameForm.Title, GameForm.ReleaseYear, GameForm.Synopsis))
            {
                return Ok("Mise à jour réussie");
            }
            return BadRequest("t'as du merder");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("ID invalide.");
            }
            bool deleteSuccess = GameRepo.Delete(Id);
            if (deleteSuccess)
            {
                return Ok("Suppression réussie");
            }
            return BadRequest("Erreur lors de la suppression du jeu.");
        }



        [HttpGet ("{Id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get([FromRoute]int Id)
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
