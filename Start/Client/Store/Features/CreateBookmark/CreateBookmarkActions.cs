using Start.Shared;

namespace Start.Client.Store.Features.CreateBookmark {
	public class ShowCreateBookmarkFormAction {
		public int GroupId { get; init; }
		public string GroupTitle { get; init; }

		public ShowCreateBookmarkFormAction(int groupId, string groupTitle) {
			this.GroupId = groupId;
			this.GroupTitle = groupTitle;
		}
	}

	public class HideCreateBookmarkFormAction { }

	public class FetchCreateBookmarkAction { }

	public class ReceivedCreateBookmarkAction { }

	public class ErrorFetchingCreateBookmarkAction {
		public string ErrorMessage { get; init; }

		public ErrorFetchingCreateBookmarkAction(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}
	}

	public class SubmitCreateBookmarkAction {
		public BookmarkDto NewBookmark { get; init; }

		public SubmitCreateBookmarkAction(BookmarkDto newBookmark) {
			this.NewBookmark = newBookmark;
		}
	}
}
