using System.Threading.Tasks;
using Data;
using Events;
using MassTransit;
using Models;

namespace Query.Api.Messaging.Consumers
{
    public class ProductCreateEventConsumer : IConsumer<ProductCreateEvent>
    {
        private readonly IUnitOfWork uow;

        public ProductCreateEventConsumer(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        
        public async Task Consume(ConsumeContext<ProductCreateEvent> context)
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

            await repo.AddAsync(product);    

            this.uow.SaveChanges();

        }
    }
}