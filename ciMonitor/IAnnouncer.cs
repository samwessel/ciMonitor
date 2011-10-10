namespace ciMonitor
{
    public interface IAnnouncer
    {
        void NewBuild();
        void FailedBuild();
        void SuccessfulBuild();
        void FixedBuild();
        void StillFailing();
        void BuildStarted();
    }
}