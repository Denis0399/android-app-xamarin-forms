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
	public partial class allgoods : ContentPage
	{
		List<webbookstore> Webbookstore = new List<webbookstore>();
		List<genres> Genres = new List<genres>();
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
		public class genres
		{

			public int Id { get; set; }



			public string genrename { get; set; }


		}
		public allgoods( string genrename)
		{
			InitializeComponent();
			SqlConnection conn = new SqlConnection(@"Data Source=192.168.1.14,49170;Database=bookstore; User id=sa; Password=123;");
			conn.Open();

			SqlCommand cmd = new SqlCommand($"SELECT * FROM webbookstore WHERE genre='{genrename}' ", conn);
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

            List<webbookstore> Webbookstore2 = new List<webbookstore>();
            cmd = new SqlCommand($"SELECT * FROM webbookstore WHERE genre='{genrename}' ORDER BY  releasedate  DESC  ", conn);
            cmd.ExecuteNonQuery();
            SqlDataReader reader3 = cmd.ExecuteReader();


            while (reader3.Read())
            {
                byte[] imagedata = reader3["imagename"] as byte[];
                Webbookstore2.Add(new webbookstore
                {
                    Id = Convert.ToInt32(reader3["id"]),
                    bookname = reader3["bookname"].ToString(),
                    price = reader3["price"].ToString(),
                    descrip = reader3["descrip"].ToString(),
                    genre = reader3["genre"].ToString(),

                    stock = Convert.ToInt32(reader3["stock"]),

                    releasedate = Convert.ToDateTime(reader3["releasedate"]),

                    imagename = imagedata,
                });

            }
            reader3.Close();

            cmd = new SqlCommand($"SELECT * FROM genres WHERE genrename='{genrename}'", conn);
			cmd.ExecuteNonQuery();
			SqlDataReader reader2 = cmd.ExecuteReader();


			while (reader2.Read())
			{
				Genres.Add(new genres { Id = Convert.ToInt32(reader2["id"]), genrename = reader2["genrename"].ToString() });

			}
			reader2.Close();
            Webbookstore2 = Webbookstore2.Take(5).ToList();
            allgoodspagelist.ItemsSource = Webbookstore;
			genrelist.ItemsSource = Genres;
            genrelist2.ItemsSource = Genres;
            allgodsnewlist.ItemsSource = Webbookstore2;

            conn.Close();
		}
		async private void OnItemTappedbook(object sender, ItemTappedEventArgs e)
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
	}
}