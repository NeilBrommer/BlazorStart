using Start.Shared.Api;
using Fluxor;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Refit;
using Start.Shared;
using Start.Client.Store.Features.CurrentContainer;
using System;
using System.Linq;

namespace Start.Client.Store.Features.CreateGroup {
	public class CreateGroupEffects {
		public IBookmarkGroupsApi BookmarkGroupsApi { get; init; }
		public IBookmarkContainersApi BookmarkContainersApi { get; init; }

		public CreateGroupEffects(IBookmarkGroupsApi bookmarksApi,
			IBookmarkContainersApi bookmarkContainersApi) {
			this.BookmarkGroupsApi = bookmarksApi;
			this.BookmarkContainersApi = bookmarkContainersApi;
		}

		[EffectMethod]
		public async Task SubmitCreateBookmarkGroup(SubmitCreateGroupAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchCreateGroupAction());

			try {
				ApiResponse<BookmarkContainerDto?>? containerResponse = await this
					.BookmarkContainersApi
					.GetBookmarkContainer(action.NewGroup.BookmarkContainerId);

				if (containerResponse == null || containerResponse.Content == null) {
					dispatch.Dispatch(new ErrorFetchingCreateGroupAction(
						"There was an error checking the new group's bookmark container"));
					return;
				}

				int sortOrder = !(containerResponse.Content.BookmarkGroups?.Any() ?? false)
					? 0
					: containerResponse.Content.BookmarkGroups.Max(g => g.SortOrder) + 1;

				ApiResponse<BookmarkGroupDto?> apiResponse = await this.BookmarkGroupsApi
					.CreateBookmarkGroup(action.NewGroup.Title, action.NewGroup.Color, sortOrder,
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
