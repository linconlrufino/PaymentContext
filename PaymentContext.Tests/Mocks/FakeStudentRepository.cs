using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Repositories;

public class FakeStudentRepository : IStudentRepository
{
    // private IList<Document> documents = new List<Document>();
    // private IList<Email> emails = new List<Email>();
    private IList<Student> students = new List<Student>();

    public void CreateSubscription(Student student)
    {
        students.Add(student);
        // documents.Add(student.Document);
        // emails.Add(student.Email);
    }

    public bool DocumentExists(string document)
    {
        var findDocument = this.students.Any(x => x.Document?.Number == document);

        if (findDocument)
            return true;
        else
            return false;
    }

    public bool EmailExists(string email)
    {
        var findEmail = this.students.Any(x => x.Email.Adress == email);

        if (findEmail)
            return true;
        else
            return false;
    }
}