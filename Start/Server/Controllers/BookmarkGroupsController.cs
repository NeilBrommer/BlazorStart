using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Models;
using Start.Server.Extensions;
using Start.Shared;

namespace Start.Server.Controllers {
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class BookmarkGroupsController : ControllerBase {
		private readonly IBookmarkGroupService bookmarkGroupService;

		public BookmarkGroupsController(IBookmarkGroupService bookmarkGroupService) {
			this.bookmarkGroupService = bookmarkGroupService;
		}

		[HttpGet]
		[Route("{bookmarkGroupId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookmarkGroupDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetBookmarkGroup(int bookmarkGroupId) {
			BookmarkGroup? group = await this.bookmarkGroupService
				.GetBookmarkGroup(this.GetAuthorizedUserId(), bookmarkGroupId);

			if (group == null)
				return NotFound();

			return Ok(group.MapToDto());
		}

		[HttpPost]
		[Route("Create")]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookmarkGroupDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateBookmarkGroup(string title, string color,
			int sortOrder, int bookmarkContainerId) {
			BookmarkGroup? newGroup = await this.bookmarkGroupService
				.CreateBookmarkGroup(this.GetAuthorizedUserId(), title, color, sortOrder, bookmarkContainerId);

			if (newGroup == null)
				return BadRequest();

			return Created(
				Url.Action(nameof(GetBookmarkGroup),
					new { bookmarkGroupId = newGroup.BookmarkGroupId }),
				newGroup.MapToDto());
		}

		[HttpDelete]
		[Route("Delete/{bookmarkGroupId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteBookmark(int bookmarkGroupId) {
			bool res = await this.bookmarkGroupService
				.DeleteBookmarkGroup(this.GetAuthorizedUserId(), bookmarkGroupId);

			if (!res)
				return NotFound();

			return Ok();
		}
	}
}
