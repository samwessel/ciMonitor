namespace ciMonitor
{
    public interface IStatusFactory
    {
        Status From(string status);
    }

    public class StatusFactory : IStatusFactory
    {
        public Status From(string status)
        {
            if (status.StartsWith("broken"))
                return Status.Fail();

            switch (status)
            {
                case "stable":
                case "back to normal":
                    return Status.Success();
                case "?":
                    return Status.Building();
                default:
                    return Status.Unknown();
            }
        }
    }
}