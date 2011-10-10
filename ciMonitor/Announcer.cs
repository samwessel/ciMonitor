using System.Media;
using SpeechLib;

namespace ciMonitor
{
    public class Announcer : IAnnouncer
    {
        private readonly SoundPlayer _soundPlayer;
        private readonly SpVoice _voice;
        private const string SoundsDirectoryLocation = @"C:\sounds\";

        public Announcer()
        {
            _soundPlayer = new SoundPlayer();
            _voice = new SpVoice();
        }

        public void NewBuild()
        {
            PlaySound("newBuild.wav");
            _voice.Speak("New build detected.");
        }

        public void FailedBuild()
        {
            PlaySound("failedBuild.wav");
            _voice.Speak("Build failed.");
        }

        public void SuccessfulBuild()
        {
            PlaySound("successfulBuild.wav");
            _voice.Speak("Build succeeded.");
        }

        public void FixedBuild()
        {
            PlaySound("fixedBuild.wav");
            _voice.Speak("Build fixed.");
        }

        public void StillFailing()
        {
            PlaySound("repeatedFailure.wav");
            _voice.Speak("Build still failing.");
        }

        public void BuildStarted()
        {
            PlaySound("buildStarted.wav");
            _voice.Speak("Build started.");
        }

        private void PlaySound(string fileName)
        {
            _soundPlayer.SoundLocation = SoundsDirectoryLocation + fileName;
            _soundPlayer.Play();
        }
    }
}