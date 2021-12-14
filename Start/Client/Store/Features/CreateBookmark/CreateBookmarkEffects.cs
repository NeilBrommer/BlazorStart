using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Start.Shared.Api;
using Start.Client.Store.Features.CurrentContainer;

namespace Start.Client.Store.Features.CreateBookmark {
	public class CreateBookmarkEffects {
		public IBookmarksApi BookmarksApi { get; init; }

		public CreateBookmarkEffects(IBookmarksApi bookmarksApi) {
			this.BookmarksApi = bookmarksApi;
		}

		[EffectMethod]
		public async Task SubmitCreateBookmark(SubmitCreateBookmarkAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchCreateBookmarkAction());

			try {
				Refit.ApiResponse<Start.Shared.BookmarkDto?>? apiResponse = await this.BookmarksApi
					.CreateBookmark(action.NewBookmark.Title, action.NewBookmark.Url,
						action.NewBookmark.Notes, action.NewBookmark.BookmarkGroupId);

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
