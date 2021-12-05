using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Extensions;
using Start.Shared;

namespace Start.Server.Controllers {
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class BookmarksController : ControllerBase {
		private readonly IBookmarkService bookmarkService;

		public BookmarksController(IBookmarkService bookmarkService) {
			this.bookmarkService = bookmarkService;
		}

		[HttpGet]
		[Route("{bookmarkId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookmarkDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetBookmark(int bookmarkId) {
			BookmarkDto? bookmark = (await this.bookmarkService
				.GetBookmark(this.GetAuthorizedUserId(), bookmarkId))
				?.MapToDto();

			if (bookmark == null)
				return NotFound();

			return Ok(bookmark);
		}

		[HttpPost]
		[Route("Create")]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookmarkDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateBookmark(string title, string url, string? notes,
			int bookmarkGroupId) {
			BookmarkDto? bookmark = (await this.bookmarkService
				.CreateBookmark(this.GetAuthorizedUserId(), title, url, notes, bookmarkGroupId))
				?.MapToDto();

			if (bookmark == null)
				return BadRequest();

			return Created(
				Url.Action(nameof(this.GetBookmark),new { bookmarkId = bookmark.BookmarkId }),
				bookmark);
		}

		[HttpDelete]
		[Route("Delete/{bookmarkId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteBookmark(int bookmarkId) {
			var res = await this.bookmarkService
				.DeleteBookmark(this.GetAuthorizedUserId(), bookmarkId);

			if (!res)
				return NotFound();

			return Ok();
		}
	}
}
