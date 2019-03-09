using System.Threading.Tasks;
using Persistence;
using Events;
using MassTransit;
using Models;

namespace Messages
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
                Available = context.Message.Available,
                Timestamp = context.Message.CreationDate
            };

            await repo.AddAsync(product);    

            this.uow.SaveChanges();

        }
    }
}