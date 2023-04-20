

namespace IntegrationTests;


[TestFixture]
public abstract class BaseTextFixture
{

  [SetUp]
  public async Task TestSetup()
  {
    await DbSetupFixture.ResetState();
  }
}