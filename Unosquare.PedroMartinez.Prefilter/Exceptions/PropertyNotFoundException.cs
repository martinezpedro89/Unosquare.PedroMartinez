namespace Unosquare.PedroMartinez.Prefilter.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException() : base("Property not found.")
        {
        }

        public PropertyNotFoundException(string message) : base(message)
        {
        }

        public PropertyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
