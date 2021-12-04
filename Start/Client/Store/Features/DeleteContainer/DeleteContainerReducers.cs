using Fluxor;

namespace Start.Client.Store.Features.DeleteContainer {
	public static class DeleteContainerReducers {
		[ReducerMethod]
		public static DeleteContainerState ShowDeleteContainerForm(DeleteContainerState state,
			ShowDeleteContainerFormAction action) {
			return state with {
				ShowDeleteContainerForm = true,
				BookmarkContainerIdToDelete = action.ContainerIdToDelete,
				BookmarkContainerTitleToDelete = action.ContainerTitleToDelete,
				DeleteContainerErrorMessage = null
			};
		}

		[ReducerMethod(typeof(HideDeleteContainerFormAction))]
		public static DeleteContainerState HideDeleteContainerForm(DeleteContainerState state) {
			return state with {
				ShowDeleteContainerForm = false,
				DeleteContainerErrorMessage = null
			};
		}

		[ReducerMethod(typeof(FetchDeleteContainerFormAction))]
		public static DeleteContainerState FetchDeleteContainerForm(DeleteContainerState state) {
			return state with {
				IsLoadingDeleteContainer = true,
				DeleteContainerErrorMessage = null
			};
		}

		[ReducerMethod(typeof(RecievedDeleteContainerAction))]
		public static DeleteContainerState ReceivedDeleteContainer(DeleteContainerState state) {
			return state with {
				IsLoadingDeleteContainer = false,
				DeleteContainerErrorMessage = null,
				ShowDeleteContainerForm = false
			};
		}

		[ReducerMethod]
		public static DeleteContainerState ErrorFetchingDeleteContainer(DeleteContainerState state,
			ErrorFetchingDeleteContainerAction action) {
			return state with {
				DeleteContainerErrorMessage = action.ErrorMessage,
				IsLoadingDeleteContainer = false
			};
		}
	}
}
