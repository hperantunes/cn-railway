namespace CNRailway.Util
{
    public interface IUserInterface
    {
        string GetDirectoryPath();
        string GetFileName();
        string GetFullFilePath();
        void ShowErrorMessage(string message);
        char GetDestination();
    }
}
