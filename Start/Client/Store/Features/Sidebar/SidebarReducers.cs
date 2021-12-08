using Fluxor;
using Start.Client.Store.State;

namespace Start.Client.Store.Features.Sidebar {
	public static class SidebarReducers {
		[ReducerMethod(typeof(ShowSidebarAction))]
		public static RootState ShowSidebar(RootState state) {
			return state with {
				ShowSidebar = true
			};
		}

		[ReducerMethod(typeof(HideSidebarAction))]
		public static RootState HideSidebar(RootState state) {
			return state with {
				ShowSidebar = false
			};
		}

		[ReducerMethod(typeof(ToggleEditModeAction))]
		public static RootState ToggleEditMode(RootState state) {
			return state with {
				EditMode = !state.EditMode
			};
		}
	}
}
