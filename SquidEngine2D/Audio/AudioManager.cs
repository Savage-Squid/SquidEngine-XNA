using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SquidEngine.Audio
{
    /// <summary>
    /// Manages playback of sounds and music.
    /// </summary>
    public class AudioManager : GameComponent
    {
        #region Private fields

        public AudioEngine AudioEngine;
        private Dictionary<string, WaveBank> _waveBanks = new Dictionary<string, WaveBank>();
        private Dictionary<string, SoundBank> _soundBanks = new Dictionary<string, SoundBank>();
        private Dictionary<string, AudioCategory> _categories = new Dictionary<string, AudioCategory>();
        private Dictionary<string, Cue> _cues = new Dictionary<string, Cue>();


        private List<GlobalFadeEffect> _fadeEffects = new List<GlobalFadeEffect>();
        #endregion


        public static AudioManager Instance;

        public List<GlobalFadeEffect> FadeEffects
        {
            get { return _fadeEffects; }
            set { _fadeEffects = value; }
        }

        /// <summary>
        /// Creates a new Audio Manager. Add this to the Components collection of your Game.
        /// </summary>
        /// <param name="game">The Game</param>
        public AudioManager(Game game, string xactProjectFile)
            : base(game)
        {
            AudioEngine = new AudioEngine(xactProjectFile);

            Instance = this;
        }

        /// <summary>
        /// Loads a Wave Bank into the audio manager
        /// </summary>
        /// <param name="waveBankName">the name of the wave bank</param>
        public void AddWaveBank(string waveBankName)
        {
            if(!_waveBanks.ContainsKey(waveBankName))
                _waveBanks.Add(waveBankName, new WaveBank(AudioEngine, "Content/SquidEngineAssets/Audio/" + waveBankName + ".xwb"));
        }

        /// <summary>
        /// Loads a Sound Bank into the audio manager
        /// </summary>
        /// <param name="waveBankName">the name of the sound bank</param>
        public void AddSoundBank(string soundBankName)
        {
            if(!_soundBanks.ContainsKey(soundBankName))
                _soundBanks.Add(soundBankName, new SoundBank(AudioEngine, "Content/SquidEngineAssets/Audio/" + soundBankName + ".xsb"));
        }

        public AudioCategory GetCategory(string categoryName)
        {
            return AudioEngine.GetCategory(categoryName);
        }


        public void PlayCue(string soundBankName, string cueName)
        {
            if (_cues.ContainsKey(cueName))
            {
                _cues.Remove(cueName);
                _cues.Add(cueName, _soundBanks[soundBankName].GetCue(cueName));
                _cues[cueName].Play();

            }
            else
            {
                if (_soundBanks.ContainsKey(soundBankName) && _soundBanks[soundBankName].GetCue(cueName) != null)
                {
                    _cues.Add(cueName, _soundBanks[soundBankName].GetCue(cueName));
                    _cues[cueName].Play();
                }
            }
        }

        public void StopCue(string cueName)
        {
            if (_cues.ContainsKey(cueName))
            {
                if (_cues[cueName].IsPlaying)
                    _cues[cueName].Stop(AudioStopOptions.AsAuthored);
                _cues.Remove(cueName);
            }
        }

        public bool IsCuePlaying(string cuename)
        {
            if (_cues.ContainsKey(cuename))
            {
                if (_cues[cuename].IsPlaying || _cues[cuename].IsPreparing)
                    return true;
            }
            return false;
        }

        public void StopAllCues()
        {
            foreach (KeyValuePair<string, Cue> cue in _cues)
            {
                if (cue.Value.IsPlaying)
                {
                    cue.Value.Stop(AudioStopOptions.AsAuthored);
                }
            }

            _cues.Clear();
        }



        public void AddGlobalSettingFadeEffect(float from, float to, TimeSpan duration, string globalSettingName)
        {
            _fadeEffects.Add(new GlobalFadeEffect(from, to, duration, globalSettingName));
        }


        /// <summary>
        /// Called per loop unless Enabled is set to false.
        /// </summary>
        /// <param name="gameTime">Time elapsed since last frame</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _fadeEffects.Count; ++i)
            {
                if (_fadeEffects[i].Complete)
                {
                    _fadeEffects.Remove(_fadeEffects[i]);
                }
                else
                {
                    _fadeEffects[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }

            AudioEngine.Update();

            base.Update(gameTime);
        }

        // Pauses all music and sound if disabled, resumes if enabled.
        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            base.OnEnabledChanged(sender, args);
        }


        #region GlobalFadeEffect
        public class GlobalFadeEffect
        {
            public float Source;
            public float Target;
            public string SettingName;
            public bool Complete;

            private float _time;
            private float _duration;

            public GlobalFadeEffect(float source, float target, TimeSpan duration, string settingName)
            {
                Source = source;
                Target = target;

                Complete = false;
                _time = 0f;
                SettingName = settingName;
                _duration = (float)duration.TotalSeconds;
            }

            public void Update(float time)
            {
                _time += time;

                if (_time >= _duration)
                {
                    _time = _duration;
                    Complete = true;
                }

                AudioManager.Instance.AudioEngine.SetGlobalVariable(SettingName, GetCurrentValue());
            }

            public float GetCurrentValue()
            {
                return MathHelper.Lerp(Source, Target, _time / _duration);
            }
        }
        #endregion
    }

    /// <summary>
    /// Overrides the XNA Media Player class and adds additional features
    /// </summary>
    public static class SquidEngineMediaPlayer
    {
        static float _volumeTransitionStartVolume = 0;
        static float _volumeTransitionEndVolume = 0;
        static float _volumeTransitionCurrent = 0;
        static float _volumeTransitionEnd = 0;
        static bool _volumeIsTransitioning = false;
        public static List<Song> SongList = new List<Song>();
        static bool _playingSongList = false;
        static bool _repeatSongList = false;
        static int _songListIndex = 0;
        
        public static float Volume
        {
            get { return MediaPlayer.Volume; }
            set { MediaPlayer.Volume = value; }
        }

        public static MediaState State
        {
            get { return MediaPlayer.State; }
        }

        public static void Play(Song song)
        {
            _playingSongList = false;
            MediaPlayer.Play(song);
        }

        public static void PlaySongList(bool repeat)
        {
            _repeatSongList = repeat;
            _songListIndex = 0;
            if (SongList.Count > _songListIndex)
            {
                MediaPlayer.Play(SongList[_songListIndex]);
                _playingSongList = true;
            }
        }

        public static void Stop()
        {
            _playingSongList = false;
            MediaPlayer.Stop();
        }

        public static void Pause()
        {
            MediaPlayer.Pause();
        }

        public static void FadeVolume(float from, float to, float time)
        {
            _volumeTransitionCurrent = 0;
            _volumeTransitionEnd = time;
            _volumeTransitionStartVolume = from;
            _volumeTransitionEndVolume = to;
            _volumeIsTransitioning = true;
        }

        public static void Update(GameTime gameTime)
        {
            if (_volumeIsTransitioning)
            {
                if (_volumeTransitionCurrent <= _volumeTransitionEnd)
                {
                    Volume = MathHelper.Lerp(0f, 1f, _volumeTransitionCurrent / _volumeTransitionEnd);
                    _volumeTransitionCurrent += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    Volume = _volumeTransitionEnd;
                    _volumeIsTransitioning = false;
                }
            }

            if (_playingSongList)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    _songListIndex++;
                    if (SongList.Count > _songListIndex)
                    {
                        MediaPlayer.Play(SongList[_songListIndex]);
                    }
                    else
                    {
                        if (_repeatSongList)
                        {
                            _songListIndex = 0;
                            MediaPlayer.Play(SongList[_songListIndex]);
                        }
                        else
                            _playingSongList = false;
                    }
                }
            }
        }
    }
}
