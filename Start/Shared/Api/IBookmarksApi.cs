using System.Threading.Tasks;
using Refit;

namespace Start.Shared.Api {
	public interface IBookmarksApi {
		[Get("{bookmarkId}")]
		Task<BookmarkDto?> GetBookmark(int bookmarkId);

		[Post("/Create")]
		Task CreateBookmark(string title, string url, string? notes, int bookmarkGroupId);

		[Delete("/Delete/{bookmarkId}")]
		Task DeleteBookmark(int bookmarkId);
	}
}
