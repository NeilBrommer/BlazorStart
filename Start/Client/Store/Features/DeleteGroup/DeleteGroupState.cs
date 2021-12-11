using System;
namespace Start.Client.Store.Features.DeleteGroup {
	public record DeleteGroupState {
		public bool ShowDeleteGroupForm { get; init; }
		public int BookmarkGroupIdToDelete { get; init; }
		public string BookmarkGroupTitleToDelete { get; init; }
		public bool IsLoadingDeleteGroup { get; init; }
		public string? DeleteGroupErrorMessage { get; init; }

		public DeleteGroupState() {
			this.BookmarkGroupTitleToDelete = "";
		}

		public DeleteGroupState(bool showDeleteGroupForm, int bookmarkGroupIdToDelete,
			string bookmarkTitleToDelete, bool isLoadingDeleteGroup,
			string? deleteGroupErrorMessage) {
			this.ShowDeleteGroupForm = showDeleteGroupForm;
			this.BookmarkGroupIdToDelete = bookmarkGroupIdToDelete;
			this.BookmarkGroupTitleToDelete = bookmarkTitleToDelete;
			this.IsLoadingDeleteGroup = isLoadingDeleteGroup;
			this.DeleteGroupErrorMessage = deleteGroupErrorMessage;
		}
	}
}
