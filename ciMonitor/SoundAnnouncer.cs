using System.Media;

namespace ciMonitor
{
    public class SoundAnnouncer : ISoundAnnouncer
    {
        private readonly SoundPlayer _soundPlayer;

        public SoundAnnouncer()
        {
            _soundPlayer = new SoundPlayer();
            _soundPlayer.SoundLocation = @"C:\sounds\newBuild.wav";
            _soundPlayer.LoadAsync();
        }

        public void NewBuild()
        {
            _soundPlayer.Play();
        }

        public void FailedBuild()
        {
            _soundPlayer.Play();
        }

        public void SuccessfulBuild()
        {
            _soundPlayer.Play();
        }

        public void FixedBuild()
        {
            _soundPlayer.Play();
        }

        public void StillFailing()
        {
            _soundPlayer.Play();
        }

        public void BuildStarted()
        {
            _soundPlayer.Play();
        }
    }
}