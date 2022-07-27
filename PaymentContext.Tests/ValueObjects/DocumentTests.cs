using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    //Red, Green , Refactor

    [TestMethod]
    public void ShouldReturnErrorWhenCNPJIsInvalid()
    {
        var document = new Document("123", EDocumentType.CNPJ);
        Assert.IsFalse(document.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenCNPJIsValid()
    {
        var document = new Document("12345678901234", EDocumentType.CNPJ);
        Assert.IsTrue(document.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenCPFIsInvalid()
    {
        var document = new Document("123", EDocumentType.CPF);
        Assert.IsFalse(document.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenCPFIsValid()
    {
        var document = new Document("12345678901", EDocumentType.CPF);
        Assert.IsTrue(document.IsValid);
    }
}