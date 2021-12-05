using Start.Shared;

namespace Start.Client.Store.Features.CreateGroup {
	public class ShowCreateGroupFormAction {
		public int ContainerId { get; init; }
		public string ContainerTitle { get; init; }

		public ShowCreateGroupFormAction(int containerId, string containerTitle) {
			this.ContainerId = containerId;
			this.ContainerTitle = containerTitle;
		}
	}

	public class HideCreateGroupFormAction { }

	public class FetchCreateGroupAction { }

	public class RecievedCreateGroupAction { }

	public class ErrorFetchingCreateGroupAction {
		public string ErrorMessage { get; init; }

		public ErrorFetchingCreateGroupAction(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}
	}

	public class SubmitCreateGroupAction {
		public BookmarkGroupDto NewGroup { get; init; }

		public SubmitCreateGroupAction(BookmarkGroupDto newGroup) {
			this.NewGroup = newGroup;
		}
	}
}
