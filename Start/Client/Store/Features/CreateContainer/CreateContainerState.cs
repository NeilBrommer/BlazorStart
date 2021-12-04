using Start.Client.Store.State;

namespace Start.Client.Store.Features.CreateContainer {
	public record CreateContainerState : RootState {
		public bool ShowCreateContainerForm { get; init; }
		public bool IsLoadingCreateContainer { get; init; }
		public string? CreateContainerErrorMessage { get; init; }

		public CreateContainerState() { }

		public CreateContainerState(ContainerListState containerList,
			CurrentContainerState currentContainer, bool showCreateContainer, bool isLoading,
			string? errorMessage)
			: base(containerList, currentContainer) {
			this.ShowCreateContainerForm = showCreateContainer;
			this.IsLoadingCreateContainer = isLoading;
			this.CreateContainerErrorMessage = errorMessage;
		}
	}
}
