using System.Collections.Generic;
using Models;

namespace Utils.Csv
{
    public interface ICsvParse
    {
        IEnumerable<ProductDataDto> Parse(string path);
    }
}