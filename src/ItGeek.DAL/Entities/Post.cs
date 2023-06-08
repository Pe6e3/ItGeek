namespace ItGeek.DAL.Entities
{
	public class Post : BaseEntity
	{
		public string? Slug { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public User? CreatedBy { get; set; }
		public User? EditedBy { get; set; }
		public bool IsDeleted { get; set; } = false;

		public List<Author>? Authors { get; set; }
		public List<Category>? Categories { get; set; }
		public List<Tag>? Tags { get; set; }
		public List<Comment>? Comments { get; set; }

		public virtual PostContent? PostContent { get; set; }
		public virtual List<PostCategory>? PostCategories { get; set; }
	}
}
