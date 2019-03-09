using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using Models;

namespace Utils.Csv
{
    public class CsvParse : ICsvParse
    {
        public IEnumerable<ProductDataDto> Parse(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {   
                List<ProductDataDto> list = new List<ProductDataDto>();                
                while( csv.Read() )
                {
                    try
                    {                        
                        var record = csv.GetRecord<ProductDataDto>();
                        list.Add(record);                        
                    }
                    catch(Exception e)
                    {           
                        //Console.WriteLine($"CsvParse====>{e.Message}");             
                        continue;
                    }
                }
                return list;
            }                       
        }
    }
}