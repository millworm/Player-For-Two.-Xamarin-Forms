using Android.Database;
using Android.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App8
{
	public partial class BotPlayList : ContentPage
	{
        MusicPlayer Tp;
		public BotPlayList(MusicPlayer PL)
		{
			InitializeComponent ();
            Tp = PL;
            listView1.ItemsSource = Tp.MusicList;
            /* ICursor cursor = global::Android.App.Application.Context.ContentResolver.Query(MediaStore.Audio.Media.ExternalContentUri, new string[] {MediaStore.Audio.AudioColumns.AlbumId, MediaStore.Audio.AudioColumns.DisplayName,
             MediaStore.Audio.AudioColumns.Title, MediaStore.Audio.AudioColumns.Duration, MediaStore.Audio.AudioColumns.Artist, MediaStore.Audio.AudioColumns.Album, MediaStore.Audio.AudioColumns.Year,
             MediaStore.Audio.AudioColumns.MimeType, MediaStore.Audio.AudioColumns.Size, MediaStore.Audio.AudioColumns.Data, MediaStore.Audio.AudioColumns.Id
         }, MediaStore.Audio.AudioColumns.MimeType + "=? or " + MediaStore.Audio.AudioColumns.MimeType + "=? ",
                                                         new string[] { "audio/mpeg", "audio/x-ms-wma" }, null);
             cursor.MoveToFirst();
             do{
                var name= cursor.GetString(2);
             } while (cursor.MoveToNext()) ;*/
            /*  Label header = new Label
              {
                  Text = "ListView",
                  FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                  HorizontalOptions = LayoutOptions.Center
              };

              ListView listView = new ListView
              {
                  // Определяем источник данных
                  ItemsSource = SL,

                  // Определяем формат отображения данных
                  ItemTemplate = new DataTemplate(() =>
                  {
                      // Create views with bindings for displaying each property.
                      Label nameLabel = new Label();
                      nameLabel.SetBinding(Label.TextProperty, "_name");

                      Label singerLabel = new Label();
                      singerLabel.SetBinding(Label.TextProperty, "_singer");

                      Label durationLabel = new Label();
                      durationLabel.SetBinding(Label.TextProperty, "_durationString");
                      // Return an assembled ViewCell.
                      return new ViewCell
                      {
                          View = new Grid
                          {
                              Padding = new Thickness(0, 5),
                              Children =
                                  {
                                      new StackLayout
                                      {
                                          VerticalOptions = LayoutOptions.Center,
                                          Spacing = 0,
                                          Children =
                                          {
                                              nameLabel,
                                              singerLabel,
                                              durationLabel
                                          }
                                          }
                                  }
                          }
                      };
                  })
              };
              this.Content = new StackLayout
              {
                  Children =
                  {
                      header,
                      listView
                  }
              };*/
        }

        private void listView1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Tp.SetSong((Song)e.SelectedItem);
            Navigation.PopModalAsync();
        }
    }
}
