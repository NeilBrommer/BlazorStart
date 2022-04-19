using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Start.Server.Models {
	/// <summary>A group of <see cref="BookmarkGroup"/>s</summary>
	public class BookmarkContainer {
		/// <summary>A unique ID for the container</summary>
		[Key]
		public int BookmarkContainerId { get; set; }
		/// <summary>A title to disply to the user</summary>
		[MaxLength(300)]
		public string Title { get; set; }
		/// <summary>Used for sorting lists of bookmark containers</summary>
		public int SortOrder { get; set; }

		/// <summary>The unique ID of the user that this container belongs to</summary>
		public string ApplicationUserId { get; set; }
		/// <summary>The user that this container belongs to</summary>
		public ApplicationUser? ApplicationUser { get; set; }

		/// <summary>The <see cref="BookmarkGroup"/>s in this container</summary>
		public List<BookmarkGroup>? BookmarkGroups { get; set; }

		public BookmarkContainer(string applicationUserId, string title, int sortOrder) {
			this.ApplicationUserId = applicationUserId;
			this.Title = title;
			this.SortOrder = sortOrder;
		}

		public BookmarkContainer(int bookmarkContainerId, string applicationUserId, string title,
			int sortOrder) : this(applicationUserId, title, sortOrder) {
			this.BookmarkContainerId = bookmarkContainerId;
		}
	}
}
