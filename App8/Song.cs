
using System;
using System.Collections.Generic;
using System.Text;

namespace App8
{
   public class Song

    {
        /*
         *             song.setmFileName(cursor.GetString(1));// file Name
            song.setmSongName(cursor.GetString(2));// song name
            song.setmDuration(cursor.GetInt(3));// play time
            song.setmSingerName(cursor.GetString(4));// artist
            song.setmAlbumName(cursor.GetString(5));// album
            song.setSongID(cursor.GetString(10));  // SongID
            */
        public string name { get; private set; }
        public string fileName { get; private set; }
        public string singer { get; private set; }
        public double durationSecond { get; private set; }
        public string durationString { get; private set; }
        public string path { get; private set; }

        public Song(string sname,string fname,string sing,int dur)
        {
            name = sname;
            fileName = fname;
            singer = sing;
            durationSecond = dur;
        }

        public Song() { }

        public void SetName(string sname)
        {
            name = sname;
        }

        public void SetFileName(string fname)
        {
            fileName = fname;
        }

        public void SetSinger(string singname)
        {
            singer = singname;
        }

        public void SetDurationSecond(double dur)
        {
            durationSecond = dur;
        }

        public void SetDurationString(string dur)
        {
            durationString = dur;
        }

        public void SetPath(string p)
        {
            path = p;
        }
    }
}
