using System.Collections.Generic;

namespace CNRailway.Util
{
    public interface IUserInterface
    {
        void ShowMessage(string message);
        void ShowErrorMessage(string message);
        void ShowList(IEnumerable<string> list);
        void BeginSection();
        void Wait();
        char GetDestination();
        IEnumerable<IEnumerable<char>> GetSortingLines();
        bool HasUserChosenToExit();
    }
}
