
namespace Entities.Exceptions
{
    public sealed class ResourceNotFoundException : NotFoundException
    {
        public ResourceNotFoundException(Guid Id) : base($"The resource with id: {Id} doesn't exist in the database.")
        {
        }
    }
}
