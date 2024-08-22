using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class orderconfirmation : ContentPage
	{

		
		public orderconfirmation(int ordernumber,string bookname,string price,string customername,string customeradress, string paymentmethod, string deliverymethod,string orderstatus)
		{
			InitializeComponent();

			ordernumberlabel.Text = ordernumber.ToString();
			booknamelabel.Text = bookname;
				pricelabel.Text = price;
				customernamelabel.Text=customername;
            customeradresslabel.Text = customeradress;
            deliverymethodlabel.Text=deliverymethod;
            paymentmethodlabel.Text = paymentmethod;
            orderstatuslabel.Text = orderstatus;
			if (deliverymethodlabel.Text == "курьером") { customeradresslabelshow.Text = "адрес доставки"; } else { customeradresslabelshow.Text = ""; }

              
			

        }

		async private void home(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new mainpage());

		}
	}
}