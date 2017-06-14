using System;
using System.Collections.Generic;

namespace LotteryService.Application
{
    public class PageList<T> : IPageList<T>
        where T : class
    {
        private ICollection<T> _currentPageList;

        private int _totalCount;

        private int _currentPageIndex;

        private int _pageSize;

        public PageList(ICollection<T> currentPageList, int totalCount, int currentPageIndex, int pageSize)
        {
            _currentPageList = currentPageList;
            _totalCount = totalCount;
            _currentPageIndex = currentPageIndex;
            _pageSize = pageSize;
        }

        public ICollection<T> CurrentPageList {
            get { return _currentPageList; } 
        }

        public int TotalCount {
            get { return _totalCount; }
        }

        public int CurrentPageIndex {
            get { return _currentPageIndex; }
        }

        public int PageSize {
            get { return _pageSize; }
        }

        public int TotalPageIndex {

            get
            {
                return Convert.ToInt32(Math.Ceiling((double)TotalCount / PageSize));
            }
        }

        public bool IsLastPage
        {
            get { return TotalPageIndex <= CurrentPageIndex; }
        }
    }
}