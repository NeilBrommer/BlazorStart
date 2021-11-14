using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Start.Server.Extensions {
	public static class ControllerExtensions {
		/// <summary>
		/// Get the current user's ID (<see cref="ClaimTypes.NameIdentifier"/>) from claims. The
		/// caller is assumed to have checked that the user is logged in (and thus they have a user
		/// ID set).
		/// <para>If there is no user ID, an exception will be thrown.</para>
		/// </summary>
		/// <param name="controller"></param>
		public static string GetAuthorizedUserId(this ControllerBase controller) {
			string? res = controller.GetUserId();

			if (res == null)
				throw new KeyNotFoundException("The user ID could not be retrieved from claims");

			return res;
		}

		public static string? GetUserId(this ControllerBase controller) {
			return controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}
