using FluentAssertions;
using Magnify.Application.Handlers;
using Magnify.Repository;
using Magnify.Repository.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Magnify.Application.Tests
{
    public class BookShipmentHandlerTests
    {
        private readonly BookShipmentHandler _sut;
        private readonly Mock<IShipmentRepository> _shipmentRepository;

        public BookShipmentHandlerTests()
        {
            _shipmentRepository = new Mock<IShipmentRepository>();

            _sut = new BookShipmentHandler(_shipmentRepository.Object);
        }

        [Fact]
        public async Task SetApproveStatus_Success()
        {
            // Arrange
            var shipment = new Shipment
            {
                Id = 0,
                Status = null
            };

            _shipmentRepository.Setup(x => x.Get(0))
                .Returns(shipment);

            // Act // Arrange
            await _sut.Handle(new BookShipmentNotification(0), default);
        }

        [Fact]
        public async Task CreateOffer_ShipmentIsAlreadyBooked()
        {
            // Arrange
            var shipment = new Shipment
            {
                Id = 0,
                Booked = true
            };

            _shipmentRepository.Setup(x => x.Get(0))
                .Returns(shipment);

            Func<Task> action = async () => { await _sut.Handle(new BookShipmentNotification(0), default); };

            // Act // Arrange
            await action.Should().ThrowAsync<Exception>().WithMessage("Shipment is already booked");
        }
    }
}
