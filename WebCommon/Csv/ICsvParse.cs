using System.Collections.Generic;
using Models;

namespace WebCommon.Csv
{
    public interface ICsvParse
    {
        IEnumerable<ProductData> Parse(string path);
    }
}