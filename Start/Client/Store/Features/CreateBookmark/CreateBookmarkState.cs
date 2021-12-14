using Start.Client.Store.State;

namespace Start.Client.Store.Features.CreateBookmark {
	public record CreateBookmarkState : RootState {
		public bool ShowCreateBookmarkForm { get; init; }
		public int GroupId { get; init; }
		public string GroupTitle { get; init; }
		public bool IsLoadingCreateBookmark { get; init; }
		public string? CreateBookmarkErrorMessage { get; init; }

		public CreateBookmarkState() {
			this.GroupTitle = "";
		}
	}
}
