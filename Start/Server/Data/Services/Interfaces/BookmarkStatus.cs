using System;
namespace Start.Server.Data.Services.Interfaces {
	public enum BookmarkStatus {
		OK = 1,
		BookmarkDoesNotExist = 2,
		OwnerDoesNotMatch = 3,
		UserDoesNotExist = 4
	}
}
