namespace Entities.Exceptions
{
    public sealed class RolesNotFoundException : NotFoundException
    {
        public RolesNotFoundException(List<string> roles) 
            : base($"The roles: [{string.Join(",", roles.ToArray())}] doesn't exists on database.")
        {
        }
    }
}
