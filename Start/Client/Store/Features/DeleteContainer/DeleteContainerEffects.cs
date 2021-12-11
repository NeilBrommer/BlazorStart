using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Start.Client.Store.Features.CurrentContainer;
using Start.Shared.Api;
using System.Net;
using Start.Client.Store.Features.ContainersList;

namespace Start.Client.Store.Features.DeleteContainer {
	public class DeleteContainerEffects {
		public IBookmarkContainersApi BookmarkContainersApi { get; init; }

		public DeleteContainerEffects(IBookmarkContainersApi bookmarkContainersApi) {
			this.BookmarkContainersApi = bookmarkContainersApi;
		}

		[EffectMethod]
		public async Task SubmitDeleteContainer(SubmitDeleteContainerAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchDeleteContainerFormAction());

			try {
				System.Net.Http.HttpResponseMessage? apiResponse = await this.BookmarkContainersApi
					.DeleteBookmarkContainer(action.ContainerIdToDelete);

				if (apiResponse == null) {
					dispatch.Dispatch(
						new ErrorFetchingDeleteContainerAction("Failed to submit request"));
					return;
				}

				if (apiResponse.StatusCode == HttpStatusCode.NotFound) {
					dispatch.Dispatch(new ErrorFetchingDeleteContainerAction(
						"The bookmark container to delete doesn't exist"));
					return;
				}

				if (!apiResponse.IsSuccessStatusCode) {
					dispatch.Dispatch(new ErrorFetchingDeleteContainerAction(
						"There was an error deleting the bookmark container"));
					return;
				}

				dispatch.Dispatch(new RemoveContainerFromListAction(action.ContainerIdToDelete));
				dispatch.Dispatch(new FixCurrentContainerAction());
				dispatch.Dispatch(new RecievedDeleteContainerAction());
			} catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}
	}
}
