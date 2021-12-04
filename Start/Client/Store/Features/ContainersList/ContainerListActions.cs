using System.Collections.Generic;
using Start.Shared;

namespace Start.Client.Store.Features.ContainersList {
	/// <summary>Dispatch before sending an API request</summary>
	public class FetchContainerListAction { }

	/// <summary>Dispatch after recieving the container list from an API request</summary>
	public class RecievedContainerListAction {
		public IList<BookmarkContainerDto> Containers { get; set; }

		public RecievedContainerListAction(IList<BookmarkContainerDto> containers) {
			this.Containers = containers;
		}
	}

	public class ErrorFetchingContainerListAction {
		public string ErrorMessage { get; set; }

		public ErrorFetchingContainerListAction(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}
	}

	public class LoadContainerListAction { }

	public class RemoveContainerFromListAction {
		public int ContainerIdToRemove { get; set; }

		public RemoveContainerFromListAction(int containerIdToRemove) {
			this.ContainerIdToRemove = containerIdToRemove;
		}
	}

	public class AddContainerToListAction {
		public BookmarkContainerDto NewContainer { get; set; }

		public AddContainerToListAction(BookmarkContainerDto newContainer) {
			this.NewContainer = newContainer;
		}
	}
}
