using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Extensions;
using Start.Server.Models;
using Start.Shared;

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
		public IList<BookmarkContainerDto> GetAllBookmarkContainers() {
			return this.bookmarkContainerService.GetUserBookmarkContainers(this.userId)
				.Select(bc => bc.MapToDto())
				.ToList();
		}

		[HttpGet]
		public (BookmarkStatus, BookmarkContainerDto?) GetBookmarkContainer(int bookmarkContainerId) {
			(BookmarkStatus status, BookmarkContainer? container) = this.bookmarkContainerService
				.GetBookmarkContainer(this.userId, bookmarkContainerId, true, true);

			return (status, container?.MapToDto());
		}

		[HttpGet]
		public (BookmarkStatus, BookmarkDto?) GetBookmark(int bookmarkId) {
			(BookmarkStatus status, Bookmark? bookmark) = this.bookmarkService
				.GetBookmark(this.userId, bookmarkId);

			return (status, bookmark?.MapToDto());
		}

		[HttpPost]
		public (BookmarkStatus, BookmarkDto?) CreateBookmark(string title, string url, string? notes,
			int bookmarkGroupId) {
			(BookmarkStatus status, Bookmark? bookmark) = this.bookmarkService
				.CreateBookmark(this.userId, title, url, notes, bookmarkGroupId);

			return (status, bookmark?.MapToDto());
		}

		[HttpPost]
		public (BookmarkStatus, BookmarkContainerDto?) CreateBookmarkContainer(string title) {
			(BookmarkStatus status, BookmarkContainer? container) = this
				.bookmarkContainerService.CreateBookmarkContainer(this.userId, title);

			return (status, container?.MapToDto());
		}
	}
}
