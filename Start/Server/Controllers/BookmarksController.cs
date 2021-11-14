using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Extensions;
using Start.Server.Models;

namespace Start.Server.Controllers {
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class BookmarksController : ControllerBase {
		private readonly IBookmarkContainerService bookmarkContainerService;
		private readonly IBookmarkGroupService bookmarkGroupService;
		private readonly IBookmarkService bookmarkService;

		private readonly string userId;

		public BookmarksController(IBookmarkContainerService bookmarkContainerService,
			IBookmarkGroupService bookmarkGroupService, IBookmarkService bookmarkService) {
			this.bookmarkContainerService = bookmarkContainerService;
			this.bookmarkGroupService = bookmarkGroupService;
			this.bookmarkService = bookmarkService;

			this.userId = this.GetAuthorizedUserId();
		}

		[HttpGet]
		public IList<BookmarkContainer> GetAllBookmarkContainers() {
			return this.bookmarkContainerService.GetUserBookmarkContainers(this.userId);
		}

		[HttpGet]
		public (BookmarkStatus, BookmarkContainer?) GetBookmarkContainer(int bookmarkContainerId) {
			return this.bookmarkContainerService.GetBookmarkContainer(this.userId, bookmarkContainerId, true, true);
		}

		[HttpGet]
		public (BookmarkStatus, Bookmark?) GetBookmark(int bookmarkId) {
			return this.bookmarkService.GetBookmark(this.userId, bookmarkId);
		}

		[HttpPost]
		public (BookmarkStatus, Bookmark?) CreateBookmark(string title, string url, string? notes,
			int bookmarkGroupId) {
			return this.bookmarkService.CreateBookmark(this.userId, title, url, notes, bookmarkGroupId);
		}
	}
}
