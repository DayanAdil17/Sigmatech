using System.Net;
using Sigmatech.Exceptions.Base;

namespace Sigmatech.Exceptions.Global;

public class NotFoundGlobalException : BaseException
{
    private readonly string moduleName;
    private readonly string errorCode;
    private readonly string nameId;
    private readonly object obj;
        
    public NotFoundGlobalException(string moduleName, string errorCode, string nameId, object obj) : base()
    {
        this.moduleName = moduleName;
        this.errorCode = errorCode;
        this.nameId = nameId;
        this.obj = obj;
    }

    public override string Message => string.Format("{0} Not Found!", this.moduleName);

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override string Code => string.Format("{0}/NOT-FOUND-EXCEPTION", this.errorCode.ToUpper());

    public override Dictionary<string, object> Errors => new Dictionary<string, object>
    {
        { this.nameId, new List<object>() { String.Format("{0} NOT-FOUND", this.obj) } }
    };
}