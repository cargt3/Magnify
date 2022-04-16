using Magnify.Repository;
using Magnify.Repository.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Magnify.Application.Handlers
{
    public class SetApproveStatusNotification : INotification
    {
        public SetApproveStatusNotification(int id, bool status)
        {
            Id = id;
            Status = status;
        }

        public int Id { get; }
        public bool Status { get; }
    }

    public class SetApproveStatusHandler : INotificationHandler<SetApproveStatusNotification>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public SetApproveStatusHandler(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        // code is not asynchronic because of in memory database
        public async Task Handle(SetApproveStatusNotification notification, CancellationToken cancellationToken)
        {
            var shipment = _shipmentRepository.Get(notification.Id);
            if (shipment.Price == null)
                throw new Exception("Cannot Approve/Reject not existing offer");

            _shipmentRepository.SetApproveStatus(notification.Id, notification.Status);
        }
    }
}
