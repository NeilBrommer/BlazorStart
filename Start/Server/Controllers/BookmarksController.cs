using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Extensions;
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
		public BookmarkContainerDto? GetBookmarkContainer(int bookmarkContainerId) {
			return this.bookmarkContainerService
				.GetBookmarkContainer(this.userId, bookmarkContainerId, true, true)
				?.MapToDto();
		}

		[HttpGet]
		public BookmarkDto? GetBookmark(int bookmarkId) {
			return this.bookmarkService
				.GetBookmark(this.userId, bookmarkId)
				?.MapToDto();
		}

		[HttpPost]
		public BookmarkDto? CreateBookmark(string title, string url, string? notes,
			int bookmarkGroupId) {
			return this.bookmarkService
				.CreateBookmark(this.userId, title, url, notes, bookmarkGroupId)
				?.MapToDto();
		}

		[HttpPost]
		public BookmarkContainerDto? CreateBookmarkContainer(string title) {
			return this.bookmarkContainerService
				.CreateBookmarkContainer(this.userId, title)
				?.MapToDto();
		}
	}
}
