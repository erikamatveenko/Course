using AccountingOfVehicles.Utils;
using System;

namespace AccountingOfVehicles.ViewModels
{
    //Класс для хранения информации о страницах разбиения
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public IParameters Parameters { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize, IParameters parameters)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Parameters = parameters;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
