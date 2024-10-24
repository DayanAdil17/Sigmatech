using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Sigmatech.Exceptions.Base;

namespace Sigmatech.Extensions
{
    public static class ValidationServiceCollection
    {
        public static IServiceCollection AddValidationHandler(this IServiceCollection services)
        {
            return services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    ValidationProblemDetails error = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new ValidationProblemDetails(actionContext.ModelState)).FirstOrDefault();

                    var actionName = actionContext.HttpContext.GetRouteData().Values["action"]?.ToString().ToUpper() ?? "";

                    var errors = new Dictionary<string, object>() { };
                    if (error != null)
                    {
                        foreach (var err in error.Errors)
                        {
                            errors.Add(err.Key, err.Value);
                        }
                    }
                    errors = mapErrorsDictionary(errors);

                    return new UnprocessableEntityObjectResult(ObjectError.FormatObjectError(
                        "Incorrect form validation",
                        actionName + "/VALIDATION-EXCEPTION", errors));
                };
            });
        }

        private static Dictionary<string, object> mapErrorsDictionary(IDictionary<string, object> error)
        {
            var errors = new Dictionary<string, object>() { };
            if (error != null)
            {
                foreach (var err in error)
                {
                    var mappedErr = mapError(err);
                    if (mappedErr != null)
                    {
                        if (IsDictionary(mappedErr.Value.Value))
                        {
                            return mapErrorsDictionary((IDictionary<string, object>)mappedErr.Value.Value);
                        }
                        if (errors.ContainsKey(mappedErr.Value.Key))
                        {
                            var currentDictionary = (IDictionary<string, object>)errors[mappedErr.Value.Key];
                            var addedDictionary = (IDictionary<string, object>)mappedErr.Value.Value;
                            IDictionary<string, object>
                                mappedDictionary = new Dictionary<string, object>(); 

                            foreach (var pair in currentDictionary.Concat(addedDictionary))
                            {
                                mappedDictionary[pair.Key] = pair.Value;
                            }

                            errors[mappedErr.Value.Key] = mappedDictionary;
                        }
                        else
                        {
                            errors.Add(mappedErr.Value.Key, mappedErr.Value.Value);
                        }
                    }
                }
                errors = mapListError(errors);
            }
            
            return errors;
        }
        
        private static KeyValuePair<string, object>? mapError(KeyValuePair<string, object> error)
        {
            Regex reg = new Regex("[*'\",_&#^@$]");
            var newKey = reg.Replace(error.Key, string.Empty);
            if (newKey[0].Equals("."))
            {
                newKey = newKey.Substring(1);
            }
            
            var key = newKey.Split(".",2);
            var firstKey = key[0];
            firstKey = key[0];
            if (firstKey.Length > 0)
            {
                firstKey = char.ToLower(firstKey[0]) + firstKey.Substring(1);
            }
            KeyValuePair<string, object>? result = null;
            if (key.Count() > 1)
            {
                result = mapError(new KeyValuePair<string, object>(key[1], error.Value));
                var dictionary = new Dictionary<string, object>() { };
                dictionary.Add(result.Value.Key, result.Value.Value);
                result = new KeyValuePair<string, object>(firstKey, dictionary);
            }
            else
            {
                result = new KeyValuePair<string, object>(firstKey, error.Value);
            }

            return result;
        }

        private static Dictionary<string, object> mapListError(Dictionary<string, object> error)
        {
            var errors = new Dictionary<string, object>() { };
            if (error != null)
            {
                foreach (var err in error)
                {
                    var key = err.Key;
                    if (key.Length > 0)
                    {
                        if (key.Contains("]"))
                        {
                            if (errors.ContainsKey(key.Substring(0, key.Length - 3)))
                            { 
                                var list = (IList<object>)errors[key.Substring(0, key.Length - 3)];
                                list.Add(err.Value);
                                errors[key.Substring(0, key.Length - 3)] = list;
                            }
                            else
                            {
                                errors.Add(key.Substring(0, key.Length - 3), new List<object> {err.Value});
                            }
                        }
                        else
                        {
                            errors.Add(key, err.Value);
                        }
                    }
                }
            }
            
            return errors;
        }
        
        private static bool IsDictionary(object o)
        {
            if(o == null) return false;
            return o is IDictionary &&
                o.GetType().IsGenericType &&
                o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<string,object>));
        }
    }
}