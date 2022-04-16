using Magnify.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magnify.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private static List<Shipment> _shipments = new List<Shipment>();

        public void Add(Shipment shipment)
        {
            shipment.Id = _shipments.Any() ? _shipments.Max(x => x.Id) + 1 : 0;

            _shipments.Add(shipment);
        }

        public void CreateOffer(int id, decimal price)
        {
            var shipment = _shipments.First(x => x.Id == id);
            shipment.Price = price;
        }

        public void Book(int id)
        {
            var shipment = _shipments.First(x => x.Id == id);
            shipment.Booked = true;
            shipment.TimeStamp = DateTime.UtcNow;
        }

        public void SetApproveStatus(int id, bool status)
        {
            var shipment = _shipments.First(x => x.Id == id);
            shipment.Status = status;
        }

        public IEnumerable<Shipment> GetAll()
        {
            return _shipments;
        }

        public Shipment Get(int id)
        {
            return _shipments.First(x => x.Id == id);
        }

    }
}
