using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace Start.Shared.Api {
	public interface IBookmarkGroupsApi {
		[Get("/{bookmarkGroupId}")]
		Task<ApiResponse<BookmarkGroupDto?>> GetBookmarkGroup(int bookmarkGroupId);

		[Post("/Create")]
		Task<ApiResponse<BookmarkGroupDto?>> CreateBookmarkGroup(string title, string color,
			int bookmarkContainerId);

		[Delete("/Delete/{bookmarkGroupId}")]
		Task<HttpResponseMessage> DeleteBookmarkGroup(int bookmarkGroupId);
	}
}
