﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Refit;
using Start.Client.Store.State;
using Start.Client.Store.Features.CreateContainer;
using Start.Shared;
using Start.Shared.Api;

namespace Start.Client.Store.Features.CurrentContainer {
	public class CurrentContainerEffects {
		public IBookmarkContainersApi BookmarkContainersApi { get; set; }
		public ILocalStorageService LocalStorage { get; set; }
		public IState<RootState> RootState { get; set; }

		public CurrentContainerEffects(IBookmarkContainersApi bookmarkContainersApi,
			ILocalStorageService localStorage, IState<RootState> rootState) {
			this.BookmarkContainersApi = bookmarkContainersApi;
			this.LocalStorage = localStorage;
			this.RootState = rootState;
		}

		[EffectMethod]
		public async Task LoadCurrentContainer(LoadCurrentContainerAction action,
			IDispatcher dispatch) {
			dispatch.Dispatch(new FetchCurrentContainerAction());

			try {
				ApiResponse<BookmarkContainerDto?> response = await this.BookmarkContainersApi
					.GetBookmarkContainer(action.BookmarkContainerId);

				BookmarkContainerDto? container = response.Content;

				if (container == null) {
					dispatch.Dispatch(new ErrorFetchingCurrentContainerAction(
						"Failed to get current bookmark container"));
					return;
				}

				dispatch.Dispatch(new ReceivedCurrentContainerAction(container));

				await this.LocalStorage
					.SetItemAsync("SelectedContainer", action.BookmarkContainerId);
			} catch (AccessTokenNotAvailableException e) {
				e.Redirect();
			}
		}

		[EffectMethod(typeof(FixCurrentContainerAction))]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async Task FixCurrentContainer(IDispatcher dispatch) {
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
			if (!this.RootState.Value.ContainerListState.Containers.Any()) {
				dispatch.Dispatch(new SubmitCreateContainerAction(
					new BookmarkContainerDto("Default")));
				return;
			}

			IEnumerable<int?> containerIds = this.RootState.Value.ContainerListState.Containers
				.Select(c => (int?)c.BookmarkContainerId);
			int? currentContainerId = this.RootState.Value.CurrentContainerState.Container
				?.BookmarkContainerId;

			if (containerIds.Contains(currentContainerId))
				return;

			int firstContainerId = this.RootState.Value.ContainerListState.Containers
				.First().BookmarkContainerId;

			dispatch.Dispatch(new LoadCurrentContainerAction(firstContainerId));
		}
	}
}
