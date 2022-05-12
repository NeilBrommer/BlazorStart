using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Extensions;
using Start.Server.Models;

namespace Start.Server.Data.Services {
	public class BookmarkGroupService : IBookmarkGroupService {
		private readonly ApplicationDbContext db;

		public BookmarkGroupService(ApplicationDbContext dbContext) {
			this.db = dbContext;
		}

		public async Task<BookmarkGroup?> GetBookmarkGroup(string userId, int bookmarkGroupId,
			bool includeBookmarks = false) {
			BookmarkGroup? group = await db.BookmarkGroups
				.Where(bg => bg.BookmarkGroupId == bookmarkGroupId)
				.If(includeBookmarks, q => q.Include(bg => bg.Bookmarks))
				.SingleOrDefaultAsync();

			if (!BookmarkOwnershipTools.IsBookmarkGroupOwner(db, userId, bookmarkGroupId))
				return null;

			return group;
		}

		public async Task<IList<BookmarkGroup>> GetUserBookmarkGroups(string userId,
			bool includeBookmarkGroups = false) {
			return await this.db.BookmarkGroups
				.Where(bg => bg.BookmarkContainer!.ApplicationUserId == userId)
				.If(includeBookmarkGroups, q => q.Include(bg => bg.Bookmarks))
				.ToListAsync();
		}

		public async Task<BookmarkGroup?> CreateBookmarkGroup(string userId, string title,
			string color, int sortOrder, int bookmarkContainerId) {
			if (!BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainerId))
				return null;

			BookmarkGroup newBookmarkGroup = new(title, color, sortOrder, bookmarkContainerId);
			await this.db.BookmarkGroups.AddAsync(newBookmarkGroup);
			await this.db.SaveChangesAsync();

			return newBookmarkGroup;
		}

		public async Task<BookmarkGroup?> UpdateBookmarkGroup(string userId,
			BookmarkGroup bookmarkGroup) {
			BookmarkGroup? existingGroup = await this.db.BookmarkGroups
				.SingleOrDefaultAsync(bg => bg.BookmarkGroupId == bookmarkGroup.BookmarkGroupId);

			if (existingGroup == null)
				return null;

			if (!BookmarkOwnershipTools
				.IsBookmarkGroupOwner(this.db, userId, bookmarkGroup.BookmarkGroupId))
				return null;

			// If it's been moved to a new container
			if (existingGroup.BookmarkContainerId != bookmarkGroup.BookmarkContainerId
				&& !BookmarkOwnershipTools
					.IsBookmarkContainerOwner(this.db, userId, bookmarkGroup.BookmarkContainerId))
				return null;

			if (existingGroup.BookmarkContainerId != bookmarkGroup.BookmarkContainerId) {
				// It's been moved to a different container - shuffle the sort order around

				List<BookmarkGroup>? oldContainerGroups = await db.BookmarkGroups
					.Where(bg => bg.BookmarkContainerId == existingGroup.BookmarkContainerId)
					.Where(bg => bg.SortOrder > existingGroup.SortOrder)
					.ToListAsync();

				oldContainerGroups.ForEach(bg => bg.SortOrder -= 1);

				List<BookmarkGroup>? newContainerGroups = await db.BookmarkGroups
					.Where(bg => bg.BookmarkContainerId == bookmarkGroup.BookmarkContainerId)
					.Where(bg => bg.SortOrder >= bookmarkGroup.SortOrder)
					.ToListAsync();

				newContainerGroups.ForEach(bg => bg.SortOrder += 1);
			}
			else if (existingGroup.SortOrder != bookmarkGroup.SortOrder) {
				// The group was moved within the same container

				List<BookmarkGroup>? containerGroups = await db.BookmarkGroups
					.Where(bg => bg.BookmarkContainerId == bookmarkGroup.BookmarkContainerId)
					.Where(bg => bg.BookmarkGroupId != bookmarkGroup.BookmarkGroupId)
					.ToListAsync();

				containerGroups
					.Where(bg => bg.SortOrder > existingGroup.SortOrder)
					.ToList()
					.ForEach(bg => bg.SortOrder -= 1);

				containerGroups
					.Where(bg => bg.SortOrder > bookmarkGroup.SortOrder)
					.ToList()
					.ForEach(bg => bg.SortOrder += 1);
			}

			this.db.Entry(bookmarkGroup).State = EntityState.Modified;
			await this.db.SaveChangesAsync();

			return bookmarkGroup;
		}

		public async Task<bool> DeleteBookmarkGroup(string userId, int bookmarkGroupId) {
			BookmarkGroup? bookmarkGroup = await this.db.BookmarkGroups
				.SingleOrDefaultAsync(bg => bg.BookmarkGroupId == bookmarkGroupId);

			if (bookmarkGroup == null)
				return false;

			if (!BookmarkOwnershipTools.IsBookmarkGroupOwner(this.db, userId, bookmarkGroupId))
				return false;

			this.db.BookmarkGroups.Remove(bookmarkGroup);
			await this.db.SaveChangesAsync();

			return true;
		}
	}
}
