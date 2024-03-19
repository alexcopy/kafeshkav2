using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using KafeshkaV2.BL.validators.payment;
using KafeshkaV2.Controllers;
using KafeshkaV2.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KafeshkaV2.Tests.Controllers;

[TestSubject(typeof(PaymentDetailController))]
public class PaymentDetailControllerTest
{
    private readonly Mock<IPaymentDetail> _paymentDetailServiceMock;
    private readonly PaymentDetailController _controller;

    public PaymentDetailControllerTest()
    {
        _paymentDetailServiceMock = new Mock<IPaymentDetail>();

    }

    [Fact]
    public async Task GetAll_ReturnsListOfPaymentDetails()
    {
    }
}