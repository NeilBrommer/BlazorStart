using Fluxor;

namespace Start.Client.Store.Features.DeleteGroup {
	public class DeleteGroupFeature : Feature<DeleteGroupState> {
		public override string GetName() => "Delete Group";

		protected override DeleteGroupState GetInitialState() {
			return new DeleteGroupState();
		}
	}
}
