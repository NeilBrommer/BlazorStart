using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Models;

namespace Start.Server.Controllers {
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class BookmarksController : ControllerBase {
		private readonly IBookmarkService bookmarkService;

		public BookmarksController(IBookmarkService bookmarkService) {
			this.bookmarkService = bookmarkService;
		}

		/*
		[HttpGet]
		public IList<BookmarkContainer> GetAllBookmarkContainers() {

		}

		[HttpGet]
		public BookmarkContainer GetBookmarkContainer(int bookmarkContainerId) {

		}
		*/

		[HttpPost]
		public Bookmark CreateBookmark(string title, string url, string? notes,
			int bookmarkGroupId) {
			string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

			return bookmarkService.CreateBookmark(userId, title, url, notes, bookmarkGroupId);
		}


	}
}
