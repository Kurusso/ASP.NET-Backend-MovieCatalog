namespace MovieCatalogBackend.Models.DTO
{
    public class PageModel
    {
        public ICollection<MovieElementModel> MovieElements { get; set; }
        public PageInfoModel PageInfoModel { get; set; }
    }
}
