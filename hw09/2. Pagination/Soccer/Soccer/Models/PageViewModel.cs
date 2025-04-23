namespace Soccer.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
    public class TeamsIndexViewModel
    {
        public IEnumerable<Teams> Teams { get; }
        public PageViewModel PageViewModel { get; }

        public TeamsIndexViewModel(IEnumerable<Teams> teams, PageViewModel pageViewModel)
        {
            Teams = teams;
            PageViewModel = pageViewModel;
        }
    }
}
