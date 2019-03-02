using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using Models;

namespace WebCommon.Csv
{
    public class CsvParse : ICsvParse
    {
        public IEnumerable<ProductData> Parse(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {   
                List<ProductData> list = new List<ProductData>();                
                while( csv.Read() )
                {
                    try
                    {
                        var record = csv.GetRecord<ProductData>();
                        list.Add(record);
                        //Console.WriteLine(record.Name);
                    }
                    catch(Exception)
                    {                        
                        continue;
                    }
                }
                return list;
            }                       
        }
    }
}