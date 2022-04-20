using Microsoft.VisualStudio.TestTools.UnitTesting;

using Start.Client.Store.Features.ContainersList;

namespace Start_Tests.Client.Store {
	[TestClass]
	public class ContainerListTests : UnitTestWithFluxor {
		public TestContext TestContext { get; set; }
		// Only RootState is needed, so no need to get child objects

		[TestMethod]
		public void OnFetchContainersList() {
			base.Store.Dispatch(new FetchContainerListAction());

			Assert.IsTrue(base.State.Value.ContainerListState.IsLoadingContainersList);
			Assert.AreEqual(0, this.State.Value.ContainerListState.Containers.Count);
		}

		[TestInitialize]
		public void InitializeState() {
			TestContext.WriteLine("Resetting Fluxor state");
			base.ResetState();
		}
	}
}
