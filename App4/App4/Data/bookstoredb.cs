using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using App4.Models;
using System.Threading.Tasks;
using System.Linq;

namespace App4.Data
{
	 public class bookstoredb
	{
		readonly SQLiteConnection db;
		public bookstoredb(string connectionstring) { 
		
		  db= new SQLiteConnection(connectionstring);

			db.CreateTable<webbookstore>();
			db.CreateTable<orders>();
			db.CreateTable<genres>();

		}
		public List<webbookstore> GetWebbookstoresAsync() { return db.Table<webbookstore>().ToList(); }
		public List<orders> GetOrdersAsync() { return db.Table<orders>().ToList(); }
		//public List<genres> GetGenres() { return db.Table<genres>().ToList(); }

		public webbookstore GetWebbookstoreGoodspageAsync( string bookname) { 
			return db.Table<webbookstore>().Where(i => i.bookname == bookname).FirstOrDefault();
		}
		public List<webbookstore> GetWebbookstoregenresAsync(string genres)
		{
			return db.Table<webbookstore>().Where(i => i.genre== genres).ToList();
		}


		public List<webbookstore> GetWebbookstoreNewBooksAsync()
		{


			return db.Table<webbookstore>().OrderByDescending(i => i.Id).Take(5).ToList();

			
		}
		public int  SaveOrdersAsync(orders orders)
		{
			if (orders.Id != 0) { return db.Update(orders); }
			else { return db.Insert(orders); }
			
		}
	}
}
