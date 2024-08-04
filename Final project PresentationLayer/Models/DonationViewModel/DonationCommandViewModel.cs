namespace Final_project_PresentationLayer.Models.DonationViewModel
{
    public class DonationCommandViewModel
    {
        public int DonationType { get; set; }
        public PaginationFilter filter { get; set; }
    }

    public class PaginationFilter
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalRecords { get; }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
