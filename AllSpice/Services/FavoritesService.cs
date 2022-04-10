using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class FavoritesService
    {
        private readonly FavoritesRepository _fRepo;
        public FavoritesService(FavoritesRepository fRepo)
        {
            _fRepo = fRepo;
        }
        internal object Create(Favorite favoriteData)
        {
            return _fRepo.Create(favoriteData);
        }

        internal object Remove(int id, Account userInfo)
        {
            Favorite favorite = _fRepo.GetById(id);
            if (favorite.AccountId != userInfo.Id)
            {
                throw new Exception("Nope");
            }
            return _fRepo.Remove(id);
        }
    }
}