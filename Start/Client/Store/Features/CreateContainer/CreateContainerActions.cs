using Start.Shared;

namespace Start.Client.Store.Features.CreateContainer {
	public class ShowCreateContainerFormAction { }

	public class HideCreateContainerFormAction { }

	public class FetchCreateContainerAction { }

	public class ReceivedCreateContainerAction { }

	public class ErrorFetchingCreateContainerAction {
		public string ErrorMessage { get; set; }

		public ErrorFetchingCreateContainerAction(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}
	}

	public class SubmitCreateContainerAction {
		public BookmarkContainerDto NewContainer { get; set; }

		public SubmitCreateContainerAction(BookmarkContainerDto container) {
			this.NewContainer = container;
		}
	}
}
