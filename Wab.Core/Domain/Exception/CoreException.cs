namespace Wab.Core.Domain.Exception;

public class CoreException : System.Exception
{
    public required CoreExceptionDetail Detail { get; init; }

    public override string ToString()
    {
        return Detail.ToString();
    }
}

public abstract record CoreExceptionDetail
{
    public abstract override string ToString();

    public record NotFound : CoreExceptionDetail
    {
        private readonly Guid _id;
        private readonly string _type;

        public NotFound(Guid id, string type)
        {
            _id = id;
            _type = type;
        }

        public override string ToString()
        {
            return $"{_type} with id {_id} not found";
        }
    }

    public record Unauthorized : CoreExceptionDetail
    {
        private readonly string _resource;
        private readonly Guid _userId;

        public Unauthorized(Guid userId, string resource)
        {
            _userId = userId;
            _resource = resource;
        }

        public override string ToString()
        {
            return $"User with id {_userId} is not authorized to access {_resource}";
        }
    }
}