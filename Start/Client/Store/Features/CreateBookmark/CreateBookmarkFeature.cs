using Fluxor;

namespace Start.Client.Store.Features.CreateBookmark {
	public class CreateBookmarkFeature : Feature<CreateBookmarkState> {
		public override string GetName() => "Create Bookmark";

		protected override CreateBookmarkState GetInitialState() {
			return new CreateBookmarkState();
		}
	}
}
