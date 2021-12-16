using Fluxor;

namespace Start.Client.Store.Features.DeleteBookmark {
	public class DeleteBookmarkFeature : Feature<DeleteBookmarkState> {
		public override string GetName() => "Delete Bookmark";

		protected override DeleteBookmarkState GetInitialState() {
			return new DeleteBookmarkState();
		}
	}
}
