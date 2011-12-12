namespace ciMonitor
{
    public class Transitions
    {
        public const string NewBuild = "NewBuild";
        public const string BuildStarted = "BuildStarted";
        public const string SuccessfulBuild = "SuccessfulBuild";
        public const string FailedBuild = "FailedBuild";
        public const string FixedBuild = "FixedBuild";
        public const string RepeatedlyFailingBuild = "RepeatedlyFailingBuild";
    }
}