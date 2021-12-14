using Fluxor;

namespace Start.Client.Store.Features.CreateBookmark {
	public static class CreateBookmarkReducers {
		[ReducerMethod]
		public static CreateBookmarkState ShowCreateBookmarkForm(CreateBookmarkState state,
			ShowCreateBookmarkFormAction action) {
			return state with {
				ShowCreateBookmarkForm = true,
				GroupId = action.GroupId,
				GroupTitle = action.GroupTitle,
				IsLoadingCreateBookmark = false,
				CreateBookmarkErrorMessage = null
			};
		}

		[ReducerMethod(typeof(HideCreateBookmarkFormAction))]
		public static CreateBookmarkState HideCreateBookmarkForm(CreateBookmarkState state) {
			return state with {
				ShowCreateBookmarkForm = false
			};
		}

		[ReducerMethod(typeof(FetchCreateBookmarkAction))]
		public static CreateBookmarkState FetchCreateBookmark(CreateBookmarkState state) {
			return state with {
				IsLoadingCreateBookmark = true,
				CreateBookmarkErrorMessage = null
			};
		}

		[ReducerMethod]
		public static CreateBookmarkState ErrorFetchingCreateBookmark(CreateBookmarkState state,
			ErrorFetchingCreateBookmarkAction action) {
			return state with {
				CreateBookmarkErrorMessage = action.ErrorMessage,
				IsLoadingCreateBookmark = false
			};
		}
	}
}
