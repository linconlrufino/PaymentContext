using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Repositories;

public class FakeStudentRepository : IStudentRepository
{
    private IList<Student> students = new List<Student>();

    public void CreateSubscription(Student student)
    {
        students.Add(student);
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