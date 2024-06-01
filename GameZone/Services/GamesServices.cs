using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GameZone.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _imgPath;

        public GamesServices(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _imgPath = $"{_environment.WebRootPath}{FileSettings.imagesFolder}";
        }


        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int Id)
        {
            var game =  _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .FirstOrDefault(g => g.Id==Id);
            
            return game;
        }




        public async Task Create(GameCreateViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryID,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(e => new GameDevice { DeviceId = e }).ToList(),
            };

            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Edit(GameEditViewModel model)
        {
            var game = _context.Games
                .Include (g => g.Devices)
                .FirstOrDefault(g => g.Id == model.Id);

            if (game is null)
                return null;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryID;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            bool hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            if (hasNewCover)
                game.Cover = await SaveCover(model.Cover!);
             
           var EffectedRowsInDB  = _context.SaveChanges();

            if (EffectedRowsInDB > 0)
            {
                if(hasNewCover)
                    {
                    var oldCoverPath = Path.Combine(_imgPath, oldCover!);
                    File.Delete(oldCoverPath);
                }
                return game;
            }
            else
            {
                var path = Path.Combine(_imgPath, game.Cover!);
                File.Delete(path);
                return null;
            }
        }
        private async Task<string> SaveCover(IFormFile Cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_imgPath, coverName);
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);

            return coverName;
        }

        public bool Delete(int Id)
        {
            bool isDeleted = false;
            var game = _context.Games.Find(Id);

            if (game is null)
                return isDeleted;

            _context.Games.Remove(game);

            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imgPath, game.Cover!);
                File.Delete(cover);
            }

            return isDeleted;
        }
    }
}
