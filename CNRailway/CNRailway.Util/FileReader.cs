using System.Collections.Generic;
using System.IO;

namespace CNRailway.Util
{
    public class FileReader : IFileReader
    {
        public IList<char[]> ReadFrom(string file)
        {
            if (!File.Exists(file))
            {
                throw new System.ArgumentException($"File {file} does not exist.");
            }

            string line;
            var lines = new List<char[]>();

            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line.ToCharArray());
                }
            }

            return lines;
        }
    }
}
