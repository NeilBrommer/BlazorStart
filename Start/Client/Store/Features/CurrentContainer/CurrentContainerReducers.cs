using System.Collections.Generic;
using System.Linq;
using Fluxor;
using Start.Client.Store.State;
using Start.Shared;

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

		[ReducerMethod]
		public static RootState AddBookmarkGroup(RootState state, AddBookmarkGroupAction action) {
			BookmarkContainerDto? container = state.CurrentContainerState.Container;

			if (container == null)
				return state;

			if (action.BookmarkGroup.BookmarkContainerId != container.BookmarkContainerId)
				return state;

			return state with {
				CurrentContainerState = state.CurrentContainerState with {
					Container = new BookmarkContainerDto(container.BookmarkContainerId,
						container.Title, container.BookmarkGroups?
							.Concat(new List<BookmarkGroupDto> { action.BookmarkGroup })
							.ToList())
				}
			};
		}

		[ReducerMethod]
		public static RootState RemoveBookmarkGroup(RootState state,
			RemoveBookmarkGroupAction action) {
			BookmarkContainerDto? container = state.CurrentContainerState.Container;

			if (container == null)
				return state;

			return state with {
				CurrentContainerState = state.CurrentContainerState with {
					Container = new BookmarkContainerDto(container.BookmarkContainerId,
						container.Title, container.BookmarkGroups?
							.Where(g => g.BookmarkGroupId != action.BookmarkGroupId)
							.ToList())
				}
			};
		}
	}
}
