using System.Collections.Generic;

namespace CNRailway.Util
{
    public interface IUserInterface
    {
        void ShowMessage(string message);
        void ShowErrorMessage(string message);
        void Wait();
        char GetDestination();
        IEnumerable<IEnumerable<char>> GetSortingLines();
    }
}
