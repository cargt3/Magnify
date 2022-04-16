using Magnify.Application;
using Magnify.Application.Handlers;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Magnify.Console
{
    public class ConsoleClient : IConsoleClient
    {
        private readonly IMediator _mediator;

        public ConsoleClient(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ShowCarrierInterface()
        {
            ConsoleKeyInfo key;
            do
            {
                System.Console.Clear();
                System.Console.WriteLine("1. Book 2. Create offer 0. Exit");
                await ShowShipments();
                key = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        await Book();
                        break;
                    case ConsoleKey.D2:
                        await CreateOffer();
                        break;
                }
            }
            while (key.Key != ConsoleKey.D0);
        }

        public async Task ShowShipperInterface()
        {
            ConsoleKeyInfo key;
            do
            {
                System.Console.Clear();
                System.Console.WriteLine("1. Create new Shipment 2. Approve, 3 Reject 0. Exit");
                await ShowShipments();
                key = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        await CreateNewShipment();
                        break;
                    case ConsoleKey.D2:
                        await SetApproveStatus(true);
                        break;
                    case ConsoleKey.D3:
                        await SetApproveStatus(false);
                        break;
                }
            }
            while (key.Key != ConsoleKey.D0);
        }

        private async Task SetApproveStatus(bool status)
        {
            System.Console.Write("Id: ");
            var idString = System.Console.ReadLine();
            if (!int.TryParse(idString, out int id))
            {
                System.Console.WriteLine($"Error {id} should be number");
                System.Console.ReadKey();
                return;
            }
            await _mediator.Publish(new SetApproveStatusNotification
            (
                id: id,
                status: status
            ));
            System.Console.WriteLine("Success");
        }

        private async Task CreateNewShipment()
        {
            string pickupAddress;
            string destinationAddress;
            decimal budgetAmount;
            string budgetAmountString;
            string additionalInformation;

            System.Console.Write("Pickup Address:");
            pickupAddress = System.Console.ReadLine();
            System.Console.Write("Destination Address:");
            destinationAddress = System.Console.ReadLine();
            System.Console.Write("Budget Amount:");
            budgetAmountString = System.Console.ReadLine();
            if (!decimal.TryParse(budgetAmountString, out budgetAmount))
            {
                System.Console.WriteLine($"Error {budgetAmount} should be number");
                System.Console.ReadKey();
                return;

            }
            System.Console.Write("Additional Information:");
            additionalInformation = System.Console.ReadLine();

            await _mediator.Publish(new RequestShipmentNotification
            (
                pickupAddress: pickupAddress,
                destinationAddress: destinationAddress,
                budgetAmount: budgetAmount,
                additionalInformation: additionalInformation
            ));
        }

        private async Task ShowShipments()
        {
            var shipments = await _mediator.Send(new ListShipmentsRequest());

            foreach (var shipment in shipments)
            {
                string status = shipment.Status == true ? "Approved" : shipment.Status == false ? "Disapprove" : "";
                System.Console.WriteLine($"shipment id: {shipment.Id}, Booked: {shipment.Booked}, BudgetAmount: {shipment.BudgetAmount}, PickupAddress: {shipment.PickupAddress}, DestinationAddress: {shipment.DestinationAddress}, AdditionalInformation: {shipment.AdditionalInformation}, Status: { status }, Time Stamp: {shipment.TimeStamp}");
            }
        }

        private async Task Book()
        {
            System.Console.Write("Id: ");
            var idString = System.Console.ReadLine();
            if (!int.TryParse(idString, out int id))
            {
                System.Console.WriteLine($"Error {id} should be number");
                System.Console.ReadKey();
                return;
            }
            await _mediator.Publish(new BookShipmentNotification
            (
                id: id
            ));
        }

        private async Task CreateOffer()
        {
            System.Console.Write("Id: ");
            var idStringOffer = System.Console.ReadLine();
            if (!int.TryParse(idStringOffer, out int idOffer))
            {
                System.Console.WriteLine($"Error {idOffer} should be number");
                System.Console.ReadKey();
                return;
            }
            System.Console.Write("Price: ");
            var priceString = System.Console.ReadLine();
            if (!int.TryParse(priceString, out int price))
            {
                System.Console.WriteLine($"Error {price} should be number");
                System.Console.ReadKey();
                return;
            }
            await _mediator.Publish(new CreateOfferNotification
            (
                id: idOffer,
                price: price
            ));
        }
    }
}
