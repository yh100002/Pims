 

namespace Events
{
    public class ProductCreateEvent : IntegrationEvent
    {
        public string ZamroID { get; set; }
        public string Name { get; set; }      
        public string Description { get; set; }   
        public int MinOrderQuantity { get; set; }    
        public string UnitOfMeasure { get; set; }   
        public int CategoryID { get; set; }   
        public double PurchasePrice { get; set; }
        public int Available { get; set; }   
        public ProductCreateEvent(string ZamroID, string Name, string Description, int MinOrderQuantity, 
        string UnitOfMeasure, int CategoryID, double PurchasePrice, int Available)
        {
            this.ZamroID = ZamroID;
            this.Name = Name;
            this.Description = Description;
            this.MinOrderQuantity = MinOrderQuantity;
            this.UnitOfMeasure = UnitOfMeasure;
            this.CategoryID = CategoryID;
            this.PurchasePrice = PurchasePrice;
            this.Available = Available;
        }
    }
}
