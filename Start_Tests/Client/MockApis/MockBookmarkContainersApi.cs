using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Start.Shared;
using Start.Shared.Api;

namespace Start_Tests.Client.MockApis {
	public class MockBookmarkContainersApi : IBookmarkContainersApi {
		public Task<ApiResponse<IEnumerable<BookmarkContainerDto>>> GetAllBookmarkContainers() {
			throw new NotImplementedException();
		}

		public Task<ApiResponse<BookmarkContainerDto>> GetBookmarkContainer(
			int bookmarkContainerId) {
			throw new NotImplementedException();
		}

		public Task<ApiResponse<BookmarkContainerDto>> CreateBookmarkContainer(string title,
			int sortOrder) {
			throw new NotImplementedException();
		}

		public Task<HttpResponseMessage> DeleteBookmarkContainer(int bookmarkContainerId) {
			throw new NotImplementedException();
		}
	}
}
