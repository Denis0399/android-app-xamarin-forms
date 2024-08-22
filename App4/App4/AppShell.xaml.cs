using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App4.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4
{
	
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(mainpage), typeof(mainpage));
			Routing.RegisterRoute(nameof(checkorder), typeof(checkorder));
			Routing.RegisterRoute(nameof(allgoods), typeof(allgoods));
			Routing.RegisterRoute(nameof(goodspage), typeof(goodspage));
			Routing.RegisterRoute(nameof(orderpage), typeof(orderpage));
			Routing.RegisterRoute(nameof(orderconfirmation), typeof(orderconfirmation));
			Routing.RegisterRoute(nameof(search), typeof(search));
            Routing.RegisterRoute(nameof(cancelorder), typeof(cancelorder));
        }
	}
}