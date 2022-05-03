using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Start.Server.Data.Services;
using Start.Server.Models;

namespace Start_Tests.Server {
	[TestClass]
	public class BookmarkServiceTests : UnitTestWithDb {
		public TestContext TestContext { get; set; }
		public BookmarkService BookmarkService { get; set; }

		public BookmarkServiceTests() {
			this.BookmarkService = new BookmarkService(_db);
		}

		[TestMethod]
		public override void TestDatabaseOK() {
			base.TestDatabaseOK();
		}

		#region CreateBookmark

		[TestMethod]
		public async Task CreateBookmark_Valid() {
			int initialCount = _db.Bookmarks.Count();

			await this.BookmarkService.CreateBookmark(base.TestUserId,
				"Bookmark Service Test Title", "http://example.com", null, 1,
				this.TestBookmarkGroup.BookmarkGroupId);

			int updatedCount = _db.Bookmarks.Count();
			Assert.AreEqual(initialCount + 1, updatedCount);
		}

		[TestMethod]
		[ExpectedException(typeof(DbUpdateException))]
		public async Task CreateBookmark_InvalidTitle() {
			await this.BookmarkService.CreateBookmark(base.TestUserId,
				null, "http://example.com", null, 1,
				this.TestBookmarkGroup.BookmarkGroupId);
		}

		[TestMethod]
		[ExpectedException(typeof(DbUpdateException))]
		public async Task CreateBookmark_InvalidUrl() {
			await this.BookmarkService.CreateBookmark(base.TestUserId,
				"Bookmark Service Test Title", null, null, 1,
				this.TestBookmarkGroup.BookmarkGroupId);
		}

		#endregion

		#region GetBookmark

		[TestMethod]
		public async Task GetBookmark_CorrectUser() {
			Bookmark bookmark = await this.BookmarkService
				.GetBookmark(base.TestUserId,base.TestBookmark.BookmarkId);

			Assert.IsNotNull(bookmark);
			Assert.AreEqual(bookmark.BookmarkId, base.TestBookmark.BookmarkId);
			Assert.AreEqual(bookmark.Url, base.TestBookmark.Url);
		}

		[TestMethod]
		public async Task GetBookmark_WrongUser() {
			Bookmark bookmark = await this.BookmarkService
				.GetBookmark(base.InvalidUserId, base.TestBookmark.BookmarkId);

			// Should return null if the user doesn't own the bookmark
			Assert.IsNull(bookmark);
		}

		[TestMethod]
		public async Task GetBookmark_WrongId() {
			// Ensure that we use an invalid ID by going past the highest ID value

			int maxBookmarkId = _db.Bookmarks.Max(b => b.BookmarkId);

			Bookmark bookmark = await this.BookmarkService
				.GetBookmark(base.TestUserId, maxBookmarkId + 1);

			Assert.IsNull(bookmark);
		}

		#endregion

		#region UpdateBookmark

		[TestMethod]
		public async Task UpdateBookmark_ValidTitle() {
			string testTitleUpdate = "Update bookkmark test title";

			base.TestBookmark.Title = testTitleUpdate;
			Bookmark updatedBookmark = await this.BookmarkService
				.UpdateBookmark(base.TestUserId, base.TestBookmark);

			Assert.IsNotNull(updatedBookmark);
			Assert.AreEqual(updatedBookmark.Title, testTitleUpdate);

			Bookmark fromDb = _db.Bookmarks.Single(b => b.BookmarkId == TestBookmark.BookmarkId);

			Assert.AreEqual(fromDb.Title, testTitleUpdate);
		}

		#endregion

		[TestInitialize]
		public void ResetDatabase() {
			TestContext.WriteLine("Reseting test DB for the next test");
			base.ResetAndFillDb();
		}
	}
}
