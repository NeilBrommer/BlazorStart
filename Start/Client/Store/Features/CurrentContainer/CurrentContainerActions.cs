using Start.Shared;

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

	public class AddBookmarkGroupAction {
		public BookmarkGroupDto BookmarkGroup { get; init; }

		public AddBookmarkGroupAction(BookmarkGroupDto bookmarkGroup) {
			this.BookmarkGroup = bookmarkGroup;
		}
	}

	public class RemoveBookmarkGroupAction {
		public int BookmarkGroupId { get; init; }

		public RemoveBookmarkGroupAction(int bookmarkGroupId) {
			this.BookmarkGroupId = bookmarkGroupId;
		}
	}

	public class AddBookmarkAction {
		public BookmarkDto Bookmark { get; init; }

		public AddBookmarkAction(BookmarkDto bookmark) {
			this.Bookmark = bookmark;
		}
	}

	public class RemoveBookmarkAction {
		public int BookmarkId { get; init; }

		public RemoveBookmarkAction(int bookmarkId) {
			this.BookmarkId = bookmarkId;
		}
	}
}
