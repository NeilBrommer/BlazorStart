using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Start.Shared.Api;
using Start.Client.Store.Features.CurrentContainer;

namespace Start.Client.Store.Features.DeleteGroup {
	public class DeleteGroupEffects {
		public IBookmarkGroupsApi BookmarkGroupsApi { get; init; }

		public DeleteGroupEffects(IBookmarkGroupsApi bookmarkGroupsApi) {
			this.BookmarkGroupsApi = bookmarkGroupsApi;
		}

		[EffectMethod]
		public async Task SubmitDeleteGroupForm(SubmitDeleteGroupFormAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchDeleteGroupFormAction());

			try {
				System.Net.Http.HttpResponseMessage? apiResonse = await this.BookmarkGroupsApi
					.DeleteBookmarkGroup(action.GroupIdToDelete);

				if (apiResonse == null) {
					dispatch.Dispatch(new ErrorFetchingDeleteGroupAction(
						"Failed to submit request"));
					return;
				}

				if (apiResonse.StatusCode == System.Net.HttpStatusCode.NotFound) {
					dispatch.Dispatch(new ErrorFetchingDeleteGroupAction(
						"The bookmark group to delete doesn't exist"));
					return;
				}

				if (!apiResonse.IsSuccessStatusCode) {
					dispatch.Dispatch(new ErrorFetchingDeleteGroupAction(
						"There was an error deleting the bookmark group"));
					return;
				}

				dispatch.Dispatch(new RemoveBookmarkGroupAction(action.GroupIdToDelete));
				dispatch.Dispatch(new ReceivedDeleteGroupAction());
			} catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}
	}
}
