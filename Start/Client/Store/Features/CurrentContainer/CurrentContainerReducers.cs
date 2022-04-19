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
			BookmarkContainerDto? container = action.BookmarkContainer;
			container.BookmarkGroups = container.BookmarkGroups
				?.SortGroups()
				.ToList();

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
						container.Title, container.SortOrder, container.BookmarkGroups?
							.Concat(new List<BookmarkGroupDto> { action.BookmarkGroup })
							.SortGroups()
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
						container.Title, container.SortOrder, container.BookmarkGroups?
							.Where(g => g.BookmarkGroupId != action.BookmarkGroupId)
							.ToList())
				}
			};
		}

		[ReducerMethod]
		public static RootState AddBookmark(RootState state, AddBookmarkAction action) {
			BookmarkContainerDto? container = state.CurrentContainerState.Container;

			if (container == null)
				return state;

			List<BookmarkGroupDto>? groups = container.BookmarkGroups
				?.Select(bg => {
					if (bg.BookmarkGroupId == action.Bookmark.BookmarkGroupId) {
						return new BookmarkGroupDto(bg.BookmarkGroupId, bg.Title, bg.Color,
							bg.SortOrder, bg.BookmarkContainerId, bg.Bookmarks?
								.Concat(new List<BookmarkDto> { action.Bookmark })
								.SortBookmarks()
								.ToList());
					}

					return bg;
				})
				.ToList();

			return state with {
				CurrentContainerState = state.CurrentContainerState with {
					Container = new BookmarkContainerDto(container.BookmarkContainerId,
						container.Title, container.SortOrder, groups)
				}
			};
		}

		[ReducerMethod]
		public static RootState RemoveBookmark(RootState state, RemoveBookmarkAction action) {
			BookmarkContainerDto? container = state.CurrentContainerState.Container;

			if (container == null)
				return state;

			List<BookmarkGroupDto>? groups = container.BookmarkGroups
				?.Select(bg => new BookmarkGroupDto(bg.BookmarkGroupId, bg.Title, bg.Color,
					bg.SortOrder, bg.BookmarkContainerId, bg.Bookmarks
						?.Where(b => b.BookmarkId != action.BookmarkId)
						.ToList()))
				.ToList();

			return state with {
				CurrentContainerState = state.CurrentContainerState with {
					Container = new BookmarkContainerDto(container.BookmarkContainerId,
						container.Title, container.SortOrder, groups)
				}
			};
		}
	}
}
