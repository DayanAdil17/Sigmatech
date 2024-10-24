using System.Globalization;

namespace Sigmatech.Exceptions.Base;

public abstract class BaseException : Exception
{
    public abstract int StatusCode { get; }
    public abstract string Code { get; }

    public abstract Dictionary<string, object> Errors{ get; }
    
    public BaseException() : base() {
            
    }
    
    public BaseException(string message) : base(message) {
            
    }
    
    public BaseException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) {
            
    }
}