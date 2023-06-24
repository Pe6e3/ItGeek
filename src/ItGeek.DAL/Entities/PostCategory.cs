namespace ItGeek.DAL.Entities
{
	public class PostCategory :BaseEntity
    {
		public int PostId { get; set; }
		public int CategoryId { get; set; }
		public virtual Post? Post { get; set; }
		public virtual Category? Category { get; set; }



	}
}
