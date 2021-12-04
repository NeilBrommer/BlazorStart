using System.Collections.Immutable;
using System.Linq;
using Fluxor;
using Start.Client.Store.State;
using Start.Shared;

namespace Start.Client.Store.Features.ContainersList {
	public static class ContainerListReducers {
		[ReducerMethod(typeof(FetchContainerListAction))]
		public static RootState OnFetchContainerList(RootState state) {
			return state with {
				ContainerListState = state.ContainerListState with {
					Containers = ImmutableList<BookmarkContainerDto>.Empty,
					IsLoadingContainersList = true
				}
			};
		}

		[ReducerMethod]
		public static RootState OnRecievedContainerList(RootState state,
			RecievedContainerListAction action) {
			return state with {
				ContainerListState = state.ContainerListState with {
					Containers = action.Containers.ToImmutableList(),
					IsLoadingContainersList = false,
					ErrorMessage = null
				}
			};
		}

		[ReducerMethod]
		public static RootState OnErrorFetchingContainerList(RootState state,
			ErrorFetchingContainerListAction action) {
			return state with {
				ContainerListState = state.ContainerListState with {
					ErrorMessage = action.ErrorMessage
				}
			};
		}

		[ReducerMethod]
		public static RootState AddContainerToList(RootState state,
			AddContainerToListAction action) {
			return state with {
				ContainerListState = state.ContainerListState with {
					Containers = state.ContainerListState.Containers.Add(action.NewContainer)
				}
			};
		}

		[ReducerMethod]
		public static RootState RemoveContainerFromList(RootState state,
			RemoveContainerFromListAction action) {
			return state with {
				ContainerListState = state.ContainerListState with {
					Containers = state.ContainerListState.Containers
						.Where(c => c.BookmarkContainerId != action.ContainerIdToRemove)
						.ToImmutableList()
				}
			};
		}
	}
}
