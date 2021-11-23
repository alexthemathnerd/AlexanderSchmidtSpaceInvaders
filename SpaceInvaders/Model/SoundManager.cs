using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;

namespace SpaceInvaders.Model
{
    public class SoundManager
    {
        public void Soundtest()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");

            WindowsMediaPlayer player = new WindowsMediaPlayer();
            System.Media.SoundPlayer player = new SoundPlayer();
            simpleSound.Play();

        }
    }
}
