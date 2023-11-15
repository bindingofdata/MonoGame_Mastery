using Engine.State;

using Microsoft.Xna.Framework.Audio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Sound
{
    public sealed class SoundManager
    {
        #region SFX
        private Dictionary<Type, SoundEffect> _soundBank = new Dictionary<Type, SoundEffect>();

        public void RegisterSound(BaseGameStateEvent gameEvent, SoundEffect sound)
        {
            _soundBank.Add(gameEvent.GetType(), sound);
        }

        public void OnNotify(BaseGameStateEvent gameEvent)
        {
            if (_soundBank.TryGetValue(gameEvent.GetType(), out SoundEffect sound))
            {
                sound.Play();
            }
        }
        #endregion

        #region BGM
        private int _soundtrackIndex;
        private List<SoundEffectInstance> _musicTracks = new List<SoundEffectInstance>();

        public void SetSoundtrack(List<SoundEffectInstance> musicTracks)
        {
            _musicTracks = musicTracks;
            _soundtrackIndex = _musicTracks.Count - 1;
        }

        public void PlaySoundtrack()
        {
            if (_musicTracks.Count < 1)
            {
                return;
            }

            SoundEffectInstance currentTrack = _musicTracks[_soundtrackIndex];
            bool currentTrackStopped = currentTrack.State == SoundState.Stopped;
            if (_musicTracks.Count == 1 && currentTrackStopped)
            {
                currentTrack.Play();
                return;
            }

            int nextTrackIndex = (_soundtrackIndex + 1) % _musicTracks.Count;
            SoundEffectInstance nextTrack = _musicTracks[nextTrackIndex];
            if (currentTrackStopped)
            {
                nextTrack.Play();
                _soundtrackIndex = nextTrackIndex;
            }
        }
        #endregion
    }
}
