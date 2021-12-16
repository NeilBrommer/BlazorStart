using Fluxor;

namespace Start.Client.Store.Features.DeleteBookmark {
	public static class DeleteBookmarkReducers {
		[ReducerMethod]
		public static DeleteBookmarkState ShowDeleteBookmarkForm(DeleteBookmarkState state,
			ShowDeleteBookmarkFormAction action) {
			return state with {
				ShowDeleteBookmarkForm = true,
				BookmarkToDelete = action.BookmarkToDelete,
				IsLoadingDeleteBookmark = false,
				DeleteBookmarkErrorMessage = null
			};
		}

		[ReducerMethod(typeof(HideDeleteBookmarkFormAction))]
		public static DeleteBookmarkState HideDeleteBookmarkForm(DeleteBookmarkState state) {
			return state with {
				ShowDeleteBookmarkForm = false
			};
		}

		[ReducerMethod(typeof(FetchDeleteBookmarkAction))]
		public static DeleteBookmarkState FetchDeleteBookmark(DeleteBookmarkState state) {
			return state with {
				IsLoadingDeleteBookmark = true,
				DeleteBookmarkErrorMessage = null
			};
		}

		[ReducerMethod(typeof(RecievedDeleteBookmarkAction))]
		public static DeleteBookmarkState RecievedDeleteBookmark(DeleteBookmarkState state) {
			return state with {
				IsLoadingDeleteBookmark = false,
				DeleteBookmarkErrorMessage = null
			};
		}
	}
}
