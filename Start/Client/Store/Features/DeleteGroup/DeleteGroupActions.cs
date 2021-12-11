namespace Start.Client.Store.Features.DeleteGroup {
	public class ShowDeleteGroupFormAction {
		public int GroupIdToDelete { get; init; }
		public string GroupTitleToDelete { get; init; }

		public ShowDeleteGroupFormAction(int groupIdToDelete, string groupTitleToDelete) {
			this.GroupIdToDelete = groupIdToDelete;
			this.GroupTitleToDelete = groupTitleToDelete;
		}
	}

	public class HideDeleteGroupFormAction { }

	public class FetchDeleteGroupFormAction { }

	public class SubmitDeleteGroupFormAction {
		public int GroupIdToDelete { get; init; }

		public SubmitDeleteGroupFormAction(int groupIdToDelete) {
			this.GroupIdToDelete = groupIdToDelete;
		}
	}

	public class ReceivedDeleteGroupAction { }

	public class ErrorFetchingDeleteGroupAction {
		public string ErrorMessage { get; init; }

		public ErrorFetchingDeleteGroupAction(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}
	}
}
