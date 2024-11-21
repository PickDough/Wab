namespace Wab.Core.Domain.Exception;

public class UnauthorizedException : CoreException
{
    private readonly string _resource;
    private readonly Guid _userId;

    public UnauthorizedException(Guid userId, string resource)
    {
        _userId = userId;
        _resource = resource;
    }

    public override string ToString()
    {
        return $"User with id {_userId} is not authorized to access {_resource}";
    }
}