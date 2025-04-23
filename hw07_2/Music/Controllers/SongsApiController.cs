using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Music.BLL.DTO;
using Music.BLL.Interfaces;

namespace Music.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class SongsApiController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongsApiController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public async Task<IEnumerable<SongDTO>> Get()
        {
            return await _songService.GetSongs();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> Get(int id)
        {
            var song = await _songService.GetSong(id);
            return Ok(song);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SongDTO dto)
        {
            await _songService.CreateSong(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SongDTO dto)
        {
            dto.Id = id;
            await _songService.UpdateSong(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _songService.DeleteSong(id);
            return Ok();
        }
    }
}
