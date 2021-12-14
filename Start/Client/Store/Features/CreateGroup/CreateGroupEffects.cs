using Start.Shared.Api;
using Fluxor;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Refit;
using Start.Shared;
using Start.Client.Store.Features.CurrentContainer;
using System;

namespace Start.Client.Store.Features.CreateGroup {
	public class CreateGroupEffects {
		public IBookmarkGroupsApi BookmarkGroupsApi { get; init; }

		public CreateGroupEffects(IBookmarkGroupsApi bookmarksApi) {
			this.BookmarkGroupsApi = bookmarksApi;
		}

		[EffectMethod]
		public async Task SubmitCreateBookmarkGroup(SubmitCreateGroupAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchCreateGroupAction());

			try {
				ApiResponse<BookmarkGroupDto?> apiResponse = await this.BookmarkGroupsApi
					.CreateBookmarkGroup(action.NewGroup.Title, action.NewGroup.Color,
					action.NewGroup.BookmarkContainerId);

				Console.WriteLine("Status code: " + apiResponse.StatusCode);

				if (!apiResponse.IsSuccessStatusCode) {
					dispatch.Dispatch(new ErrorFetchingCreateGroupAction(
						"Error creating bookmark group"));
					return;
				}

				if (apiResponse.Content == null) {
					dispatch.Dispatch(new ErrorFetchingCreateGroupAction(
						"Error creating bookmark group"));
					return;
				}

				dispatch.Dispatch(new AddBookmarkGroupAction(apiResponse.Content));
				dispatch.Dispatch(new RecievedCreateGroupAction());
				dispatch.Dispatch(new HideCreateGroupFormAction());
			} catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}
	}
}
