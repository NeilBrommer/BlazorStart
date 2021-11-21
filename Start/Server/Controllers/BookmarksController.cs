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
	[Route("[controller]/[action]")]
	public class BookmarksController : ControllerBase {
		private readonly IBookmarkContainerService bookmarkContainerService;
		private readonly IBookmarkGroupService bookmarkGroupService;
		private readonly IBookmarkService bookmarkService;

		public BookmarksController(IBookmarkContainerService bookmarkContainerService,
			IBookmarkGroupService bookmarkGroupService, IBookmarkService bookmarkService) {
			this.bookmarkContainerService = bookmarkContainerService;
			this.bookmarkGroupService = bookmarkGroupService;
			this.bookmarkService = bookmarkService;
		}

		[HttpGet]
		public IList<BookmarkContainerDto> GetAllBookmarkContainers() {
			return this.bookmarkContainerService.GetUserBookmarkContainers(this.GetAuthorizedUserId())
				.Select(bc => bc.MapToDto())
				.ToList();
		}

		[HttpGet]
		[Route("{bookmarkContainerId}")]
		public BookmarkContainerDto? GetBookmarkContainer(int bookmarkContainerId) {
			return this.bookmarkContainerService
				.GetBookmarkContainer(this.GetAuthorizedUserId(), bookmarkContainerId, true, true)
				?.MapToDto();
		}

		[HttpGet]
		[Route("{bookmarkId}")]
		public BookmarkDto? GetBookmark(int bookmarkId) {
			return this.bookmarkService
				.GetBookmark(this.GetAuthorizedUserId(), bookmarkId)
				?.MapToDto();
		}

		[HttpPost]
		public BookmarkDto? CreateBookmark(string title, string url, string? notes,
			int bookmarkGroupId) {
			return this.bookmarkService
				.CreateBookmark(this.GetAuthorizedUserId(), title, url, notes, bookmarkGroupId)
				?.MapToDto();
		}

		[HttpPost]
		public BookmarkContainerDto? CreateBookmarkContainer([FromBody] string title) {
			return this.bookmarkContainerService
				.CreateBookmarkContainer(this.GetAuthorizedUserId(), title)
				?.MapToDto();
		}

		[HttpDelete]
		public bool DeleteBookmarkContainer(int bookmarkContainerId) {
			return this.bookmarkContainerService
				.DeleteBookmarkContainer(this.GetAuthorizedUserId(), bookmarkContainerId);
		}
	}
}
