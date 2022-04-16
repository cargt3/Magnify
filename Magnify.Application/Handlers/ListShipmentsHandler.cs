using Magnify.Application.Dtos;
using Magnify.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Magnify.Application.Handlers
{
    public class ListShipmentsRequest : IRequest<IEnumerable<Shipment>>
    {
    }

    public class ListShipmentsHandler : IRequestHandler<ListShipmentsRequest, IEnumerable<Shipment>>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ListShipmentsHandler(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        // code is not asynchronic because of in memory database
        public async Task<IEnumerable<Shipment>> Handle(ListShipmentsRequest request, CancellationToken cancellationToken)
        {
            return _shipmentRepository.GetAll().Select(x => new Shipment(x.PickupAddress, x.DestinationAddress, x.BudgetAmount, x.AdditionalInformation, x.Id, x.Booked, x.Price, x.Status, x.TimeStamp));
        }

    }
}
