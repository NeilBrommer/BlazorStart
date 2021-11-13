using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Start.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
		/// <summary>The <see cref="BookmarkContainer"/>s that belong to this user</summary>
		public List<BookmarkContainer>? BookmarkContainers { get; set; }
	}
}
