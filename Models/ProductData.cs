using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProductData
    {
        [Key]
        public string ZamroID { get; set; }
        public string Name { get; set; }      
        public string Description { get; set; }   
        public int MinOrderQuantity { get; set; }    
        public string UnitOfMeasure { get; set; }   
        public int CategoryID { get; set; }   
        public double PurchasePrice { get; set; }
        public int Available { get; set; }   
    }
}
