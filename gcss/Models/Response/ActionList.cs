namespace gcss.Models.Response
{
	public class ActionList
	{

		public static string DefaultAddAction = "GridAdd";
		public static string DefaultUpdAction = "GridUpd";
		public static string DefaultDelAction = "GridDel";
		public static string DefaultGridAction = "Grid";
		public static string DefaultSortAction = "GridSort";
		public static string DefaultFilterAction = "GridFilter";
		public static string DefaultPagingAction = "GridPaging";

		public ActionList(string controller)
		{
			Add = new ActionItem(controller, DefaultAddAction);
			Upd = new ActionItem(controller, DefaultUpdAction);
			Del = new ActionItem(controller, DefaultDelAction);
			Grid = new ActionItem(controller, DefaultGridAction);
			Sort = new ActionItem(controller, DefaultSortAction);
			Filter = new ActionItem(controller, DefaultFilterAction);
			Paging = new ActionItem(controller, DefaultPagingAction);
		}
		public ActionItem Add { get; set; }
		public ActionItem Upd { get; set; }
		public ActionItem Del { get; set; }
		public ActionItem Grid { get; set; }
		public ActionItem Sort { get; set; }
		public ActionItem Filter { get; set; }
		public ActionItem Paging { get; set; }
	}
}