# Blazor Start

This is a rewrite of my [New Tab Page project](https://github.com/NeilBrommer/NewTabPage) using
[Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor).

## Structure

- Bookmark containers (tabs along the top)
- Bookmark groups
- Bookmarks

## To Do

- [x] Backend data services
- [x] API Controllers
	- [x] Bookmark containers
	- [x] Bookmark groups
	- [x] Bookmarks
- [ ] Manage bookmark containers
	- [x] Create
	- [ ] Delete
	- [ ] Edit
- [ ] Manage bookmark groups
	- [x] Create
	- [x] Delete
	- [ ] Edit
- [ ] Manage bookmarks
	- [ ] Create
	- [ ] Delete
	- [ ] Edit
- [x] Use [Refit](https://github.com/reactiveui/refit) for strongly typed API calls
- [ ] Support choosing between storing data on the server or in IndexedDB
- [ ] Look into speeding up authorization on page load
	- https://github.com/dotnet/aspnetcore/issues/31926

## Dependancies

- [Dart SASS](https://sass-lang.com/) for compiling SCSS files
- [Spectre.css](https://picturepan2.github.io/spectre/)
- [Blazored LocalStorage](https://github.com/blazored/LocalStorage)
- [Refit](https://github.com/reactiveui/refit/)
- [Fluxor](https://github.com/mrpmorris/Fluxor/)
