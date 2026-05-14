namespace LowPolyBacklogWeb.Models
{
    public class LibraryViewModel
    {
        public PagedResponse<GameViewModel> Games { get; set; } = new PagedResponse<GameViewModel>();

        public GameFilterViewModel Filters { get; set; } = new GameFilterViewModel();
    }
}
