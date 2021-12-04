using System;
using System.Collections.Generic;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// The List of Sound Effects
    /// </summary>
    public enum SoundEffectsEnum
    {
        PlayerFire,
        PlayerDestroyed,
        EnemyFire,
        EnemyDestroyed,
        SpecialShip,
        Win,
        Lose
    }

    /// <summary>
    /// Manages the Sounds
    /// </summary>
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
                        AutoPlay = false,
                        Volume = .5
                    }
                },
                {
                    SoundEffectsEnum.PlayerDestroyed,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(
                            new Uri("ms-appx:///Assets/SoundEffects/Player_Destroyed.wav")),
                        AutoPlay = false,
                        Volume = .5
                    }
                },
                {
                    SoundEffectsEnum.EnemyFire,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SoundEffects/Enemy_Fire.wav")),
                        AutoPlay = false,
                        Volume = .5
                    }
                },
                {
                    SoundEffectsEnum.EnemyDestroyed,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(
                            new Uri("ms-appx:///Assets/SoundEffects/Enemy_Destroyed.wav")),
                        AutoPlay = false,
                        Volume = .5
                    }
                },
                {
                   SoundEffectsEnum.SpecialShip,
                   new MediaPlayer
                   {
                       Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SoundEffects/SpecialShip.wav")),
                       AutoPlay = false,
                       Volume = .5
                   }
                },
                {
                    SoundEffectsEnum.Lose,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SoundEffects/You_Lose.wav")),
                        AutoPlay = false,
                        Volume = .5
                    }
                },
                {
                    SoundEffectsEnum.Win,
                    new MediaPlayer
                    {
                        Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/SoundEffects/You_Win.wav")),
                        AutoPlay = false,
                        Volume = .5
                    }
                }
            };

        #endregion

        #region Methods

        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public static void Play(SoundEffectsEnum sound)
        {
            var soundEffect = SoundEffects[sound];

            soundEffect.PlaybackSession.Position = TimeSpan.MinValue;
            soundEffect.Play();
        }

        /// <summary>
        /// Stop the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public static void Stop(SoundEffectsEnum sound)
        {
            var soundEffect = SoundEffects[sound];
           
            soundEffect.Pause();
        }
        #endregion
    }
}