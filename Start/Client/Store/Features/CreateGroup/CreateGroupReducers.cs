using System;
using Fluxor;

namespace Start.Client.Store.Features.CreateGroup {
	public static class CreateGroupReducers {
		[ReducerMethod]
		public static CreateGroupState ShowCreateGroupForm(CreateGroupState state,
			ShowCreateGroupFormAction action) {
			return state with {
				ShowCreateGroupForm = true,
				ContainerId = action.ContainerId,
				ContainerTitle = action.ContainerTitle,
				IsLoadingCreateGroup = false,
				CreateGroupErrorMessage = null
			};
		}

		[ReducerMethod(typeof(HideCreateGroupFormAction))]
		public static CreateGroupState HideCreateContainerForm(CreateGroupState state) {
			return state with {
				ShowCreateGroupForm = false,
				IsLoadingCreateGroup = false,
				CreateGroupErrorMessage = null,
				ContainerId = 0,
				ContainerTitle = ""
			};
		}

		[ReducerMethod(typeof(FetchCreateGroupAction))]
		public static CreateGroupState FetchCreateGroup(CreateGroupState state) {
			return state with {
				IsLoadingCreateGroup = true,
				CreateGroupErrorMessage = null
			};
		}

		[ReducerMethod(typeof(RecievedCreateGroupAction))]
		public static CreateGroupState RecievedCreateGroup(CreateGroupState state) {
			return state with {
				IsLoadingCreateGroup = false,
				CreateGroupErrorMessage = null
			};
		}

		[ReducerMethod]
		public static CreateGroupState ErrorFetchingCreateGroup(CreateGroupState state,
			ErrorFetchingCreateGroupAction action) {
			return state with {
				CreateGroupErrorMessage = action.ErrorMessage,
				IsLoadingCreateGroup = false
			};
		}
	}
}
