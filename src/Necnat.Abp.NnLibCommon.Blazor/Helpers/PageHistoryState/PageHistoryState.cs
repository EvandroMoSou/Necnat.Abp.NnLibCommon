using System.Collections.Generic;
using System.Linq;

namespace Necnat.Abp.NnLibCommon.Blazor.Helpers
{
    public class PageHistoryState : IPageHistoryState
    {
        private List<PageHistoryComponent> _previousPages;

        public PageHistoryState()
        {
            _previousPages = new List<PageHistoryComponent>();
        }

        public void AddPageToHistory(string pageName, object obj)
        {
            _previousPages.Add(new PageHistoryComponent(pageName, obj));
        }

        public PageHistoryComponent? GetGoBackPage()
        {
            if (_previousPages.Count > 0)
                return _previousPages.ElementAt(_previousPages.Count - 1);

            return null;
        }

        public bool CanGoBack()
        {
            return _previousPages.Count > 0;
        }

        public void RemoveLastPage()
        {
            var goBackPage = GetGoBackPage();
            if (goBackPage != null)
                _previousPages.Remove(goBackPage);
        }

        public void Clear()
        {
            _previousPages = new List<PageHistoryComponent>();
        }
    }

    public class PageHistoryComponent
    {
        public PageHistoryComponent(string uri, object? data)
        {
            Uri = uri;
            Data = data;
        }

        public string Uri { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}