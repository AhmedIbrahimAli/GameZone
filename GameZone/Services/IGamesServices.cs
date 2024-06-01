using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGamesServices 
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int Id);
        Task Create(GameCreateViewModel Game);
        Task<Game?> Edit(GameEditViewModel Game);
        bool Delete(int Id);    
        


    }
}
