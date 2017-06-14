using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace LotteryService.Application
{
    public interface IPageList<T>
        where T : class 
    {       
        ICollection<T> CurrentPageList { get;  }

        int TotalCount { get; }

        int CurrentPageIndex { get; }

        int PageSize { get;}

        int TotalPageIndex { get; }

        bool IsLastPage { get; }
    }
}