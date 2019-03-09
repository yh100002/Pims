using System;
using System.Threading.Tasks;
using Persistence;
using Events;
using MassTransit;
using Models;

namespace Messages
{
    public class ProductDeleteEventConsumer : IConsumer<ProductDeleteEvent>
    {
        private readonly IUnitOfWork uow;

        public ProductDeleteEventConsumer(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task Consume(ConsumeContext<ProductDeleteEvent> context)
        {
            var repo = this.uow.GetRepository<ProductData>();          

            Console.WriteLine("Consume=======>" + context.Message.ZamroID);
            repo.Delete(context.Message.ZamroID);

            this.uow.SaveChanges();
        }           
    }
}