using Magnify.Repository;
using Magnify.Repository.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Magnify.Application.Handlers
{
    public class CreateOfferNotification : INotification
    {
        public CreateOfferNotification(int id, decimal price)
        {
            Id = id;
            Price = price;
        }

        public int Id { get; }
        public decimal Price { get; }
    }

    public class CreateOfferHandler : INotificationHandler<CreateOfferNotification>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public CreateOfferHandler(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        // code is not asynchronic because of in memory database
        public async Task Handle(CreateOfferNotification notification, CancellationToken cancellationToken)
        {
            var shipment = _shipmentRepository.Get(notification.Id);
            if (shipment.Booked == true)
                throw new Exception("Cannot create offer for a booked shipment");
            if (shipment.Price != null)
                throw new Exception("Offer has been already created");

            _shipmentRepository.Book(notification.Id);
        }
    }
}
