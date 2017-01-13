

using Android.Database;
using Android.Media;
using Android.Provider;
using App8.Droid;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App8
{
   public class MusicPlayer
    {
        public enum Position { Top,Bot};
        public List<Song> MusicList;
        MediaPlayer musicPlayer;
        public float musicVolume { get; private set; }
        int _maxMusicVolume { get; set; }
        Song _currentSong;
        int _songId;
        bool musicChanged = false;
        Position playerPoisition;
        public MusicPlayer(float Vol, int MaxVolume,Position pos)
        {
            musicVolume = Vol / MaxVolume;
            _maxMusicVolume = MaxVolume;
            MusicList = new List<Song>();
            CreatePlayList();
            _currentSong = MusicList[0];
            playerPoisition = pos;
        }


        public void Repeat(bool r)
        {
            musicPlayer.Looping = r;
        }

        public void Play()
        {
            if (MusicList.Count > 0)
            {
                if (_currentSong == null)
                {
                    _currentSong = MusicList[0];
                    _songId = 0;
                }
            }
            else
            {
                CreatePlayList();
                if (MusicList.Count > 0)
                {
                    _currentSong = MusicList[0];
                    _songId = 0;
                }
            }

            musicPlayer = musicPlayer ?? new MediaPlayer();
            musicPlayer.Reset();
            musicPlayer.SetDataSource(_currentSong.path);
            musicPlayer.SetAudioStreamType(Stream.Music);
            musicPlayer.Prepare();
            musicPlayer.Start();
            if(playerPoisition==Position.Top)
                musicPlayer.SetVolume(musicVolume, 0);
            else
                musicPlayer.SetVolume(0,musicVolume);
            musicPlayer.Completion += delegate { NextSong(); musicChanged = true; };
        }

        public void VolumeChanged(int Vol, bool full)
        {
            musicVolume = (float)Vol / _maxMusicVolume;
            musicPlayer = musicPlayer ?? new MediaPlayer();
            if (full)
            {
                musicPlayer.SetVolume(musicVolume, musicVolume);
            }
            else
            {
                if (playerPoisition == Position.Top)
                    musicPlayer.SetVolume(musicVolume, 0);
                else
                    musicPlayer.SetVolume(0, musicVolume);
            }
        }

        public void Stop()
        {
            if (musicPlayer.IsPlaying)
            {
                musicPlayer.Stop();
            }
        }

        public int GetCurrentposition()
        {
            return musicPlayer.CurrentPosition;
        }

        public int GetDuration()
        {
            return musicPlayer.Duration;
        }

        public bool isPlaying()
        {
            return musicPlayer.IsPlaying;
        }

        public void CreatePlayList()
        {
            ICursor cursor = global::Android.App.Application.Context.ContentResolver.Query(MediaStore.Audio.Media.ExternalContentUri,
                new string[] {MediaStore.Audio.AudioColumns.AlbumId, MediaStore.Audio.AudioColumns.DisplayName,
            MediaStore.Audio.AudioColumns.Title, MediaStore.Audio.AudioColumns.Duration, MediaStore.Audio.AudioColumns.Artist,
              MediaStore.Audio.AudioColumns.Data, MediaStore.Audio.AudioColumns.Id
        }, MediaStore.Audio.AudioColumns.MimeType + "=? or " + MediaStore.Audio.AudioColumns.MimeType + "=? ",
                                            new string[] { "audio/mpeg", "audio/x-ms-wma" }, null);

            cursor.MoveToFirst();

            do
            {
                var Dur = Math.Round(cursor.GetInt(3) / 1000.0);
                var Song = new Song();
                Song.SetFileName(cursor.GetString(1));// file Name
                Song.SetName(cursor.GetString(2));// song name
                Song.SetDurationSecond(Dur);// play time
                Song.SetSinger(cursor.GetString(4));// singer
                Song.SetPath(cursor.GetString(5));
                Song.SetDurationString(Time(Dur));
                MusicList.Add(Song);
            } while (cursor.MoveToNext());

            cursor.Close();
        }

        public void NextSong()
        {
            /* var id = MusicList.FindIndex(item=>item==_currentSong);
             if (MusicList.Count-1 >= id + 1)
             {
                 _currentSong = MusicList[id + 1];

             }else
             {
                 if(MusicList.Count-1 < id + 1)
                 {
                     _currentSong = MusicList[0];
                 }
             }
             Play();
             */

            if (MusicList.Count - 1 >= _songId + 1)
            {
                _currentSong = MusicList[_songId + 1];
                _songId += 1;

            }
            else
            {
                if (MusicList.Count - 1 < _songId + 1)
                {
                    _currentSong = MusicList[0];
                    _songId = 0;
                }
            }
            Play();
        }

        public void PreviewSong()
        {
            /*var id = MusicList.FindIndex(item => item == _currentSong);
            if (id-1<0)
            {
                _currentSong = MusicList[MusicList.Count-1];

            }
            else
            {
                    _currentSong = MusicList[id-1];
            }
            Play();*/
            if (_songId - 1 < 0)
            {
                _songId = MusicList.Count - 1;
                _currentSong = MusicList[_songId];


            }
            else
            {
                _songId -= 1;
                _currentSong = MusicList[_songId];


            }
            Play();
        }

        static string Time(double stime)
        {
            TimeSpan t = TimeSpan.FromSeconds(stime);
            if (t.Hours > 0)
                return t.ToString(@"hh\:mm\:ss");
            else
                return t.ToString(@"mm\:ss");
        }

        public string[] GetInfo()
        {
            return new string[] { _currentSong.singer, _currentSong.name, _currentSong.durationSecond.ToString() };
        }

        public bool SongWasChanged()
        {
            if (musicChanged)
            {
                var t = musicChanged;
                musicChanged = false;
                return t;
            }
            return musicChanged;
        }

        public void SetSong(Song ss)
        {
            _currentSong = ss;
            musicChanged = true;
            Play();
        }
    }
}
