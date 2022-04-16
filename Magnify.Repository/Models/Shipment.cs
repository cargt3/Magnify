using System;

namespace Magnify.Repository.Models
{
    public class Shipment
    {
        public string PickupAddress { get; set; }
        public string DestinationAddress { get; set; }
        public decimal BudgetAmount { get; set; }
        public string AdditionalInformation { get; set; }
        public int Id { get; set; }
        public bool Booked { get; set; }
        public decimal? Price { get; set; }
        public bool? Status { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
