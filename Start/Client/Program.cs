using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Refit;
using Start.Shared.Api;
using Fluxor;

namespace Start.Client {
	public class Program {
		public static async Task Main(string[] args) {
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddHttpClient("Start.ServerAPI",
					client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the
			// server project
			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
					.CreateClient("Start.ServerAPI"));

			// Blazor will throw an error if a relative URI is used, so we have to get the base
			// address for building the API paths
			Uri baseUri = new(builder.HostEnvironment.BaseAddress);

			builder.Services.AddRefitClient<IBookmarkContainersApi>()
				.ConfigureHttpClient(c => {
					c.BaseAddress = new Uri(baseUri, "BookmarkContainers");
				})
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			builder.Services.AddRefitClient<IBookmarkGroupsApi>()
				.ConfigureHttpClient(c => { c.BaseAddress = new Uri(baseUri, "BookmarkGroups"); })
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			builder.Services.AddRefitClient<IBookmarksApi>()
				.ConfigureHttpClient(c => { c.BaseAddress = new Uri(baseUri, "Bookmarks"); })
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			builder.Services.AddApiAuthorization();
			builder.Services.AddBlazoredLocalStorage();

			builder.Services.AddFluxor(opt => {
				opt.ScanAssemblies(typeof(Program).Assembly);
#if DEBUG
				Console.WriteLine("Enabling Redux dev tools");
				opt.UseReduxDevTools();
#endif
			});

			await builder.Build().RunAsync();
		}
	}
}
