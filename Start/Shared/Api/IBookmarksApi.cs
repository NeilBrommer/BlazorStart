using System.Threading.Tasks;
using Refit;
using System.Net.Http;

namespace Start.Shared.Api {
	public interface IBookmarksApi {
		[Get("/{bookmarkId}")]
		Task<ApiResponse<BookmarkDto?>> GetBookmark(int bookmarkId);

		[Post("/Create")]
		Task<ApiResponse<BookmarkDto?>> CreateBookmark(string title, string url, string? notes,
			int sortOrder, int bookmarkGroupId);

		[Delete("/Delete/{bookmarkId}")]
		Task<HttpResponseMessage> DeleteBookmark(int bookmarkId);
	}
}
