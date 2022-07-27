using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests;

[TestClass]
public class CreateBoletoSubscriptionCommandTests
{
    //Red, Green , Refactor

    [TestMethod]
    public void ShouldReturnErrorWhenNameIsInvalid()
    {
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "";

        command.Validate();

        Assert.IsFalse(command.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenNameIsValid()
    {
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "Tolstoi";
        command.LastName = "EcoBag";

        command.Validate();

        Assert.IsTrue(command.IsValid);
    }
}