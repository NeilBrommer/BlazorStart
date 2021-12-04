using Start.Client.Store.State;

namespace Start.Client.Store.Features.DeleteContainer {
	public record DeleteContainerState : RootState {
		public bool ShowDeleteContainerForm { get; set; }
		public int BookmarkContainerIdToDelete { get; set; }
		public string BookmarkContainerTitleToDelete { get; set; }
		public bool IsLoadingDeleteContainer { get; set; }
		public string? DeleteContainerErrorMessage { get; set; }

		public DeleteContainerState() {
			this.BookmarkContainerIdToDelete = 0;
			this.BookmarkContainerTitleToDelete = "";
		}

		public DeleteContainerState(ContainerListState containerList,
			CurrentContainerState currentContainer, bool showDeleteContainerForm,
			int containerIdToDelete, string containerTitleToDelete, bool isLoadingDeleteContainer,
			string? errorMessage)
			: base(containerList, currentContainer) {
			this.ShowDeleteContainerForm = showDeleteContainerForm;
			this.BookmarkContainerIdToDelete = containerIdToDelete;
			this.BookmarkContainerTitleToDelete = containerTitleToDelete;
			this.IsLoadingDeleteContainer = isLoadingDeleteContainer;
			this.DeleteContainerErrorMessage = errorMessage;
		}
	}
}
