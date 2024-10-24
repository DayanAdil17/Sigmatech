namespace Sigmatech.Exceptions.Base;

public class ObjectError
{
    public static object FormatObjectError(object message, string code, object errors)
    {
        return new { message = message, code = code, errors = errors };
    }
}