using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cancelorder : ContentPage
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
        public cancelorder()
        {
            InitializeComponent();
        }
        private void cancelbutton_Clicked(object sender, EventArgs e)
        {
            Orders.Clear();
            if (ordernumberlabel.Text != null)
            {
                ordernumberlabel.Text = null;
            }
            if (orderlabel1.Text != null)
            {
                orderlabel1.Text = null;
            }
            if (orderlabel2.Text != null)
            {
                orderlabel2.Text = null;
            }

            SqlConnection conn = new SqlConnection(@"Data Source=192.168.1.14,49170;Database=bookstore; User id=sa; Password=123;");
            conn.Open();
            SqlCommand cmd = new SqlCommand($"SELECT * FROM orders ", conn);
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
                    conn.Open();
                    cmd = new SqlCommand($"DELETE FROM orders WHERE ordernumber={Convert.ToInt32(ordernumbertextbox.Text)}", conn);
                    cmd.ExecuteNonQuery();


                    cmd = new SqlCommand($"UPDATE webbookstore SET stock =stock+1 WHERE bookname='{item.bookname}';", conn);
                    cmd.ExecuteNonQuery();
                    orderlabel1.Text = "ваш заказ ";
                    ordernumberlabel.Text = ordernumbertextbox.Text;
                    orderlabel2.Text = "отменен ";
                    
                    conn.Close();
                    break;
                }
                else if(item.ordernumber != Convert.ToInt32(ordernumbertextbox.Text))
                { orderlabel1.Text = "заказ "; ordernumberlabel.Text = ordernumbertextbox.Text; orderlabel2.Text = "не найден проверьте номер заказа "; }
            }
        }
    }
}
