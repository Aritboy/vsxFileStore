using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsxFileStore
{
    public class DataFile
    {
        public int m_size = int.MaxValue;
        public int m_calls = 1;
        public string m_FileName;
        public List<string> m_SourceFilePathList = new List<string>();

        public DataFile(string FileName, string SourceFilePath)
        {
            m_FileName = FileName;
            m_SourceFilePathList.Add(SourceFilePath);
        }
    }
}
