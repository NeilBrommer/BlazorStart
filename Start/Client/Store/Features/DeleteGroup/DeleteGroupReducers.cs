using Fluxor;

namespace Start.Client.Store.Features.DeleteGroup {
	public static class DeleteGroupReducers {
		[ReducerMethod]
		public static DeleteGroupState ShowDeleteGroupForm(DeleteGroupState state,
			ShowDeleteGroupFormAction action) {
			return state with {
				ShowDeleteGroupForm = true,
				BookmarkGroupIdToDelete = action.GroupIdToDelete,
				BookmarkGroupTitleToDelete = action.GroupTitleToDelete,
				IsLoadingDeleteGroup = false,
				DeleteGroupErrorMessage = null
			};
		}

		[ReducerMethod(typeof(HideDeleteGroupFormAction))]
		public static DeleteGroupState HideDeleteGroupForm(DeleteGroupState state) {
			return state with {
				 ShowDeleteGroupForm = false
			};
		}

		[ReducerMethod(typeof(FetchDeleteGroupFormAction))]
		public static DeleteGroupState FetchDeleteGroup(DeleteGroupState state) {
			return state with {
				IsLoadingDeleteGroup = true,
				DeleteGroupErrorMessage = null
			};
		}

		[ReducerMethod(typeof(ReceivedDeleteGroupAction))]
		public static DeleteGroupState ReceivedDeleteGroup(DeleteGroupState state) {
			return state with {
				IsLoadingDeleteGroup = false,
				ShowDeleteGroupForm = false
			};
		}

		[ReducerMethod]
		public static DeleteGroupState ErrorFetchingDeleteGroup(DeleteGroupState state,
			ErrorFetchingDeleteGroupAction action) {
			return state with {
				IsLoadingDeleteGroup = false,
				DeleteGroupErrorMessage = action.ErrorMessage
			};
		}
	}
}
