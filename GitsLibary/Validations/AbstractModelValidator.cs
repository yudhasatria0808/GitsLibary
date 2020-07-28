using FluentValidation;

namespace GitsLibary.Validations
{
    public abstract class AbstractModelValidator<T>: AbstractValidator<T> where T : class
    {

    }
}
