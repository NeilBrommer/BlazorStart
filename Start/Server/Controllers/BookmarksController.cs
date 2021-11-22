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
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookmarkContainerDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetAllBookmarkContainers() {
			List<BookmarkContainerDto>? containers = this.bookmarkContainerService
				.GetUserBookmarkContainers(this.GetAuthorizedUserId())
				.Select(bc => bc.MapToDto())
				.ToList();

			if (containers == null)
				return NotFound();

			return Ok(containers);
		}

		[HttpGet]
		[Route("{bookmarkContainerId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookmarkContainerDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetBookmarkContainer(int bookmarkContainerId) {
			BookmarkContainerDto? container = this.bookmarkContainerService
				.GetBookmarkContainer(this.GetAuthorizedUserId(), bookmarkContainerId, true, true)
				?.MapToDto();

			if (container == null)
				return NotFound();

			return Ok(container);
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

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookmarkContainerDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateBookmarkContainer([FromBody] string title) {
			BookmarkContainerDto? container = this.bookmarkContainerService
				.CreateBookmarkContainer(this.GetAuthorizedUserId(), title)
				?.MapToDto();

			if (container == null)
				return BadRequest();

			return Created(
				Url.Action(nameof(this.GetBookmarkContainer),
					new { bookmarkContainerId = container.BookmarkContainerId }),
				container);
		}

		[HttpDelete]
		[Route("{bookmarkContainerId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteBookmarkContainer(int bookmarkContainerId) {
			bool res = this.bookmarkContainerService
				.DeleteBookmarkContainer(this.GetAuthorizedUserId(), bookmarkContainerId);

			if (!res)
				return NotFound();

			return Ok();
		}
	}
}
