using System;

namespace Magnify.Application.Dtos
{
    public class Shipment
    {
        public Shipment(string pickupAddress, string destinationAddress, decimal budgetAmount, string additionalInformation, int id, bool booked, decimal? price, bool? status, DateTime? timeStamp)
        {
            PickupAddress = pickupAddress;
            DestinationAddress = destinationAddress;
            BudgetAmount = budgetAmount;
            AdditionalInformation = additionalInformation;
            Id = id;
            Booked = booked;
            Price = price;
            Status = status;
            TimeStamp = timeStamp;
        }

        public string PickupAddress { get; }
        public string DestinationAddress { get; }
        public decimal BudgetAmount { get; }
        public string AdditionalInformation { get; } 
        public int Id { get; }
        public bool Booked { get; }
        public decimal? Price { get; }
        public bool? Status { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
