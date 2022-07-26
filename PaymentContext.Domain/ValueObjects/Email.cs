using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string adress)
    {
        Adress = adress;

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsEmail(adress, "Email.Adress", "E-mail inválido")
        );
    }

    public string Adress { get; private set; }
}