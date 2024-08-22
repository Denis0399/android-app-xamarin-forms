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
using static App4.Views.mainpage;
using static Xamarin.Essentials.Permissions;
using System.IO;

namespace App4.Views
{
	
	public partial class mainpage : ContentPage
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
		public mainpage()
		{
			InitializeComponent();

		
		}
		protected override async void OnAppearing()
		{
			
			SqlConnection conn = new SqlConnection(@"Data Source=192.168.1.14,49170;Database=bookstore; User id=sa; Password=123;");
			conn.Open();
			SqlCommand cmd = new SqlCommand($"SELECT * FROM genres", conn);
			cmd.ExecuteNonQuery();
			SqlDataReader reader = cmd.ExecuteReader();


			while (reader.Read())
			{
				Genres.Add(new genres { Id = Convert.ToInt32(reader["id"]), genrename = reader["genrename"].ToString() });

			}
			reader.Close();
			genreslist.ItemsSource = Genres;
			
			SqlCommand cmd2 = new SqlCommand($"SELECT * FROM webbookstore ORDER BY  releasedate  DESC ", conn);
			cmd2.ExecuteNonQuery();
			SqlDataReader reader2 = cmd2.ExecuteReader();

            

            while (reader2.Read())
			{
				byte[] imagedata= reader2["imagename"] as byte[];

                Webbookstore.Add(new webbookstore { Id = Convert.ToInt32(reader2["id"]), bookname = reader2["bookname"].ToString(), 
					price = reader2["price"].ToString(),
					descrip = reader2["descrip"].ToString(),
					genre = reader2["genre"].ToString(),
				
					stock = Convert.ToInt32(reader2["stock"]),
                    
                    releasedate =Convert.ToDateTime( reader2["releasedate"]),

                    imagename = imagedata,
                });

			}
			reader2.Close();
		
			Webbookstore =Webbookstore.Take(5).ToList();
			webbookstorelist.ItemsSource = Webbookstore;
			
            conn.Close();
			base.OnAppearing();
		
		}
	
	
		async private void OnItemTappedgenre(object sender, ItemTappedEventArgs e)
		{
			genres selectedgenrename = e.Item as genres;
			if (selectedgenrename != null)
			{
				string genrename = selectedgenrename.genrename.ToString();
				await Navigation.PushAsync(new allgoods(genrename));
			}

			


			
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
				

				await Navigation.PushAsync(new goodspage(bookname,price,descrip,genre, releasedate));
			}





		}

	}
}