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
	public partial class orderpage : ContentPage
	{
		List<orders> Orders = new List<orders>();
		public class orders
		{

            public int Id { get; set; }
            public int ordernumber { get; set; }
            public string bookname { get; set; }
            public string price { get; set; }
            public string customername { get; set; }
            public string orderadress { get; set; }
            public string paymentmethod { get; set; }
            public string deliverymethod { get; set; }
            public string orderstatus { get; set; }

        }
		public orderpage (string bookname ,string price	)
		{
			InitializeComponent ();

			booknamelabel.Text = bookname;
			pricelabel.Text = price;
          
         makeorder.IsEnabled= false;


        }
		void ontextchanged(object sender, TextChangedEventArgs e)
		{
		   if (editorcustomername.Text == "" && editorcustomeradress.IsEnabled == false || editorcustomername.Text == " " && editorcustomeradress.IsEnabled == false)
			{
				
                editorcustomername.Text = null;

            }

			else if (editorcustomername.Text == "" && editorcustomeradress.Text == "" || editorcustomername.Text == " " && editorcustomeradress.Text == " ")
			{
                editorcustomername.Text = null; editorcustomeradress.Text = null;
            }
		}
		void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
           


            if (checkboxcash.IsChecked == true)
			{

				checkboxcard.IsEnabled = false;

			}
			else { checkboxcard.IsEnabled = true; }

			if (checkboxcard.IsChecked == true)
			{

				checkboxcash.IsEnabled = false;

			}
			else { checkboxcash.IsEnabled = true; }

			if (checkboxdelivery.IsChecked == true)
			{
                

                checkboxself.IsEnabled = false;

			}
			else { checkboxself.IsEnabled = true;  }
			if (checkboxself.IsChecked == true)
			{
                editorcustomeradress.IsEnabled = false;
                checkboxdelivery.IsEnabled = false;

			}
			else { checkboxdelivery.IsEnabled = true; editorcustomeradress.IsEnabled = true; }


			if (checkboxdelivery.IsChecked == true && checkboxcard.IsChecked == true && editorcustomername.Text != null && editorcustomeradress.Text != null)
			{

				makeorder.IsEnabled = true;
			}


			else if (checkboxself.IsChecked == true && checkboxcard.IsChecked == true && editorcustomername.Text != null && editorcustomeradress.IsEnabled == false)
			{

				makeorder.IsEnabled = true;
			}
			else if (checkboxself.IsChecked == true && checkboxcash.IsChecked == true && editorcustomername.Text != null && editorcustomeradress.IsEnabled == false) { makeorder.IsEnabled = true; }

			else if (checkboxdelivery.IsChecked == true && checkboxcash.IsChecked == true && editorcustomername.Text != null && editorcustomeradress.Text != null) { makeorder.IsEnabled = true; }
			else { makeorder.IsEnabled = false; }
        }
		async private void make_order(object sender, EventArgs e)
		{
			string orderstatus = "готовиться";

			SqlConnection conn = new SqlConnection(@"Data Source=192.168.1.14,49170;Database=bookstore; User id=sa; Password=123;");
			conn.Open();


			string bookname = booknamelabel.Text;
			string price = pricelabel.Text;


			string customeradress= editorcustomeradress.Text;
			string customername = editorcustomername.Text;
			string cash = labelcash.Text;
			string card = labelcard.Text;
			string self = labelself.Text;
			string courer = labeldelivery.Text;
			if (checkboxself.IsChecked == true)
			{ customeradress = ""; editorcustomeradress.Text = null; }

                Random r = new Random();
			int ordernumber = r.Next(10000000, 99999999);
			SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand($"SELECT * FROM orders WHERE ordernumber='{ordernumber}' ", conn);
			cmd.ExecuteNonQuery();
			SqlDataReader reader = cmd.ExecuteReader();


			while (reader.Read())
			{
				Orders.Add(new orders
				{
					Id = Convert.ToInt32(reader["id"]),
					ordernumber = Convert.ToInt32(reader["ordernumber"]),

					bookname = reader["bookname"].ToString(),
					price = reader["price"].ToString(),
					customername = reader["customername"].ToString(),
					orderadress = reader["orderadress"].ToString(),
                    paymentmethod = reader["paymentmethod"].ToString(),
                    deliverymethod = reader["deliverymethod"].ToString(),

                    orderstatus = reader["orderstatus"].ToString()
                });

			}
			reader.Close();
			foreach (var order in Orders)
			{
				if (order.ordernumber == ordernumber)
				{
					Random r2 = new Random();
					ordernumber = r2.Next(10000000, 99999999);

					

				}
		
			}
            if (checkboxdelivery.IsChecked == true  && checkboxcard.IsChecked == true )
            {
                cmd = new SqlCommand($"INSERT INTO orders VALUES({ordernumber},'{bookname}','{price}','{customername}','{customeradress}','{card}','{courer}','{orderstatus}')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"UPDATE webbookstore SET stock =stock-1 WHERE bookname='{bookname}';", conn);
                cmd.ExecuteNonQuery();
                await Navigation.PushAsync(new orderconfirmation(ordernumber, bookname, price, customername, customeradress, card, courer, orderstatus));
            }
            else if (checkboxself.IsChecked == true   && checkboxcard.IsChecked == true)
            {
               
                cmd = new SqlCommand($"INSERT INTO orders VALUES({ordernumber},'{bookname}','{price}','{customername}','{customeradress}','{card}','{self}','{orderstatus}')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"UPDATE webbookstore SET stock =stock-1 WHERE bookname='{bookname}';", conn);
                cmd.ExecuteNonQuery();
                await Navigation.PushAsync(new orderconfirmation(ordernumber, bookname, price, customername, customeradress, card, self, orderstatus));
            }
            else if (checkboxself.IsChecked == true && checkboxcash.IsChecked == true )
            {
               
                cmd = new SqlCommand($"INSERT INTO orders VALUES({ordernumber},'{bookname}','{price}','{customername}','{customeradress}','{cash}','{self}','{orderstatus}')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"UPDATE webbookstore SET stock =stock-1 WHERE bookname='{bookname}';", conn);
                cmd.ExecuteNonQuery();
                await Navigation.PushAsync(new orderconfirmation(ordernumber, bookname, price, customername, customeradress, cash, self, orderstatus));
            }
            else if (checkboxdelivery.IsChecked == true && checkboxcash.IsChecked == true)
            {
               
                cmd = new SqlCommand($"INSERT INTO orders VALUES({ordernumber},'{bookname}','{price}','{customername}','{customeradress}','{cash}','{courer}','{orderstatus}')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"UPDATE webbookstore SET stock =stock-1 WHERE bookname='{bookname}';", conn);
                cmd.ExecuteNonQuery();
                await Navigation.PushAsync(new orderconfirmation(ordernumber, bookname, price, customername, customeradress, cash, courer, orderstatus ));
            }


           
		
			
			





		}
	}
}