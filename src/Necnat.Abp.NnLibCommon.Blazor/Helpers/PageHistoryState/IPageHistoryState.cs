namespace Necnat.Abp.NnLibCommon.Blazor.Helpers
{
    public interface IPageHistoryState
    {
        void AddPageToHistory(string pageName, object obj);
        PageHistoryComponent? GetGoBackPage();
        bool CanGoBack();
        void RemoveLastPage();
        void Clear();
    }
}
