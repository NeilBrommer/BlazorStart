namespace Start.Client.Store.Features.DeleteContainer {
	public class ShowDeleteContainerFormAction {
		public int ContainerIdToDelete { get; init; }
		public string ContainerTitleToDelete { get; init; }

		public ShowDeleteContainerFormAction(int containerIdToDelete,
			string containerTitleToDelete) {
			this.ContainerIdToDelete = containerIdToDelete;
			this.ContainerTitleToDelete = containerTitleToDelete;
		}
	}

	public class HideDeleteContainerFormAction { }

	public class FetchDeleteContainerFormAction { }

	public class SubmitDeleteContainerAction {
		public int ContainerIdToDelete { get; init; }

		public SubmitDeleteContainerAction(int containerIdToDelete) {
			this.ContainerIdToDelete = containerIdToDelete;
		}
	}

	public class RecievedDeleteContainerAction { }

	public class ErrorFetchingDeleteContainerAction {
		public string ErrorMessage { get; init; }

		public ErrorFetchingDeleteContainerAction(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}
	}
}
