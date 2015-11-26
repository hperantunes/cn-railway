using System.Collections.Generic;
using System.IO;

namespace CNRailway.Util
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadFrom(string file)
        {
            if (!File.Exists(file))
            {
                throw new System.ArgumentException($"File {file} does not exist.");
            }

            string line;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
