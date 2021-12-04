using Fluxor;
using Start.Client.Store.State;

namespace Start.Client.Store.Features.CurrentContainer {
	public static class CurrentContainerReducers {
		[ReducerMethod(typeof(FetchCurrentContainerAction))]
		public static RootState FetchCurrentContainer(RootState state) {
			return state with {
				CurrentContainerState = state.CurrentContainerState with {
					Container = null,
					IsLoadingCurrentContainer = true,
					ErrorMessage = null
				}
			};
		}

		[ReducerMethod]
		public static RootState ReceivedCurrentContainer(RootState state,
			ReceivedCurrentContainerAction action) {
			return state with {
				CurrentContainerState = state.CurrentContainerState with {
					Container = action.BookmarkContainer,
					IsLoadingCurrentContainer = false,
					ErrorMessage = null
				}
			};
		}

		[ReducerMethod]
		public static RootState ErrorFetchingCurrentContainer(RootState state,
			ErrorFetchingCurrentContainerAction action) {
			return state with {
				CurrentContainerState = state.CurrentContainerState with {
					Container = null,
					IsLoadingCurrentContainer = false,
					ErrorMessage = action.ErrorMessage
				}
			};
		}
	}
}
