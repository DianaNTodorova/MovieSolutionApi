using Movie.Core.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Domain.Contracts
{
    public interface IGenreRepository
    {
       
        Task<Genre> GetByIdAsync(int genreId);

    }
}
