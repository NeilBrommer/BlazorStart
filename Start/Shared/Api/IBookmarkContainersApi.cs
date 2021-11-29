using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace Start.Shared.Api {
	public interface IBookmarkContainersApi {
		[Get("/")]
		Task<ApiResponse<IEnumerable<BookmarkContainerDto>>> GetAllBookmarkContainers();

		[Get("/{bookmarkContainerId}")]
		Task<ApiResponse<BookmarkContainerDto?>> GetBookmarkContainer(int bookmarkContainerId);

		[Post("/Create")]
		Task<ApiResponse<BookmarkContainerDto?>> CreateBookmarkContainer(
			[Body(BodySerializationMethod.Serialized)] string title);

		[Delete("/Delete/{bookmarkContainerId}")]
		Task<HttpResponseMessage> DeleteBookmarkContainer(int bookmarkContainerId);
	}
}
