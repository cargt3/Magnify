using Magnify.Repository;
using Magnify.Repository.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Magnify.Application.Handlers
{
    public class BookShipmentNotification : INotification
    {
        public BookShipmentNotification(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class BookShipmentHandler : INotificationHandler<BookShipmentNotification>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public BookShipmentHandler(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        // code is not asynchronic because of in memory database
        public async Task Handle(BookShipmentNotification notification, CancellationToken cancellationToken)
        {
            var shipment = _shipmentRepository.Get(notification.Id);
            if (shipment.Booked == true)
                throw new Exception("Shipment is already booked");

            _shipmentRepository.Book(notification.Id);
        }
    }
}
