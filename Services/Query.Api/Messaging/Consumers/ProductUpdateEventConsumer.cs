using System.Threading.Tasks;
using Data;
using Events;
using MassTransit;
using Models;


namespace Query.Api.Messaging.Consumers
{
    public class ProductUpdateEventConsumer : IConsumer<ProductUpdateEvent>
    {
        private readonly IUnitOfWork uow;

        public ProductUpdateEventConsumer(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task Consume(ConsumeContext<ProductUpdateEvent> context)
        {
            var repo = this.uow.GetRepositoryAsync<ProductData>();

            var product = new ProductData()
            {
                ZamroID = context.Message.ZamroID,
                Name = context.Message.Name,
                Description = context.Message.Description,
                MinOrderQuantity = context.Message.MinOrderQuantity,
                UnitOfMeasure = context.Message.UnitOfMeasure,
                CategoryID = context.Message.CategoryID,
                PurchasePrice = context.Message.PurchasePrice,
                Available = context.Message.Available
            };

            repo.UpdateAsync(product);

            this.uow.SaveChanges();
        }       
    }
}