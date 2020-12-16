using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject_Team5_Armoire.ViewModels
{
    public class PaginationInfoVM
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
        public int CategoryId { get; set; }
    }
}
