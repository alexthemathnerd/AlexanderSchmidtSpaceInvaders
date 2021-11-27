using System;
using System.Collections.Generic;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace SpaceInvaders.Model
{
    public enum SoundEffectsEnum
    {
        PlayerFire,
        PlayerDestroyed,
        EnemyFire,
        EnemyDestroyed,
        Win,
        Lose
    }

    /// <summary>Plays GameSounds.</summary>
    public class SoundManager
    {
        #region Properties

        private static Dictionary<SoundEffectsEnum, MediaPlayer> SoundEffects { get; } =
            new Dictionary<SoundEffectsEnum, MediaPlayer>
            {
                {
                    SoundEffectsEnum.PlayerFire,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SoundEffects/Player_Fire.wav")),
                        AutoPlay = false
                    }
                },
                {
                    SoundEffectsEnum.PlayerDestroyed,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(
                            new Uri("ms-appx:///Assets/SoundEffects/Player_Destroyed.wav")),
                        AutoPlay = false
                    }
                },
                {
                    SoundEffectsEnum.EnemyFire,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SoundEffects/Enemy_Fire.wav")),
                        AutoPlay = false
                    }
                },
                {
                    SoundEffectsEnum.EnemyDestroyed,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(
                            new Uri("ms-appx:///Assets/SoundEffects/Enemy_Destroyed.wav")),
                        AutoPlay = false
                    }
                },
                {
                    SoundEffectsEnum.Lose,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SoundEffects/You_Lose.wav")),
                        AutoPlay = false
                    }
                },
                {
                    SoundEffectsEnum.Win,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SoundEffects/You_Win.wav")),
                        AutoPlay = false
                    }
                }
            };

        #endregion

        #region Methods

        /// <summary>Plays the specified sound.</summary>
        /// <param name="sound">The sound.</param>
        public static void Play(SoundEffectsEnum sound)
        {
            var soundEffect = SoundEffects[sound];

            soundEffect.Play();
        }

        #endregion
    }
}