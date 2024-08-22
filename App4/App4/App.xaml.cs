using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App4.Data;
using System.IO;
using App4.Views;
namespace App4
{
	public partial class App : Application
	{
		
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
