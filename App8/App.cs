using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App8
{
	public class App : Application
	{
        public static NavigationPage MyNavigationPage;

        public App ()
		{
            // global::Xamarin.Forms.Forms.SetTitleBarVisibility(Xamarin.Forms.AndroidTitleBarVisibility.Never);
             // The root page of your application
           /*  MyNavigationPage = new NavigationPage();

             MainPage = MyNavigationPage;
             MyNavigationPage.PushAsync(new Page1(), true);*/
            MainPage = new Page1();

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
