using Zeus.ContentProperties;
using Zeus.ContentTypes;
using Zeus.Design.Editors;
using Zeus.FileSystem;
using Zeus.Templates.ContentTypes;

namespace Zeus.AddIns.Blogs.ContentTypes
{
	[ContentType]
	public class Blog : BasePage, ISelfPopulator, IFileSystemContainer
	{
		private const string CATEGORY_CONTAINER_NAME = "categories";
		private const string FILES_NAME = "files";

		public override string IconUrl
		{
			get { return GetIconUrl(typeof(Blog), "Zeus.AddIns.Blogs.Icons.user_comment.png"); }
		}

		public CategoryContainer Categories
		{
			get { return GetChild(CATEGORY_CONTAINER_NAME) as CategoryContainer; }
		}

		public Folder Files
		{
			get { return GetChild(FILES_NAME) as Folder; }
		}

		void ISelfPopulator.Populate()
		{
			CategoryContainer categories = new CategoryContainer
			{
				Name = CATEGORY_CONTAINER_NAME,
				Title = "Categories"
			};
			categories.AddTo(this);

			Folder files = new Folder
			{
				Name = FILES_NAME,
				Title = "Files"
			};
			files.AddTo(this);
		}
	}
}