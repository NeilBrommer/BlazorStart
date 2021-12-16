using Start.Shared;

namespace Start.Client.Store.Features.DeleteBookmark {
	public class ShowDeleteBookmarkFormAction {
		public BookmarkDto BookmarkToDelete { get; init; }

		public ShowDeleteBookmarkFormAction(BookmarkDto bookmarkToDelete) {
			this.BookmarkToDelete = bookmarkToDelete;
		}
	}

	public class HideDeleteBookmarkFormAction { }

	public class FetchDeleteBookmarkAction { }

	public class RecievedDeleteBookmarkAction { }

	public class ErrorFetchingDeleteBookmarkAction {
		public string ErrorMessage { get; init; }

		public ErrorFetchingDeleteBookmarkAction(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}
	}

	public class SubmitDeleteBookmarkFormAction {
		public int BookmarkIdToDelete { get; init; }

		public SubmitDeleteBookmarkFormAction(int bookmarkIdToDelete) {
			this.BookmarkIdToDelete = bookmarkIdToDelete;
		}
	}
}
