namespace Wab.Core.Domain.Exception;

public class NotFoundException : CoreException
{
    private readonly Guid _id;
    private readonly string _type;

    public NotFoundException(Guid id, string type)
    {
        _id = id;
        _type = type;
    }

    public override string ToString()
    {
        return $"{_type} with id {_id} not found";
    }
}