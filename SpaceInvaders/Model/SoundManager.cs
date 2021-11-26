using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

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

    public class SoundManager
    {
        #region Properties

        /*private static var musicFolder = Windows.ApplicationModel.Package.Current.InstalledLocation.;*/

        private Dictionary<SoundEffectsEnum, MediaElement> SoundEffects { get; }

        #endregion

        #region Constructors

        public SoundManager()
        {
            this.SoundEffects = new Dictionary<SoundEffectsEnum, MediaElement>();
            this.loadSoundEffects();
        }

        #endregion

        #region Methods

        private async Task<MediaElement> LoadSoundFile(string sound)
        {
            var soundEffect = new MediaElement
            {
                AutoPlay = false
            };
            var musicFolder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets\SoundEffects");
            var soundEffectFile = await musicFolder.GetFileAsync(sound);
            var stream = await soundEffectFile.OpenAsync(FileAccessMode.Read);
            soundEffect.SetSource(stream, soundEffectFile.ContentType);
            return soundEffect;

            /*player.Source = new Uri(@"Assets\SoundEffects\ding.wav"); */ /*MediaSource.CreateFromStorageFile(sound);*/

            /*            player.Source = MediaSource.CreateFromUri(new Uri("Assets\\SoundEffects\\ding.wav"));
                        Windows.Storage.StorageFile file;
                        file.Path = "Assets\\SoundEffects\\ding.wav";*/
        }

        private async void loadSoundEffects()
        {
            this.SoundEffects.Add(SoundEffectsEnum.PlayerFire, await this.LoadSoundFile("Player_Fire.wav"));
            this.SoundEffects.Add(SoundEffectsEnum.PlayerDestroyed, await this.LoadSoundFile("Player_Destroyed.wav"));
            this.SoundEffects.Add(SoundEffectsEnum.EnemyFire, await this.LoadSoundFile("Enemy_Fire.wav"));
            this.SoundEffects.Add(SoundEffectsEnum.EnemyDestroyed, await this.LoadSoundFile("Enemy_Destroyed.wav"));
            this.SoundEffects.Add(SoundEffectsEnum.Lose, await this.LoadSoundFile("You_Lose.wav"));
            this.SoundEffects.Add(SoundEffectsEnum.Win, await this.LoadSoundFile("You_Win.wav"));
        }

        public async Task Play(SoundEffectsEnum sound)
        {
            var soundEffect = this.SoundEffects[sound];

            soundEffect.Play();
        }

        #endregion
    }
}