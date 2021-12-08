using System.Collections.Immutable;
using Fluxor;
using Start.Shared;

namespace Start.Client.Store.State {
	[FeatureState]
	public record RootState {
		public ContainerListState ContainerListState { get; init; }
		public CurrentContainerState CurrentContainerState { get; init; }

		public bool ShowSidebar { get; init; }
		public bool EditMode { get; init; }

		public RootState() {
			this.ContainerListState = new ContainerListState();
			this.CurrentContainerState = new CurrentContainerState();
		}

		public RootState(ContainerListState containerList, CurrentContainerState currentContainer) {
			this.ContainerListState = containerList;
			this.CurrentContainerState = currentContainer;
		}
	}

	public record ContainerListState {
		public ImmutableList<BookmarkContainerDto> Containers { get; init; }
		public bool IsLoadingContainersList { get; init; }
		public string? ErrorMessage { get; init; }

		public ContainerListState() {
			this.Containers = ImmutableList<BookmarkContainerDto>.Empty;
			this.IsLoadingContainersList = false;
			this.ErrorMessage = null;
		}

		public ContainerListState(ImmutableList<BookmarkContainerDto> containers,
			bool isLoadingContainersList, string? errorMessage) {
			this.Containers = containers;
			this.IsLoadingContainersList = isLoadingContainersList;
			this.ErrorMessage = errorMessage;
		}
	}

	public record CurrentContainerState {
		public BookmarkContainerDto? Container { get; init; }
		public bool IsLoadingCurrentContainer { get; init; }
		public string? ErrorMessage { get; init; }

		public CurrentContainerState() {
			this.Container = null;
			this.IsLoadingCurrentContainer = false;
			this.ErrorMessage = null;
		}

		public CurrentContainerState(BookmarkContainerDto? currentContainer,
			bool isLoadingCurrentContainer, string? errorMessage) {
			this.Container = currentContainer;
			this.IsLoadingCurrentContainer = isLoadingCurrentContainer;
			this.ErrorMessage = errorMessage;
		}
	}
}
