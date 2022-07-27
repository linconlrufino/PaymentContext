using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Address _address;
    private readonly Email _email;
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Thorin", "Oakenshield");
        _document = new Document("12345678901", EDocumentType.CPF);
        _email = new Email("thorin@lotr.com");
        _address = new Address("Rua 1", "123", "bairro", "cidade", "uf", "country", "123");
        _student = new Student(_name, _document, _email, _address);
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("123", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "MoriaMining", _document, _address, _email);
        _subscription.AddPayment(payment);

        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenAddSubscription()
    {
        var payment = new PayPalPayment("123", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "MoriaMining", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.IsValid);
    }
}