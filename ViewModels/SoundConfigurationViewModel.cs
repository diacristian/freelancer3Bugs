using System;
using System.IO;
using System.Reflection;
using System.Windows.Threading;

namespace FL1.ViewModels
{
    public class SoundConfigurationViewModel // : Screen
    {
        private DispatcherTimer _timer;
        private int _SpeakerVolume;
        private int _MicVolume;
        private int _FrontLeftSpeakerVolume;
        private int _FrontRightSpeakerVolume;
        private int _MicrophoneLevel;
        private int _SpeakerLevel;
        private int _FrontRightSpeakerLevel;
        private int _FrontLeftSpeakerLevel;

        /// <summary>
        ///
        /// </summary>
        /// <param name="container"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="omniSystem"></param>
        public SoundConfigurationViewModel()
        {

        }

        /// <summary>
        ///
        /// </summary>
        protected void OnActivate()
        {
            // create timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer;
            _timer.Start();
        }

        /// <summary>
        ///
        /// </summary>
        public int MasterVolume
        {
            get
            {
                return _SpeakerVolume;
            }
            set
            {
                if (value != _SpeakerVolume)
                {
                    _SpeakerVolume = value;
                    //NotifyOfPropertyChange(() => MasterVolume);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int MicrophoneVolume
        {
            get
            {
                return _MicVolume;
            }
            set
            {
                if (value != _MicVolume)
                {
                    _MicVolume = value;
                   // NotifyOfPropertyChange(() => MicrophoneVolume);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int SelectedChannelVolume
        {
            get
            {
                return _FrontLeftSpeakerVolume;
            }
            set
            {
                if (value != (int)_FrontLeftSpeakerVolume)
                {
                    _FrontLeftSpeakerVolume = value;
                    //NotifyOfPropertyChange(() => SelectedChannelVolume);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int UnSelectedChannelVolume
        {
            get
            {
                return _FrontRightSpeakerVolume;
            }
            set
            {
                if (value != (int)_FrontRightSpeakerVolume)
                {
                    _FrontRightSpeakerVolume = value;
                    //NotifyOfPropertyChange(() => UnSelectedChannelVolume);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int MicrophoneActiveSoundLevel
        {
            get { return _MicrophoneLevel; }
        }

        /// <summary>
        ///
        /// </summary>
        public int MasterActiveSoundLevel
        {
            get { return _SpeakerLevel;  }
        }

        /// <summary>
        ///
        /// </summary>
        public int SelectedActiveSoundLevel
        {
            get { return _FrontLeftSpeakerLevel; }
 
        }

        /// <summary>
        ///
        /// </summary>
        public int UnSelectedActiveSoundLevel
        {
            get { return _FrontRightSpeakerLevel; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer(object sender, EventArgs e)
        {
            try
            {
                //NotifyOfPropertyChange(() => MasterActiveSoundLevel);
                //NotifyOfPropertyChange(() => SelectedActiveSoundLevel);
                //NotifyOfPropertyChange(() => UnSelectedActiveSoundLevel);
                //NotifyOfPropertyChange(() => MicrophoneActiveSoundLevel);
            }
            catch (Exception)
            {
            }
        }
    }
}