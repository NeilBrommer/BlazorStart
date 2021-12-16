using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Start.Client.Store.Features.CurrentContainer;
using Start.Shared.Api;

namespace Start.Client.Store.Features.DeleteBookmark {
	public class DeleteBookmarkEffects {
		public IBookmarksApi BookmarksApi { get; init; }

		public DeleteBookmarkEffects(IBookmarksApi bookmarksApi) {
			this.BookmarksApi = bookmarksApi;
		}

		[EffectMethod]
		public async Task SubmitDeleteBookmarkForm(SubmitDeleteBookmarkFormAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchDeleteBookmarkAction());

			try {
				System.Net.Http.HttpResponseMessage? apiResponse = await this.BookmarksApi
					.DeleteBookmark(action.BookmarkIdToDelete);

				if (apiResponse == null) {
					dispatch.Dispatch(new ErrorFetchingDeleteBookmarkAction(
						"Failed to make submit request"));
					return;
				}

				if (apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound) {
					dispatch.Dispatch(new ErrorFetchingDeleteBookmarkAction(
						"The bookmark to delete doesn't exist"));
					return;
				}

				if (!apiResponse.IsSuccessStatusCode) {
					dispatch.Dispatch(new ErrorFetchingDeleteBookmarkAction(
						"There was an error deleting the bookmark"));
					return;
				}

				dispatch.Dispatch(new RemoveBookmarkAction(action.BookmarkIdToDelete));
				dispatch.Dispatch(new RecievedDeleteBookmarkAction());
				dispatch.Dispatch(new HideDeleteBookmarkFormAction());
			} catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}
	}
}
