using PaymentContext.Shared.ValuesObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string adress)
    {
        Adress = adress;
    }

    public string Adress { get; private set; }
}