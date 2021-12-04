﻿using Start.Shared;

namespace Start.Client.Store.Features.CurrentContainer {
	public class FetchCurrentContainerAction { }

	public class ReceivedCurrentContainerAction {
		public BookmarkContainerDto BookmarkContainer { get; init; }

		public ReceivedCurrentContainerAction(BookmarkContainerDto bookmarkContainer) {
			this.BookmarkContainer = bookmarkContainer;
		}
	}

	public class ErrorFetchingCurrentContainerAction {
		public string ErrorMessage { get; init; }

		public ErrorFetchingCurrentContainerAction(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}
	}

	public class LoadCurrentContainerAction {
		public int BookmarkContainerId { get; init; }

		public LoadCurrentContainerAction(int bookmarkContainerId) {
			this.BookmarkContainerId = bookmarkContainerId;
		}
	}

	public class FixCurrentContainerAction { }
}
