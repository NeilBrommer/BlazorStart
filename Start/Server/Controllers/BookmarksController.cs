using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Extensions;
using Start.Shared;

namespace Start.Server.Controllers {
	[Authorize]
	[ApiController]
	[Route("[controller]/[action]")]
	public class BookmarksController : ControllerBase {
		private readonly IBookmarkGroupService bookmarkGroupService;
		private readonly IBookmarkService bookmarkService;

		public BookmarksController(IBookmarkGroupService bookmarkGroupService,
			IBookmarkService bookmarkService) {
			this.bookmarkGroupService = bookmarkGroupService;
			this.bookmarkService = bookmarkService;
		}

		[HttpGet]
		[Route("{bookmarkId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookmarkDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetBookmark(int bookmarkId) {
			BookmarkDto? bookmark = this.bookmarkService
				.GetBookmark(this.GetAuthorizedUserId(), bookmarkId)
				?.MapToDto();

			if (bookmark == null)
				return NotFound();

			return Ok(bookmark);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookmarkDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateBookmark(string title, string url, string? notes,
			int bookmarkGroupId) {
			BookmarkDto? bookmark =  this.bookmarkService
				.CreateBookmark(this.GetAuthorizedUserId(), title, url, notes, bookmarkGroupId)
				?.MapToDto();

			if (bookmark == null)
				return BadRequest();

			return Created(
				Url.Action(nameof(this.GetBookmark),new { bookmarkId = bookmark.BookmarkId }),
				bookmark);
		}
	}
}
