using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Start.Shared;
using Start.Shared.Api;

namespace Start_Tests.Client.MockApis {
	public class MockBookmarksApi : IBookmarksApi {
		public Task<ApiResponse<BookmarkDto>> GetBookmark(int bookmarkId) {
			throw new NotImplementedException();
		}

		public Task<ApiResponse<BookmarkDto>> CreateBookmark(string title, string url,
			string notes, int sortOrder, int bookmarkGroupId) {
			throw new NotImplementedException();
		}

		public Task<HttpResponseMessage> DeleteBookmark(int bookmarkId) {
			throw new NotImplementedException();
		}
	}
}
