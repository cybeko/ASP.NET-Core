using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.BLL.DTO;

namespace Music.BLL.Interfaces
{
    public interface ISongService
    {
        Task CreateSong(SongDTO dto);
        Task UpdateSong(SongDTO dto);
        Task DeleteSong(int id);
        Task<SongDTO> GetSong(int id);
        Task<IEnumerable<SongDTO>> GetSongs();
    }
}
