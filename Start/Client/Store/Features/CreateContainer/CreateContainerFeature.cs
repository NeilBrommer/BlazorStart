using Fluxor;
using Start.Client.Store.State;

namespace Start.Client.Store.Features.CreateContainer {
	public class CreateContainerFeature : Feature<CreateContainerState> {
		public override string GetName() => "Create Container";

		protected override CreateContainerState GetInitialState() {
			return new CreateContainerState();
		}
	}
}
