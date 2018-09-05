using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Services
{
    public class PaginationService<T>
    {
        public IEnumerable<T> GetEventsPagination(ICollection<T> allItems, int pageSize, int pageIndex)
        {
            IEnumerable<T> result = new List<T>();

            if (ParameterValidation(pageIndex, pageSize, allItems.Count))
            {
                return allItems.Skip(pageIndex * pageSize).Take(CalcNumberOfItemsOnPage(pageIndex, pageSize, allItems.Count));
            }
            return result;
        }

        private bool ParameterValidation(int pageIndex, int pageSize, int itemCount)
        {
            if (pageSize * (pageIndex) < itemCount)
            {
                return true;
            }
            return false;
        }

        private int CalcNumberOfItemsOnPage(int pageIndex, int pageSize, int itemCount)
        {
            if ((pageIndex + 1) * pageSize > itemCount)
            {
                return itemCount - (pageSize * (pageIndex));
            }
            return pageSize;
        }
    }
}
