namespace ciMonitor
{
    public interface IStatusFactory
    {
        Status From(string status);
    }

    public class JenkinsStatusFactory : IStatusFactory
    {
        public Status From(string status)
        {
            if (status.StartsWith("broken") || status == "aborted")
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