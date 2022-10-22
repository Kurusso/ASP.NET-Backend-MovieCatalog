namespace MovieCatalogBackend.Models.DTO
{
    public class MoviesPagedListModel
    {
        public ICollection<MovieElementModel> Movies { get; set; }
        public PageInfoModel PageInfo { get; set; }
    }
}
