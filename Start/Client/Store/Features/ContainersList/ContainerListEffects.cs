using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Refit;
using Start.Client.Store.Features.CreateContainer;
using Start.Client.Store.Features.CurrentContainer;
using Start.Client.Store.State;
using Start.Shared;
using Start.Shared.Api;

namespace Start.Client.Store.Features.ContainersList {
	public class ContainerListEffects {
		public IBookmarkContainersApi BookmarkContainersApi { get; init; }
		public ILocalStorageService LocalStorage { get; set; }

		public ContainerListEffects(IBookmarkContainersApi bookmarkContainersApi,
			ILocalStorageService localStorage) {
			this.BookmarkContainersApi = bookmarkContainersApi;
			this.LocalStorage = localStorage;
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
					ApiResponse<BookmarkContainerDto?>? createResponse = await this
						.BookmarkContainersApi
						.CreateBookmarkContainer("Default", 0);

					BookmarkContainerDto? newContainer = createResponse.Content;

					if (newContainer == null) {
						dispatch.Dispatch(new ErrorFetchingContainerListAction(
							"Failed to create default container"));
						return;
					}

					bookmarkContainers = new List<BookmarkContainerDto> { newContainer };

					await this.SetSelectedContainer(newContainer.BookmarkContainerId);
					dispatch.Dispatch(new LoadCurrentContainerAction(newContainer.BookmarkContainerId));
				}

				dispatch.Dispatch(new RecievedContainerListAction(bookmarkContainers));
			}
			catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}

		private async Task SetSelectedContainer(int selectedContainerId) {
			await this.LocalStorage.SetItemAsync("SelectedContainer", selectedContainerId);
		}
	}
}
