using System.Collections.Generic;
using System.Linq;
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
	public class BookmarkContainersController : ControllerBase {
		private readonly IBookmarkContainerService bookmarkContainerService;

		public BookmarkContainersController(IBookmarkContainerService bookmarkContainerService) {
			this.bookmarkContainerService = bookmarkContainerService;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK,
			Type = typeof(IEnumerable<BookmarkContainerDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAllBookmarkContainers() {
			List<BookmarkContainerDto>? containers = (await this.bookmarkContainerService
				.GetUserBookmarkContainers(this.GetAuthorizedUserId()))
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
		public async Task<IActionResult> GetBookmarkContainer(int bookmarkContainerId) {
			BookmarkContainerDto? container = (await this.bookmarkContainerService
				.GetBookmarkContainer(this.GetAuthorizedUserId(), bookmarkContainerId, true, true))
				?.MapToDto();

			if (container == null)
				return NotFound();

			return Ok(container);
		}

		[HttpPost]
		[Route("Create")]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookmarkContainerDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateBookmarkContainer([FromBody] string title) {
			BookmarkContainerDto? container = (await this.bookmarkContainerService
				.CreateBookmarkContainer(this.GetAuthorizedUserId(), title))
				?.MapToDto();

			if (container == null)
				return BadRequest();

			return Created(
				Url.Action(nameof(this.GetBookmarkContainer),
					new { bookmarkContainerId = container.BookmarkContainerId }),
				container);
		}

		[HttpDelete]
		[Route("Delete/{bookmarkContainerId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteBookmarkContainer(int bookmarkContainerId) {
			bool res = await this.bookmarkContainerService
				.DeleteBookmarkContainer(this.GetAuthorizedUserId(), bookmarkContainerId);

			if (!res)
				return NotFound();

			return Ok();
		}
	}
}
