using FluentValidation;

namespace MyError.Model;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string? Phone { get; set; }
    public int Course { get; set; }
}

public sealed class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name must be less than 100 characters");
        
        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Surname is required")
            .MaximumLength(100)
            .WithMessage("Surname must be less than 100 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Incorrect email format");
        
        RuleFor(x => x.Age)
            .NotEmpty()
            .WithMessage("Age is required")
            .InclusiveBetween(18, 80)
            .WithMessage("Age must be between 18 and 80");
        
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone is required")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Incorrect phone number");
        
        RuleFor(x => x.Course)
            .NotEmpty()
            .WithMessage("Course is required")
            .InclusiveBetween(1, 6)
            .WithMessage("Course must be between 1 and 6");;
    }
}