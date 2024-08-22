using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App4.Models;
using SQLite;
using static App4.Views.checkorder;

namespace App4.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class checkorder : ContentPage
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
		public checkorder()
		{
			InitializeComponent();
		}
		private void ourderbutton_Clicked(object sender, EventArgs e)
		{
			Orders.Clear();
            if (ifwrongnumberlabel.Text != null)
            {
                ifwrongnumberlabel.Text = null;
            }
            if (ordernumberlabel.Text != null)
			{
                ordernumberlabel.Text = null;
			}
            if (ordernumberlabelshow.Text != null)
            {
                ordernumberlabelshow.Text = null;
            }
            if (booknamelabel.Text != null)
            {
                booknamelabel.Text = null;
            }
            if (booknamelabelshow.Text != null)
            {
                booknamelabelshow.Text = null;
            }
            if (pricelabel.Text != null)
            {
                pricelabel.Text = null;
            }
            if (pricelabelshow.Text != null)
            {
                pricelabelshow.Text = null;
            }
            if (customernamelabel.Text != null)
            {
                customernamelabel.Text = null;
            }
            if (customernamelabelshow.Text != null)
			{
                customernamelabelshow.Text = null;
			}
            if (customeradresslabel.Text != null)
            {
                customeradresslabel.Text = null;
            }
            if (customeradresslabelshow.Text != null)
            {
                customeradresslabelshow.Text = null;
            }
            if (deliverymethodlabel.Text != null)
            {
                deliverymethodlabel.Text = null;

            }
            if (deliverymethodlabelshow.Text != null)
            {
                deliverymethodlabelshow.Text = null;

            }
            if (paymentmethodlabel.Text != null)
            {
                paymentmethodlabel.Text = null;

            }
            if (paymentmethodlabelshow.Text != null)
            {
                paymentmethodlabelshow.Text = null;

            }
            if (orderstatuslabel.Text != null)
            {
                orderstatuslabel.Text = null;

            }
            if (orderstatuslabelshow.Text != null)
            {
                orderstatuslabelshow.Text = null;

            }


            SqlConnection conn = new SqlConnection(@"Data Source=192.168.1.14,49170;Database=bookstore; User id=sa; Password=123;");
			conn.Open();
			SqlCommand cmd = new SqlCommand($"SELECT * FROM orders  ", conn);
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
			conn.Close();
			foreach (var item in Orders)
			{
                if (item.ordernumber == Convert.ToInt32(ordernumbertextbox.Text))
                {
                    ifwrongnumberlabel.Text = null;


                    ordernumberlabel.Text = "номер заказа";
                    ordernumberlabelshow.Text = item.ordernumber.ToString();


                    booknamelabel.Text = "название книги";
                    booknamelabelshow.Text = item.bookname;


                    pricelabel.Text = "цена товара";
                    pricelabelshow.Text = item.price;


                    customernamelabel.Text = "имя заказчика";
                    customernamelabelshow.Text = item.customername;

                    if (item.deliverymethod == "курьером")
                    {
                        customeradresslabel.Text = "адрес доставки";
                        customeradresslabelshow.Text = item.orderadress;
                    }
                    else
                    {
                        customeradresslabel.Text = "";
                        customeradresslabelshow.Text = item.orderadress;
                    }

                    deliverymethodlabel.Text = "способ ролучения";
                    deliverymethodlabelshow.Text = item.deliverymethod;

                    paymentmethodlabel.Text = "способ оплаты";
                    paymentmethodlabelshow.Text = item.paymentmethod;
                    orderstatuslabel.Text = "стату заказа";
                    orderstatuslabelshow.Text = item.orderstatus;
                    break;
                }

                else if (item.ordernumber!= Convert.ToInt32(ordernumbertextbox.Text)) { ifwrongnumberlabel.Text = "убедитесь что вы правильно ввели номер заказа"; } 
            }




        }
	}
}