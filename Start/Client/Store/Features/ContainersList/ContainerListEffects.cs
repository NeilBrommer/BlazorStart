using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Refit;
using Start.Client.Store.Features.CreateContainer;
using Start.Shared;
using Start.Shared.Api;

namespace Start.Client.Store.Features.ContainersList {
	public class ContainerListEffects {
		public IBookmarkContainersApi BookmarkContainersApi { get; init; }

		public ContainerListEffects(IBookmarkContainersApi bookmarkContainersApi) {
			this.BookmarkContainersApi = bookmarkContainersApi;
		}

		[EffectMethod(typeof(LoadContainerListAction))]
		public async Task LoadContainerList(IDispatcher dispatch) {
			dispatch.Dispatch(new FetchContainerListAction());

			try {
				ApiResponse<IEnumerable<BookmarkContainerDto>> response = await this
					.BookmarkContainersApi
					.GetAllBookmarkContainers();

				List<BookmarkContainerDto>? bookmarkContainers = response.Content?.ToList();

				if (bookmarkContainers == null) {
					dispatch.Dispatch(new ErrorFetchingContainerListAction(
						"Failed to fetch containers list"));
					return;
				}

				if (!bookmarkContainers.Any()) {
					dispatch.Dispatch(new SubmitCreateContainerAction(
						new BookmarkContainerDto("Default", 0)));

					// And load again
					response = await this
						.BookmarkContainersApi
						.GetAllBookmarkContainers();

					bookmarkContainers = response.Content?.ToList();

					if (bookmarkContainers == null) {
						dispatch.Dispatch(new ErrorFetchingContainerListAction(
							"Failed to fetch containers list"));
						return;
					}
				}

				dispatch.Dispatch(new RecievedContainerListAction(bookmarkContainers));
			}
			catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}
	}
}
