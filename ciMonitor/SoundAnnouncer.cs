using System;
using System.Media;

namespace ciMonitor
{
    public class SoundAnnouncer : ISoundAnnouncer
    {
        public void NewBuild()
        {
            throw new NotImplementedException();
            new SoundPlayer(@"C:\sounds\newBuild.wav").Play();
        }

        public void FailedBuild()
        {
            throw new NotImplementedException();
        }

        public void SuccessfulBuild()
        {
            throw new NotImplementedException();
        }

        public void FixedBuild()
        {
            throw new NotImplementedException();
        }

        public void StillFailing()
        {
            throw new NotImplementedException();
        }

        public void BuildStarted()
        {
            throw new NotImplementedException();
        }
    }
}