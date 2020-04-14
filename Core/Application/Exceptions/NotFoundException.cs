namespace Application.Exceptions
{
    public sealed class NotFoundException : PublicException
    {
        public NotFoundException(string field, object attemptedValue)
            : base($"{field} is not found for ${attemptedValue}")
        {
        }
    }
}