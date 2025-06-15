namespace Demo.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AnonymousAttribute : Attribute
    {
    }
}
