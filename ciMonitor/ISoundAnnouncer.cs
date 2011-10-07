namespace ciMonitor
{
    public interface ISoundAnnouncer
    {
        void NewBuild();
        void FailedBuild();
        void SuccessfulBuild();
        void FixedBuild();
        void StillFailing();
        void BuildStarted();
    }
}