using Start.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Options;

namespace Start.Server.Data {
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser> {
		public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
		public DbSet<BookmarkGroup> BookmarkGroups => Set<BookmarkGroup>();
		public DbSet<BookmarkContainer> BookmarkContainers => Set<BookmarkContainer>();

		public ApplicationDbContext(DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions)
				: base(options, operationalStoreOptions) {

		}
	}
}
