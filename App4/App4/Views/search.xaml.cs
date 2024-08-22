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
	public partial class search : ContentPage
	{
		public IList<webbookstore> Webbookstore = new List<webbookstore>();
		string searchbook;
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
		public search()
		{
			InitializeComponent();
		}
		async private void OnItemTappedsearchpage(object sender, ItemTappedEventArgs e)
		{
			webbookstore selectedbookname = e.Item as webbookstore;
			if (selectedbookname != null)
			{
				string bookname = selectedbookname.bookname.ToString();
				string price = selectedbookname.price.ToString();
				string descrip = selectedbookname.descrip.ToString();
				string genre = selectedbookname.genre.ToString();
				string releasedate = selectedbookname.releasedate.ToString();


				await Navigation.PushAsync(new goodspage(bookname, price, descrip, genre, releasedate));
			}

		}
		private void searchbutton_Clicked(object sender, EventArgs e)
		{
			Webbookstore.Clear();
			if (searchlist.ItemsSource!=null) {
				searchlist.ItemsSource = null;
			}
			
			
			searchbook = searchname.Text.Trim();

			SqlConnection conn = new SqlConnection(@"Data Source=192.168.1.14,49170;Database=bookstore; User id=sa; Password=123;");
			conn.Open();

			SqlCommand cmd = new SqlCommand($"SELECT * FROM webbookstore WHERE bookname LIKE'%{searchbook}%' ", conn);
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

			searchlist.ItemsSource = Webbookstore;

			conn.Close();

		}
	}
}