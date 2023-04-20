using Entities.Models;
using Repository;
using Shared.RequestHelper;
using IntegrationTests.TestData;

using static IntegrationTests.DbSetupFixture;

namespace IntegrationTests;

[TestFixture]
public class Tests : BaseTextFixture
{

    private CustomerRepository _customerRepository = null!;

    [OneTimeSetUp]
    public void Init()
    {
        _customerRepository = new CustomerRepository(Context!);
    }

    [Test]
    public async Task Test1()
    {
        var fakeCustomer = Fake.Customer with
        {
            Email = "edu@exemplo.com"
        };
        var customer = Mapper.Map<Customer>(fakeCustomer);

        _customerRepository.CreateCustomer(customer);

        await Context!.SaveChangesAsync();

        var result = await _customerRepository.GetByIdAsync(customer.Id, trackChanges: false);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(customer.Name));
    }

    [Test]
    public async Task Test2()
    {
        var customer = Mapper.Map<Customer>(Fake.Customer);

        _customerRepository.CreateCustomer(customer);

        await Context!.SaveChangesAsync();

        var result = await _customerRepository.GetAllAsync(new CustomerParams { }, trackChanges: false);

        Assert.That(result.Count(), Is.EqualTo(1));
    }
}