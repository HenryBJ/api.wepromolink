using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WePromoLink.DTO.SubscriptionPlan;
using WePromoLink.Models;
using WePromoLink.Services.Email;
using WePromoLink.Services.Marketing;
using WePromoLink.Services.SubscriptionPlan;

namespace WePromoLink.Test;

public class SubscriptionPlanServiceTest : BaseTest
{

    private readonly ISubPlanService? _service;

    public SubscriptionPlanServiceTest()
    {
        _service = _serviceProvider?.GetRequiredService<ISubPlanService>();
    }

    [Fact]
    public async Task Create_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("SubPlanService null");

        SubscriptionPlanCreate item = new SubscriptionPlanCreate
        {
            Annually = 200,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 20,
            Level = 1,
            Monthly = 20,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 1,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };

        SubscriptionPlanModel? plan = null;
        try
        {
            var result = await _service.Create(item);
            Assert.NotEqual(result, Guid.Empty);
            plan = await _db.SubscriptionPlans.Include(e => e.Features).Where(e => e.Id == result).SingleOrDefaultAsync();
            Assert.NotNull(plan);
            Assert.Equal("https://test.stripe", plan.AnnualyPaymantLink);
            Assert.Equal("123456", plan.AnnualyProductId);
            Assert.Equal(20, plan.Discount);
            Assert.Equal(1, plan.Level);
            Assert.Equal(20, plan.Monthly);
            Assert.Equal("https://test.stripe", plan.MonthlyPaymantLink);
            Assert.Equal("654321", plan.MonthlyProductId);
            Assert.Equal(1, plan.Order);
            Assert.Equal("stripe", plan.PaymentMethod);
            Assert.Equal("testing", plan.Tag);
            Assert.Equal("basic", plan.Title);
            Assert.NotNull(plan.Features);
            Assert.Equal(3, plan.Features.Count);
        }
        finally
        {
            if (plan != null)
            {
                _db.SubscriptionPlans.Remove(plan);
                _db.SaveChanges();
            }
        }
    }

    [Fact]
    public async Task Edit_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("SubPlanService null");

        SubscriptionPlanCreate item = new SubscriptionPlanCreate
        {
            Annually = 200,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 20,
            Level = 1,
            Monthly = 20,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 1,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };

        SubscriptionPlanModel? plan = null;
        try
        {
            var result = await _service.Create(item);

            SubscriptionPlanEdit itemEdited = new SubscriptionPlanEdit
            {
                Id = result,
                Annually = 201,
                AnnualyPaymantLink = "https://test.stripe1",
                AnnualyProductId = "1234561",
                Discount = 21,
                Monthly = 22,
                MonthlyPaymantLink = "https://test.stripe1",
                MonthlyProductId = "6543211",
                Order = 2,
                PaymentMethod = "stripe1",
                Tag = "testing1",
                Title = "basic1",
            };

            await _service.Edit(itemEdited);

            Assert.NotEqual(result, Guid.Empty);
            plan = await _db.SubscriptionPlans.Where(e => e.Id == result).SingleOrDefaultAsync();
            Assert.NotNull(plan);
            Assert.Equal("https://test.stripe1", plan.AnnualyPaymantLink);
            Assert.Equal("1234561", plan.AnnualyProductId);
            Assert.Equal(21, plan.Discount);
            Assert.Equal(1, plan.Level);
            Assert.Equal(22, plan.Monthly);
            Assert.Equal("https://test.stripe1", plan.MonthlyPaymantLink);
            Assert.Equal("6543211", plan.MonthlyProductId);
            Assert.Equal(2, plan.Order);
            Assert.Equal("stripe1", plan.PaymentMethod);
            Assert.Equal("testing1", plan.Tag);
            Assert.Equal("basic1", plan.Title);
        }
        finally
        {
            if (plan != null)
            {
                _db.SubscriptionPlans.Remove(plan);
                _db.SaveChanges();
            }
        }
    }

    [Fact]
    public async Task Delete_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("SubPlanService null");

        SubscriptionPlanCreate item = new SubscriptionPlanCreate
        {
            Annually = 200,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 20,
            Level = 1,
            Monthly = 20,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 1,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };

        SubscriptionPlanModel? plan = null;
        try
        {
            var result = await _service.Create(item);
            await _service.Delete(new SubscriptionPlanDelete { Id = result });
            plan = await _db.SubscriptionPlans.Where(e => e.Id == result).SingleOrDefaultAsync();
            Assert.Null(plan);
        }
        finally
        {
            if (plan != null)
            {
                _db.SubscriptionPlans.Remove(plan);
                _db.SaveChanges();
            }
        }
    }

    [Fact]
    public async Task Get_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("SubPlanService null");

        SubscriptionPlanCreate item = new SubscriptionPlanCreate
        {
            Annually = 200,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 20,
            Level = 1,
            Monthly = 20,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 1,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };

        SubscriptionPlanRead? plan = null;
        try
        {
            var result = await _service.Create(item);
            plan = await _service.Get(result);

            Assert.NotNull(plan);
            Assert.Equal("https://test.stripe", plan.AnnualyPaymantLink);
            Assert.Equal("123456", plan.AnnualyProductId);
            Assert.Equal(20, plan.Discount);
            Assert.Equal(20, plan.Monthly);
            Assert.Equal("https://test.stripe", plan.MonthlyPaymantLink);
            Assert.Equal("654321", plan.MonthlyProductId);
            Assert.Equal(1, plan.Order);
            Assert.Equal("stripe", plan.PaymentMethod);
            Assert.Equal("testing", plan.Tag);
            Assert.Equal("basic", plan.Title);
        }
        finally
        {
            if (plan != null)
            {
                await _service.Delete(new SubscriptionPlanDelete { Id = plan.Id });
            }
        }
    }

    [Fact]
    public async Task GetAll_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("SubPlanService null");

        SubscriptionPlanCreate item1 = new SubscriptionPlanCreate
        {
            Annually = 200,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 20,
            Level = 1,
            Monthly = 20,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 1,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };

        SubscriptionPlanCreate item2 = new SubscriptionPlanCreate
        {
            Annually = 400,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 40,
            Level = 2,
            Monthly = 40,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 2,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };

        Guid? result1 = null;
        Guid? result2 = null;
        try
        {
            result1 = await _service.Create(item1);
            result2 = await _service.Create(item2);
            var page = await _service.GetAll(1, 10);

            Assert.NotNull(page);
            Assert.NotNull(page.Items);
            Assert.NotNull(page.Pagination);
            Assert.Equal(1, page.Pagination.TotalPages);

        }
        finally
        {
            if (result1 != null)
            {
                await _service.Delete(new SubscriptionPlanDelete { Id = result1.Value });
            }
            if (result2 != null)
            {
                await _service.Delete(new SubscriptionPlanDelete { Id = result2.Value });
            }
        }
    }

    [Fact]
    public async Task Create_Feature_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("SubPlanService null");

        SubscriptionPlanCreate item = new SubscriptionPlanCreate
        {
            Annually = 200,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 20,
            Level = 1,
            Monthly = 20,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 1,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };


        SubscriptionPlanModel? plan = null;
        try
        {
            var result = await _service.Create(item);
            var feature = await _service.Create(new SubscriptionPlanFeatureCreate { Name = "Demo", BoolValue = true, SubscrioptionPlanId = result });

            Assert.NotEqual(feature, Guid.Empty);
            plan = await _db.SubscriptionPlans.Include(e => e.Features).Where(e => e.Id == result).SingleOrDefaultAsync();
            Assert.NotNull(plan);
            Assert.NotNull(plan.Features);
            Assert.True(plan.Features.Any(e => e.Name == "Demo"));

        }
        finally
        {
            if (plan != null)
            {
                _db.SubscriptionPlans.Remove(plan);
                _db.SaveChanges();
            }
        }
    }

    [Fact]
    public async Task Edit_Feature_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("SubPlanService null");

        SubscriptionPlanCreate item = new SubscriptionPlanCreate
        {
            Annually = 200,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 20,
            Level = 1,
            Monthly = 20,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 1,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };


        SubscriptionPlanModel? plan = null;
        try
        {
            var result = await _service.Create(item);
            var feature = await _service.Create(new SubscriptionPlanFeatureCreate { Name = "Demo", BoolValue = false, Value = "NOT", SubscrioptionPlanId = result });
            await _service.Edit(new SubscriptionPlanFeatureEdit { Id = feature, Name = "Demo1", Value = "yeah", BoolValue = true });

            Assert.NotEqual(feature, Guid.Empty);
            plan = await _db.SubscriptionPlans.Include(e => e.Features).Where(e => e.Id == result).SingleOrDefaultAsync();
            Assert.NotNull(plan);
            Assert.NotNull(plan.Features);
            Assert.NotNull(plan.Features.Where(e=>e.Id == feature).SingleOrDefault());
            Assert.True(plan.Features.Where(e=>e.Id == feature).Single().BoolValue);
            Assert.Equal("Demo1", plan.Features.Where(e=>e.Id == feature).Single().Name);
            Assert.Equal("yeah", plan.Features.Where(e=>e.Id == feature).Single().Value);
        }
        finally
        {
            if (plan != null)
            {
                _db.SubscriptionPlans.Remove(plan);
                _db.SaveChanges();
            }
        }
    }

    [Fact]
    public async Task Delete_Feature_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("SubPlanService null");

        SubscriptionPlanCreate item = new SubscriptionPlanCreate
        {
            Annually = 200,
            AnnualyPaymantLink = "https://test.stripe",
            AnnualyProductId = "123456",
            Discount = 20,
            Level = 1,
            Monthly = 20,
            MonthlyPaymantLink = "https://test.stripe",
            MonthlyProductId = "654321",
            Order = 1,
            PaymentMethod = "stripe",
            Tag = "testing",
            Title = "basic",
            Features = new List<SubscriptionPlanFeatureCreate>
            {
                new SubscriptionPlanFeatureCreate{Name = "Contain ads", BoolValue=true },
                new SubscriptionPlanFeatureCreate{Name = "Campaings", Value = "100" },
                new SubscriptionPlanFeatureCreate{Name = "Links", Value = "200" },
            }
        };


        SubscriptionPlanModel? plan = null;
        try
        {
            var result = await _service.Create(item);
            var feature = await _service.Create(new SubscriptionPlanFeatureCreate { Name = "Demo", BoolValue = false, Value = "NOT", SubscrioptionPlanId = result });
            await _service.Delete(new SubscriptionPlanFeatureDelete{Id = feature});

            Assert.NotEqual(feature, Guid.Empty);
            plan = await _db.SubscriptionPlans.Include(e => e.Features).Where(e => e.Id == result).SingleOrDefaultAsync();
            Assert.NotNull(plan);
            Assert.NotNull(plan.Features);
            Assert.Null(plan.Features.Where(e=>e.Id == feature).SingleOrDefault());
        }
        finally
        {
            if (plan != null)
            {
                _db.SubscriptionPlans.Remove(plan);
                _db.SaveChanges();
            }
        }
    }

}