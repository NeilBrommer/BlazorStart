using Start.Client.Store.State;
using Start.Shared;

namespace Start.Client.Store.Features.DeleteBookmark {
	public record DeleteBookmarkState : RootState {
		public bool ShowDeleteBookmarkForm { get; init; }
		public BookmarkDto? BookmarkToDelete { get; init; }
		public bool IsLoadingDeleteBookmark { get; init; }
		public string? DeleteBookmarkErrorMessage { get; init; }

		public DeleteBookmarkState() { }

		public DeleteBookmarkState(bool showDeleteBookmarkForm, BookmarkDto bookmarkToDelete,
			bool isLoadingDeleteBookmark, string? deleteBookmarkErrorMessage) {
			this.ShowDeleteBookmarkForm = showDeleteBookmarkForm;
			this.BookmarkToDelete = bookmarkToDelete;
			this.IsLoadingDeleteBookmark = isLoadingDeleteBookmark;
			this.DeleteBookmarkErrorMessage = deleteBookmarkErrorMessage;
		}
	}
}
