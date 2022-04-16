using Magnify.Repository;
using Magnify.Repository.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Magnify.Application.Handlers
{
    public class RequestShipmentNotification : INotification
    {
        public RequestShipmentNotification(string pickupAddress, string destinationAddress, decimal budgetAmount, string additionalInformation)
        {
            PickupAddress = pickupAddress;
            DestinationAddress = destinationAddress;
            BudgetAmount = budgetAmount;
            AdditionalInformation = additionalInformation;
        }

        public string PickupAddress { get; }
        public string DestinationAddress { get; }
        public decimal BudgetAmount { get; }
        public string AdditionalInformation { get; }
    }

    public class RequestShipmentHandler : INotificationHandler<RequestShipmentNotification>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public RequestShipmentHandler(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        // code is not asynchronic because of in memory database
        public async Task Handle(RequestShipmentNotification notification, CancellationToken cancellationToken)
        {
            _shipmentRepository.Add(new Shipment
            {
                AdditionalInformation = notification.AdditionalInformation,
                BudgetAmount = notification.BudgetAmount,
                DestinationAddress = notification.DestinationAddress,
                PickupAddress = notification.PickupAddress
            });
        }
    }
}
