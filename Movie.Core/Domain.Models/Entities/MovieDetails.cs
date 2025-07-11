﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Core.Domain.Models.Entities
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int Budget { get; set; }
        //navigation property to ling Movie and Movie details
        public Movies Movie { get; set; } = null!;

    }
}
