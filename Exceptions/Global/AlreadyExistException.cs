using System.Net;
using Sigmatech.Exceptions.Base;

namespace src.Exceptions.Global
{
    public class AlreadyExistException : BaseException
    {
        private readonly string processName;

        private readonly string fieldName;

        private readonly string key;

        private readonly string value;
        public AlreadyExistException(string processName, string fieldName, string value) : base()
        {
            this.processName = processName;
            this.fieldName = fieldName;
            var trimmedFieldName = fieldName.Replace(" ", "");
            this.key = char.ToLower(trimmedFieldName[0]) + trimmedFieldName.Substring(1);
            this.value = value;
        }
            
        public override string Message => "Incorrect form filled";
        public override int StatusCode => (int)HttpStatusCode.UnprocessableEntity;
        public override string Code => $"EMPLOYEE/{this.processName}/VALIDATION-EXCEPTION";
        public override Dictionary<string, object> Errors => new Dictionary<string, object>
        {
            { this.key, new List<object>() { "DUPLICATE" } }
        };
    }
}