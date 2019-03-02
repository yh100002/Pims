 

namespace Events
{
    public class ProductDeleteEvent : IntegrationEvent
    {
        public string ZamroID { get; set; }

        public ProductDeleteEvent(string ZamroID)
        {
            this.ZamroID = ZamroID;         
        }
    }
}
