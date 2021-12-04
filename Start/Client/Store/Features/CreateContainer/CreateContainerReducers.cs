using Fluxor;

namespace Start.Client.Store.Features.CreateContainer {
	public static class CreateContainerReducers {
		[ReducerMethod(typeof(ShowCreateContainerFormAction))]
		public static CreateContainerState ShowCreateContainerForm(CreateContainerState state) {
			return state with {
				ShowCreateContainerForm = true
			};
		}

		[ReducerMethod(typeof(HideCreateContainerFormAction))]
		public static CreateContainerState HideCreateContainerForm(CreateContainerState state) {
			return state with {
				ShowCreateContainerForm = false
			};
		}

		[ReducerMethod(typeof(FetchCreateContainerAction))]
		public static CreateContainerState FetchCreateContainer(CreateContainerState state) {
			return state with {
				IsLoadingCreateContainer = true
			};
		}

		[ReducerMethod(typeof(ReceivedCreateContainerAction))]
		public static CreateContainerState ReceivedCreateContainer(CreateContainerState state) {
			return state with {
				IsLoadingCreateContainer = false,
				CreateContainerErrorMessage = null,
				ShowCreateContainerForm = false
			};
		}

		[ReducerMethod]
		public static CreateContainerState ErrorFetchingCreateContainer(CreateContainerState state,
			ErrorFetchingCreateContainerAction action) {
			return state with {
				IsLoadingCreateContainer = false,
				CreateContainerErrorMessage = action.ErrorMessage
			};
		}
	}
}
