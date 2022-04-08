using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class IngredientsService
    {
        private readonly IngredientsRepository _iRepo;
        public IngredientsService(IngredientsRepository iRepo)
        {
            _iRepo = iRepo;
        }
    }
}