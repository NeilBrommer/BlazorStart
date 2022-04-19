using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Refit;
using Start.Shared;
using Start.Shared.Api;
using Start.Client.Store.Features.ContainersList;
using System.Collections.Generic;
using System.Linq;

namespace Start.Client.Store.Features.CreateContainer {
	public class CreateContainerEffects {
		public IBookmarkContainersApi BookmarkContainersApi { get; init; }

		public CreateContainerEffects(IBookmarkContainersApi bookmarkContainersApi) {
			this.BookmarkContainersApi = bookmarkContainersApi;
		}

		[EffectMethod]
		public async Task SubmitCreateContainer(SubmitCreateContainerAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchCreateContainerAction());

			try {
				ApiResponse<IEnumerable<BookmarkContainerDto>>? containersResponse = await this
					.BookmarkContainersApi
					.GetAllBookmarkContainers();

				if (containersResponse == null || containersResponse.Content == null) {
					dispatch.Dispatch(new ErrorFetchingCreateContainerAction(
						"There was an error checking bookmark containers"));
					return;
				}

				int sortOrder = !containersResponse.Content.Any()
					? 0
					: containersResponse.Content.Max(c => c.SortOrder) + 1;

				ApiResponse<BookmarkContainerDto?> apiResponse = await this.BookmarkContainersApi
					.CreateBookmarkContainer(action.NewContainer.Title, sortOrder);

				BookmarkContainerDto? container = apiResponse.Content;

				if (container == null) {
					dispatch.Dispatch(new ErrorFetchingCreateContainerAction(
						"Failed to create container"));
					return;
				}

				dispatch.Dispatch(new AddContainerToListAction(container));
				dispatch.Dispatch(new ReceivedCreateContainerAction());
			} catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}
	}
}
