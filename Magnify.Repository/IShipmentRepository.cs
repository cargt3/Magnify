using Magnify.Repository.Models;
using System.Collections.Generic;

namespace Magnify.Repository
{
    public interface IShipmentRepository
    {
        void Add(Shipment shipment);
        void Book(int id);
        Shipment Get(int id);
        IEnumerable<Shipment> GetAll();
        void SetApproveStatus(int id, bool status);
    }
}