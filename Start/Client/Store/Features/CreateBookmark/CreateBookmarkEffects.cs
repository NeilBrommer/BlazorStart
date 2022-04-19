using System.Threading.Tasks;
using System.Linq;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Start.Shared.Api;
using Start.Client.Store.Features.CurrentContainer;

namespace Start.Client.Store.Features.CreateBookmark {
	public class CreateBookmarkEffects {
		public IBookmarksApi BookmarksApi { get; init; }
		public IBookmarkGroupsApi BookmarkGroupsApi { get; init; }

		public CreateBookmarkEffects(IBookmarksApi bookmarksApi,
			IBookmarkGroupsApi bookmarkGroupsApi) {
			this.BookmarksApi = bookmarksApi;
			this.BookmarkGroupsApi = bookmarkGroupsApi;
		}

		[EffectMethod]
		public async Task SubmitCreateBookmark(SubmitCreateBookmarkAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchCreateBookmarkAction());

			try {
				Refit.ApiResponse<Start.Shared.BookmarkGroupDto?>? groupResponse = await this
					.BookmarkGroupsApi
					.GetBookmarkGroup(action.NewBookmark.BookmarkGroupId);

				if (groupResponse == null || groupResponse.Content == null) {
					dispatch.Dispatch(new ErrorFetchingCreateBookmarkAction(
						"There was an error checking the bookmark group"));
					return;
				}

				// Set the sort order to highest in the group + 1
				// .Max throws an exception if Bookmarks is empty
				int sortOrder = !(groupResponse.Content.Bookmarks?.Any() ?? false)
					? 0
					: groupResponse.Content.Bookmarks.Max(b => b.SortOrder) + 1;

				Refit.ApiResponse<Start.Shared.BookmarkDto?>? apiResponse = await this.BookmarksApi
					.CreateBookmark(action.NewBookmark.Title, action.NewBookmark.Url,
						action.NewBookmark.Notes, sortOrder, action.NewBookmark.BookmarkGroupId);

				if (!apiResponse.IsSuccessStatusCode) {
					dispatch.Dispatch(new ErrorFetchingCreateBookmarkAction(
						"Error creating bookmark group: Status code " + apiResponse.StatusCode.ToString()));
					return;
				}

				if (apiResponse.Content == null) {
					dispatch.Dispatch(new ErrorFetchingCreateBookmarkAction(
						"Error creating bookmark group"));
					return;
				}

				dispatch.Dispatch(new AddBookmarkAction(apiResponse.Content));
				dispatch.Dispatch(new ReceivedCreateBookmarkAction());
				dispatch.Dispatch(new HideCreateBookmarkFormAction());
			} catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}
	}
}
