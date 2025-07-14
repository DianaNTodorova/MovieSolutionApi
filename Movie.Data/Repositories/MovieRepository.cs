using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.Entities;

namespace Movie.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public void Add(Movies movie)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movies>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movies> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Movies movie)
        {
            throw new NotImplementedException();
        }

        public void Update(Movies movie)
        {
            throw new NotImplementedException();
        }
    }
}
