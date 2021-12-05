using Start.Client.Store.State;

namespace Start.Client.Store.Features.CreateGroup {
	public record CreateGroupState : RootState {
		public bool ShowCreateGroupForm { get; init; }
		public int ContainerId { get; init; }
		public string ContainerTitle { get; init; }
		public bool IsLoadingCreateGroup { get; init; }
		public string? CreateGroupErrorMessage { get; init; }

		public CreateGroupState() {
			this.ContainerTitle = "";
		}

		public CreateGroupState(ContainerListState containerList,
			CurrentContainerState currentContainer, bool showCreateGroupForm, string containerTitle,
			bool isLoadingCreateGroup, string? createGroupErrorMessage)
			: base(containerList, currentContainer) {
			this.ShowCreateGroupForm = showCreateGroupForm;
			this.ContainerTitle = containerTitle;
			this.IsLoadingCreateGroup = isLoadingCreateGroup;
			this.CreateGroupErrorMessage = createGroupErrorMessage;
		}
	}
}
