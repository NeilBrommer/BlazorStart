using Fluxor;

namespace Start.Client.Store.Features.CreateGroup {
	public class CreateGroupFeature : Feature<CreateGroupState> {
		public override string GetName() => "Create Group";

		protected override CreateGroupState GetInitialState() {
			return new CreateGroupState();
		}
	}
}
