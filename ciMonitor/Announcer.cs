using System.Media;

namespace ciMonitor
{
    public class Announcer : IAnnouncer
    {
        private readonly SoundPlayer _soundPlayer;
        private const string SoundsDirectoryLocation = @"C:\sounds\";

        public Announcer()
        {
            _soundPlayer = new SoundPlayer();
        }

        public void NewBuild()
        {
            PlaySound("newBuild.wav");
        }

        public void FailedBuild()
        {
            PlaySound("failedBuild.wav");
        }

        public void SuccessfulBuild()
        {
            PlaySound("successfulBuild.wav");
        }

        public void FixedBuild()
        {
            PlaySound("fixedBuild.wav");
        }

        public void StillFailing()
        {
            PlaySound("repeatedFailure.wav");
        }

        public void BuildStarted()
        {
            PlaySound("buildStarted.wav");
        }

        private void PlaySound(string fileName)
        {
            _soundPlayer.SoundLocation = SoundsDirectoryLocation + fileName;
            _soundPlayer.Play();
        }
    }
}