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

		[HttpGet]
		public (BookmarkStatus, Bookmark?) GetBookmark(int bookmarkId) {
			return this.bookmarkService.GetBookmark(this.GetUserId(), bookmarkId);
		}

		[HttpPost]
		public (BookmarkStatus, Bookmark?) CreateBookmark(string title, string url, string? notes,
			int bookmarkGroupId) {
			return this.bookmarkService.CreateBookmark(this.GetUserId(), title, url, notes,
				bookmarkGroupId);
		}

		private string GetUserId() {
			return this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
		}
	}
}
