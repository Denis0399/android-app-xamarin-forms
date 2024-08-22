using SQLite;

namespace App4.Models
{
    public class genres
    {
        [PrimaryKey,AutoIncrement] 
        public int Id { get; set; }
        


        public string genrename  { get; set; }
        public genres(int id ,string Genrename) { Id = id;
			genrename=Genrename;

		}

    }
}
