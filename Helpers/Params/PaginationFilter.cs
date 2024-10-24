namespace Sigmatech.Helpers.Params
{
    public class PaginationFilter
    {
        
        public int page { get; set; }

        public int limit { get; set; }

        public string sort { get; set; }

        public string sortType { get; set; }

    public PaginationFilter()
    {

        this.page = 1;

        this.limit = 10;

        this.sort = "id";

        this.sortType = "ASC";

    }

    public PaginationFilter(int page, int limit, string sort, string sortType)
    {

        this.page = page < 1 ? 1 : page;

        this.limit = limit < 1 ? 1 : limit;

        this.sort = sort == null ? "id" : sort;

        this.sortType = sortType == null ? "ASC" : sortType;

    }
        
    }
}