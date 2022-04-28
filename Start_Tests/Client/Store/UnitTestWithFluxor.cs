using System;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Start.Client.Store.State;
using Start.Shared.Api;
using Start_Tests.Client.MockApis;

namespace Start_Tests.Client.Store {
	public abstract class UnitTestWithFluxor {
		protected IServiceProvider ServiceProvider { get; set; }
		protected IStore Store { get; set; }
		protected IDispatcher Dispatcher { get; set; }
		protected IState<RootState> State { get; set; }
		// Add child states in the individual tests

		protected Bunit.TestContext BunitTc { get; set; }

		public UnitTestWithFluxor() {
			this.ResetState();
		}

		public void ResetState() {
			this.BunitTc = new Bunit.TestContext();

			BunitTc.Services.AddBlazoredLocalStorage();
			BunitTc.Services.AddFluxor(config => config.ScanAssemblies(typeof(RootState).Assembly));
			BunitTc.Services.AddScoped<IBookmarksApi>(sp => new MockBookmarksApi());
			BunitTc.Services.AddScoped<IBookmarkGroupsApi>(sp => new MockBookmarkGroupsApi());
			BunitTc.Services
				.AddScoped<IBookmarkContainersApi>(sp => new MockBookmarkContainersApi());

			this.Store = this.BunitTc.Services.GetRequiredService<IStore>();
			this.Dispatcher = this.BunitTc.Services.GetRequiredService<IDispatcher>();
			this.State = this.BunitTc.Services.GetRequiredService<IState<RootState>>();
			this.Store.InitializeAsync().Wait();
		}
	}
}
