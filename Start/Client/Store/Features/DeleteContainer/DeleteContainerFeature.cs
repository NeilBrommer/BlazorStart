using Fluxor;

namespace Start.Client.Store.Features.DeleteContainer {
	public class DeleteContainerFeature : Feature<DeleteContainerState> {
		public override string GetName() => "Delete Container";

		protected override DeleteContainerState GetInitialState() {
			return new DeleteContainerState();
		}
	}
}
