using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class goodspage : ContentPage
	{

		public IList<webbookstore> Webbookstore = new List<webbookstore>();
		
		public class webbookstore
		{

			public int Id { get; set; }


			public string bookname { get; set; }


			public string price { get; set; }
			public string descrip { get; set; }
			public string genre { get; set; }

			

			public int stock { get; set; }
			public DateTime releasedate { get; set; }
			public byte[] imagename { get; set; }

            public ImageSource ImageSource
            {
                get
                {
                    if (imagename == null)
                        return null;
                    return ImageSource.FromStream(() => new MemoryStream(imagename));
                }
            }


        }
		public goodspage(string bookname, string price, string descrip, string genre,string releasedate)
		{
			InitializeComponent();
			
			
			booknamelabel.Text = bookname;
			pricelabel.Text = price;
			descriplabel.Text = descrip;	
			genrelabel.Text = genre;
            releasedatelabel.Text = releasedate;
            SqlConnection conn = new SqlConnection(@"Data Source=192.168.1.14,49170;Database=bookstore; User id=sa; Password=123;");
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM webbookstore WHERE bookname='{bookname}' ", conn);
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                byte[] imagedata = reader["imagename"] as byte[];

                Webbookstore.Add(new webbookstore
                {
                    Id = Convert.ToInt32(reader["id"]),
                    bookname = reader["bookname"].ToString(),
                    price = reader["price"].ToString(),
                    descrip = reader["descrip"].ToString(),
                    genre = reader["genre"].ToString(),

                    stock = Convert.ToInt32(reader["stock"]),

                    releasedate = Convert.ToDateTime(reader["releasedate"]),

                    imagename = imagedata,
                });

            }
            reader.Close();

            foreach (var item in Webbookstore)
            {
                imagenameshow.Source = item.ImageSource;

            }

            foreach (var item in Webbookstore)
			{
				if (item.stock==0) { buybutton.IsEnabled = false; buybutton.Text = "нет в наличии"; buybutton.BackgroundColor = Color.Aquamarine; }
			}

        }
		async private void buy_Clicked(object sender, EventArgs e)
		{
			
				string bookname = booknamelabel.Text;

				string price = pricelabel.Text;
				await Navigation.PushAsync(new orderpage(bookname, price));
			





		}
	}
}
