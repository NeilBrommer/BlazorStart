using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Start.Shared;
using Start.Shared.Api;

namespace Start_Tests.Client.MockApis {
	public class MockBookmarkGroupsApi : IBookmarkGroupsApi {
		public Task<ApiResponse<BookmarkGroupDto>> GetBookmarkGroup(int bookmarkGroupId) {
			throw new NotImplementedException();
		}

		public Task<ApiResponse<BookmarkGroupDto>> CreateBookmarkGroup(string title, string color,
			int sortOrder, int bookmarkContainerId) {
			throw new NotImplementedException();
		}

		public Task<HttpResponseMessage> DeleteBookmarkGroup(int bookmarkGroupId) {
			throw new NotImplementedException();
		}
	}
}
