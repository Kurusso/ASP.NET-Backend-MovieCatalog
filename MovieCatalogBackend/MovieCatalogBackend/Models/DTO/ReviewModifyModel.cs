using System.ComponentModel.DataAnnotations;

namespace MovieCatalogBackend.Models.DTO
{
    public class ReviewModifyModel
    {
        public string ReviewText { get; set; }
        [Range(0,10)]
        public int Rating { get; set; }
        public bool IsAnonymus { get; set; }
    }
}
