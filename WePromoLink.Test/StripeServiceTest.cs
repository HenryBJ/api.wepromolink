using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WePromoLink.DTO.SubscriptionPlan;
using WePromoLink.Models;
using WePromoLink.Services;
using WePromoLink.Services.Email;
using WePromoLink.Services.Marketing;
using WePromoLink.Services.SubscriptionPlan;

namespace WePromoLink.Test;

public class StripeServiceTest : BaseTest
{

    private readonly StripeService? _service;

    public StripeServiceTest()
    {
        _service = _serviceProvider?.GetRequiredService<StripeService>();
    }

    [Fact]
    public async Task GetWithdrawRequests_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("StripeService null");

        try
        {
            var result = await _service.GetAllWitdrawRequests(1, 20);
            Assert.NotNull(result);
        }
        finally
        {

        }
    }

    [Fact]
    public async Task ApprovedWithdrawRequest_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("StripeService null");

        try
        {
            var result = await _service.GetAllWitdrawRequests(1, 20);
            Assert.NotNull(result);
        }
        finally
        {

        }
    }


}