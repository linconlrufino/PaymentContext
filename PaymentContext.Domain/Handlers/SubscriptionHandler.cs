using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler :
    Notifiable<Notification>,
    IHandler<CreateBoletoSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        //Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar sua assianturea");
        }

        // verificar se document ja esta cadastrado
        if (_repository.DocumentExists(command.Document))
        {
            AddNotification("Document", "Este CPF já está em uso");
            //return new CommandResult(false, "Este CPF já está em uso");
        }

        //verificar se email ja esta cadastrado
        if (_repository.EmailExists(command.Email))
        {
            AddNotification("Email", "Este Email já está em uso");
            //return new CommandResult(false, "Este Email já está em uso");
        }

        //Gerar os VOs
        var name = new Name(command.FirstName, command.LastName);

        var document = new Document(command.Document, EDocumentType.CPF);

        var email = new Email(command.Email);

        var address = new Address(command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.Country,
            command.ZipCode
        );

        //Geras as entendidas
        var student = new Student(name, document, email, address);

        var subscription = new Subscription(DateTime.Now.AddMonths(1));

        var payment = new BoletoPayment(
            command.BarCode,
            command.BoletoNumber,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            command.Payer,
            new Document(command.PayerDocument, command.PayerDocumentType),
            address,
            email
        );

        //relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        //aplicar as validações 
        AddNotifications(name, document, email, address, student, subscription, payment);

        //Salvar as informações
        _repository.CreateSubscription(student);

        //enviar emails de boas vindas
        _emailService.Send(student.ToString(),
            student.Email.Adress,
            "Bem vindo ao balta.io",
            "sua assinatura foi criada"
        );

        //retornar informações

        return new CommandResult(true, "Assinatura realizada com sucesso");
    }
}