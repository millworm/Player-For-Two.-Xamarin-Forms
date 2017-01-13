

using Android.Media;
using App8.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace App8
{
    public partial class Page1 : ContentPage
    {
        MusicPlayer TopMusicPlayer,BotMusicPlayer;
        Timer timer;

        public Page1()
        {
            InitializeComponent();

            AudioManager audio = (AudioManager)global::Android.App.Application.Context.GetSystemService(Android.Content.Context.AudioService);
            int currentVolume = audio.GetStreamVolume(Stream.Music);
            audio.Dispose();

            TopMusicPlayer = new MusicPlayer((int)currentVolume, (int)TopVolumeSlider.Maximum,MusicPlayer.Position.Top);
            TopVolumeSlider.Value = currentVolume;

            BotMusicPlayer = new MusicPlayer((int)currentVolume, (int)TopVolumeSlider.Maximum, MusicPlayer.Position.Top);
            BotVolumeSlider.Value = currentVolume;

            timer = new Timer(1000);
            timer.Elapsed += delegate
            {

                if (TopMusicPlayer.isPlaying())
                {

                    var pos = Math.Round(TopMusicPlayer.GetCurrentposition() / 1000.0);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TopTextCurrentTime.Text = Time(pos);
                    });

                    TopMusicSlider.Value = pos;
                }
                if (TopMusicPlayer.SongWasChanged())
                {
                    SetTopMusicInfo(TopMusicPlayer.GetInfo());
                }

                if (BotMusicPlayer.isPlaying())
                {

                    var pos = Math.Round(BotMusicPlayer.GetCurrentposition() / 1000.0);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        BotTextCurrentTime.Text = Time(pos);
                    });

                    BotMusicSlider.Value = pos;
                }
                if (BotMusicPlayer.SongWasChanged())
                {
                    SetTopMusicInfo(BotMusicPlayer.GetInfo());
                }

            };

            TopVolumeSlider.ValueChanged += TopMusicSlider_ValueChanged;
            BotVolumeSlider.ValueChanged += BotMusicSlider_ValueChanged;
            timer.Start();

            SetTopMusicInfo(TopMusicPlayer.GetInfo());
            SetBotMusicInfo(BotMusicPlayer.GetInfo());
        }


        void OnTapTopPlayList(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new TopPlayList(TopMusicPlayer), true);
            if (TopMusicPlayer.MusicList.Count == 0)
                TopMusicPlayer.CreatePlayList();
        }

        void OnTapTopShuffle(object sender, EventArgs args)
        {
            // Do something
            DisplayAlert("Alert", "OnTapShuffle", "OK");
        }

        void OnTapTopRepeat(object sender, EventArgs args)
        {

        }

        void OnTapTopResize(object sender, EventArgs args)
        {

            Navigation.PushModalAsync(new TopPlayerView(), true);
        }

        void OnTapTopStop(object sender, EventArgs args)
        {
            TopMusicPlayer.Stop();
            TopPlayButton.IsVisible = true;
            TopStopButton.IsVisible = false;
        }

        void OnTapTopPlay(object sender, EventArgs args)
        {

            /* Tp.Play("/sdcard/Music/sample.mp3");


             MediaMetadataRetriever reader = new MediaMetadataRetriever();
             reader.SetDataSource("/sdcard/Music/sample.mp3");
             String title = reader.ExtractMetadata(MetadataKey.Title);
             String artist = reader.ExtractMetadata(MetadataKey.Artist);
             String AllTime = reader.ExtractMetadata(MetadataKey.Duration);
             var _secondFulltime = Math.Round(Convert.ToInt32(AllTime) / 1000.0); */
            //http://stackoverflow.com/questions/19309565/reading-id3-tags-in-xamarin-android
            TopMusicPlayer.Play();
            SetTopMusicInfo(TopMusicPlayer.GetInfo());
            /*  var info = Tp.GetInfo();
              var fulltime = Convert.ToDouble(info[2]);
              TopTextArtist.Text = info[0];
              TopTextSong.Text = info[1];
              TopTextFullTime.Text= Time(fulltime);
              TopMusicSlider.Maximum = fulltime;
              */
            TopPlayButton.IsVisible = false;
            TopStopButton.IsVisible = true;

        }

        private void TopMusicSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var vol = Math.Round(e.NewValue);
            TopMusicPlayer.VolumeChanged((int)vol, false);
            TopVolumeSlider.Value = vol;
        }

        void OnTapTopMute(object sender, EventArgs args)
        {
            TopMusicSlider_ValueChanged(sender, new ValueChangedEventArgs(0, 0));
        }

        void OnTapTopHigh(object sender, EventArgs args)
        {
            TopMusicSlider_ValueChanged(sender, new ValueChangedEventArgs(0, TopMusicSlider.Maximum));
        }

        string Time(double stime)
        {
            TimeSpan t = TimeSpan.FromSeconds(stime);
            if (t.Hours > 0)
                return t.ToString(@"hh\:mm\:ss");
            else
                return t.ToString(@"mm\:ss");
        }

        void OnTapTopNextSong(object sender, EventArgs args)
        {
            TopMusicPlayer.NextSong();
            SetTopMusicInfo(TopMusicPlayer.GetInfo());
        }
        void OnTapTopPreviewSong(object sender, EventArgs args)
        {
            TopMusicPlayer.PreviewSong();
            SetTopMusicInfo(TopMusicPlayer.GetInfo());
        }

        void SetTopMusicInfo(string[] info)
        {
            var fulltime = Convert.ToDouble(info[2]);
            TopTextArtist.Text = info[0];
            TopTextSong.Text = info[1];
            TopTextFullTime.Text = Time(fulltime);
            TopMusicSlider.Maximum = fulltime;
        }


        ////////////////////////////////////////////
        ////////////////////////////////////////////
        ///////////////////////////////////////////
        void OnTapBotPlayList(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new BotPlayList(BotMusicPlayer), true);
            if (BotMusicPlayer.MusicList.Count == 0)
                BotMusicPlayer.CreatePlayList();
        }

        void OnTapBotShuffle(object sender, EventArgs args)
        {
            // Do something
            DisplayAlert("Alert", "OnTapShuffle", "OK");
        }

        void OnTapBotRepeat(object sender, EventArgs args)
        {

        }

        void OnTapBotResize(object sender, EventArgs args)
        {

            Navigation.PushModalAsync(new BotPlayerView(), true);
        }

        void OnTapBotStop(object sender, EventArgs args)
        {
            BotMusicPlayer.Stop();
            BotPlayButton.IsVisible = true;
            BotStopButton.IsVisible = false;
        }

        void OnTapBotPlay(object sender, EventArgs args)
        {

            /* Tp.Play("/sdcard/Music/sample.mp3");


             MediaMetadataRetriever reader = new MediaMetadataRetriever();
             reader.SetDataSource("/sdcard/Music/sample.mp3");
             String title = reader.ExtractMetadata(MetadataKey.Title);
             String artist = reader.ExtractMetadata(MetadataKey.Artist);
             String AllTime = reader.ExtractMetadata(MetadataKey.Duration);
             var _secondFulltime = Math.Round(Convert.ToInt32(AllTime) / 1000.0); */
            //http://stackoverflow.com/questions/19309565/reading-id3-tags-in-xamarin-android
            BotMusicPlayer.Play();
            SetBotMusicInfo(BotMusicPlayer.GetInfo());
            /*  var info = Tp.GetInfo();
              var fulltime = Convert.ToDouble(info[2]);
              BotTextArtist.Text = info[0];
              BotTextSong.Text = info[1];
              BotTextFullTime.Text= Time(fulltime);
              BotMusicSlider.Maximum = fulltime;
              */
            BotPlayButton.IsVisible = false;
            BotStopButton.IsVisible = true;

        }

        private void BotMusicSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var vol = Math.Round(e.NewValue);
            BotMusicPlayer.VolumeChanged((int)vol, false);
            BotVolumeSlider.Value = vol;
        }

        void OnTapBotMute(object sender, EventArgs args)
        {
            BotMusicSlider_ValueChanged(sender, new ValueChangedEventArgs(0, 0));
        }

        void OnTapBotHigh(object sender, EventArgs args)
        {
            BotMusicSlider_ValueChanged(sender, new ValueChangedEventArgs(0, BotMusicSlider.Maximum));
        }

        void OnTapBotNextSong(object sender, EventArgs args)
        {
            TopMusicPlayer.NextSong();
            SetBotMusicInfo(BotMusicPlayer.GetInfo());
        }
        void OnTapBotPreviewSong(object sender, EventArgs args)
        {
            BotMusicPlayer.PreviewSong();
            SetBotMusicInfo(BotMusicPlayer.GetInfo());
        }

        void SetBotMusicInfo(string[] info)
        {
            var fulltime = Convert.ToDouble(info[2]);
            BotTextArtist.Text = info[0];
            BotTextSong.Text = info[1];
            BotTextFullTime.Text = Time(fulltime);
            BotMusicSlider.Maximum = fulltime;
        }
    }
}
