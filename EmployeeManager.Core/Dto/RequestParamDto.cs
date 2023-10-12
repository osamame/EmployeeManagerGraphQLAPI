namespace EmployeeManager.Core.Dto
{
    public class RequestParamDto
    {
        private const int maxPageSize = 50;
        private const int minPageSize = 1;
        private int _pageSize = 10;

        private const int minPageNumber = 1;
        private int _pageNumber = 1;

        public int pageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = (value < minPageNumber) ? _pageNumber : value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : (value < 1) ? minPageSize : value; }
        }
    }
}
