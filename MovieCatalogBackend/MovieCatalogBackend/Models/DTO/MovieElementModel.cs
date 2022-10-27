﻿namespace MovieCatalogBackend.Models.DTO
{
    public class MovieElementModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Poster { get; set; }
        public int Year { get; set; }
        public string? Country { get; set; }
        public ICollection<GenreModel> Genres { get; set; }
        public ICollection<ReviewShortModel>? Reviews { get; set; }
    }
}
