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
    public class SetApproveStatusHandlerTests
    {
        private readonly SetApproveStatusHandler _sut;
        private readonly Mock<IShipmentRepository> _shipmentRepository;

        public SetApproveStatusHandlerTests()
        {
            _shipmentRepository = new Mock<IShipmentRepository>();

            _sut = new SetApproveStatusHandler(_shipmentRepository.Object);
        }

        [Fact]
        public async Task SetApproveStatus_Success()
        {
            // Arrange
            var shipment = new Shipment
            {
                Id = 0,
                Price = 1m
            };

            _shipmentRepository.Setup(x => x.Get(0))
                .Returns(shipment);

            // Act // Arrange
            await _sut.Handle(new SetApproveStatusNotification(0, true), default);
        }

        [Fact]
        public async Task CreateOffer_CannotCreateOfferForABookedShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                Id = 0,
                Price = null
            };

            _shipmentRepository.Setup(x => x.Get(0))
                .Returns(shipment);

            Func<Task> action = async () => { await _sut.Handle(new SetApproveStatusNotification(0, true), default); };

            // Act // Arrange
            await action.Should().ThrowAsync<Exception>().WithMessage("Cannot Approve/Reject not existing offer");
        }
    }
}
