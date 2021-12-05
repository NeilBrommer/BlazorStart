using System.Threading.Tasks;
using Fluxor;
using Blazored.LocalStorage;
using Start.Client.Store.Features.ContainersList;
using Start.Client.Store.Features.CurrentContainer;
using System.Linq;

namespace Start.Client.Store.State {
	public class StoreInitializedEffects {
		public IState<RootState> State { get; init; }
		public ILocalStorageService LocalStorage { get; init; }

		public StoreInitializedEffects(IState<RootState> state, ILocalStorageService localStorage) {
			this.State = state;
			this.LocalStorage = localStorage;
		}

		[EffectMethod(typeof(StoreInitializedAction))]
		public async Task InitialLoad(IDispatcher dispatch) {
			dispatch.Dispatch(new LoadContainerListAction());
			dispatch.Dispatch(new LoadCurrentContainerAction(await GetSelectedContainerId()));
		}

		private async Task<int> GetSelectedContainerId() {
			bool hasValue = await this.LocalStorage.ContainKeyAsync("SelectedContainer");

			if (hasValue)
				return await this.LocalStorage.GetItemAsync<int>("SelectedContainer");

			// Default to the first container
			int firstContainer = this.State.Value.ContainerListState.Containers
				.First().BookmarkContainerId;
			await this.SetSelectedContainer(firstContainer);
			return firstContainer;
		}

		protected async Task SetSelectedContainer(int selectedContainerId) {
			await this.LocalStorage.SetItemAsync("SelectedContainer", selectedContainerId);
		}
	}
}
