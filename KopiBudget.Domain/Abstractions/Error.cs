using System.Net;

namespace KopiBudget.Domain.Abstractions
{
    public record Error(string Name, string Description, int StatusCode)
    {
        public static readonly Error None = new(string.Empty, string.Empty, ((int)HttpStatusCode.Accepted));
        public static readonly Error DifferentEntity = new("DifferentEntityException", "Entity with same name already exists", ((int)HttpStatusCode.Ambiguous));
        public static readonly Error DuplicateEntity = new("DuplicateEntityException", "Entity with same fields already exists", ((int)HttpStatusCode.Ambiguous));
        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided", ((int)HttpStatusCode.BadRequest));
        public static readonly Error Validation = new("ValidationException", "One or more values has failed", ((int)HttpStatusCode.BadRequest));
        public static readonly Error InvalidRequest = new("InvalidRequestException", "Invalid Request", ((int)HttpStatusCode.BadRequest));
        public static Error Notfound(string entity) => new($"{entity}.NotFound", entity + " not found.", ((int)HttpStatusCode.NotFound));
        public static Error Invalid(string entity, string message) => new($"{entity}.Invalid", message, ((int)HttpStatusCode.BadRequest));
        public static Error FormControl(string field, string message) => new(field, message, ((int)HttpStatusCode.BadRequest));
    }
}