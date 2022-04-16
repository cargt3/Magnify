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
    public class CreateOfferHandlerTests
    {
        private readonly CreateOfferHandler _sut;
        private readonly Mock<IShipmentRepository> _shipmentRepository;

        public CreateOfferHandlerTests()
        {
            _shipmentRepository = new Mock<IShipmentRepository>();

            _sut = new CreateOfferHandler(_shipmentRepository.Object);
        }

        [Fact]
        public async Task CreateOffer_Success()
        {
            // Arrange
            var shipment = new Shipment
            {
                Id = 0,
            };

            _shipmentRepository.Setup(x => x.Get(0))
                .Returns(shipment);

            // Act // Arrange
            await _sut.Handle(new CreateOfferNotification(0, 1m), default);
        }

        [Fact]
        public async Task CreateOffer_OfferAlreadyCreated()
        {
            // Arrange
            var shipment = new Shipment
            {
                Id = 0,
                Price = 1m
            };

            _shipmentRepository.Setup(x => x.Get(0))
                .Returns(shipment);

            Func<Task> action = async () => { await _sut.Handle(new CreateOfferNotification(0, 1m), default); };

            // Act // Arrange
            await action.Should().ThrowAsync<Exception>().WithMessage("Offer has been already created");
        }

        [Fact]
        public async Task CreateOffer_CannotCreateOfferForABookedShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                Id = 0,
                Booked = true
            };

            _shipmentRepository.Setup(x => x.Get(0))
                .Returns(shipment);

            Func<Task> action = async () => { await _sut.Handle(new CreateOfferNotification(0, 1m), default); };

            // Act // Arrange
            await action.Should().ThrowAsync<Exception>().WithMessage("Cannot create offer for a booked shipment");
        }
    }
}
