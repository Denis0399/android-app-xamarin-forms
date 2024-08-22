
using SQLite;
using System;
using Xamarin.Forms;

namespace App4.Models
{
    public class webbookstore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }


        public string bookname { get; set; }


        public string price { get; set; }
        public string descrip { get; set; }
        public string genre { get; set; }

       

        public int stock { get; set; }

        public DateTime releasedate { get; set; }

        public byte[] imagename{ get; set; }
    }
}
