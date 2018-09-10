using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Services
{
    public class PaginationService
    {
        public bool ParameterValidation(int pageIndex, int pageSize, Task<int> itemCount)
        {
            if (pageSize * (pageIndex) < itemCount)
            {
                return true;
            }
            return false;
        }

        public int CalcNumberOfItemsOnPage(int pageIndex, int pageSize, int itemCount)
        {
            if ((pageIndex + 1) * pageSize > itemCount)
            {
                return itemCount - (pageSize * (pageIndex));
            }
            return pageSize;
        }
    }
}
