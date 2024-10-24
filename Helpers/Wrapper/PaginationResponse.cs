namespace Sigmatech.Helpers.Wrapper
{
    public class PaginationResponse<T>
    {

        public T data { get; set; }

        public int limit { get; set; }

        public int page { get; set; }

        public int total { get; set; }

        public PaginationResponse(int limit, int page, int total, T data)
        {

            this.data = data;

            this.page = page;

            this.limit = limit;

            this.total = total;

        }
        
    }
}