using Music.BLL.DTO;
using Music.BLL.Infrastructure;
using Music.BLL.Interfaces;
using Music.DAL.Entities;
using Music.DAL.Interfaces;
using AutoMapper;
using System.Numerics;

namespace Music.BLL.Services
{
    public class SongService : ISongService
    {
        IUnitOfWork _database { get; set; }

        public SongService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task CreateSong(SongDTO dto)
        {
            var song = new Song
            {
                Id = dto.Id,
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre,
            };
            await _database.Songs.Create(song);
            await _database.Save();
        }
        public async Task UpdateSong(SongDTO dto)
        {
            var song = new Song
            {
                Id = dto.Id,
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre,
            };
            _database.Songs.Update(song);
            await _database.Save();
        }

        public async Task DeletePlayer(int id)
        {
            await _database.Songs.Delete(id);
            await _database.Save();
        }

        public async Task<SongDTO> GetSong(int id)
        {
            var song = await _database.Songs.Get(id);
            if (song == null)
                throw new ValidationException("Inalid song", "");
            return new SongDTO
            {
                Id = song.Id,
                Title = song.Title,
                Author = song.Author,
                Genre = song.Genre
            };
        }

        public async Task<IEnumerable<SongDTO>> GetSongs()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Song, SongDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await _database.Songs.GetAll());
        }

    }
}
