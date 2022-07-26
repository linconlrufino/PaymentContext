using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities;

public class PayPalPayment : Payment
{
    public PayPalPayment(
        string transactionCode,
        DateTime date,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        string payer,
        Document document,
        Adress address,
        Email email
       ) : base(
            date,
            expireDate,
            total,
            totalPaid,
            payer,
            document,
            address,
            email)
    {
        TransactionCode = transactionCode;
    }

    public string TransactionCode { get; private set; }
}