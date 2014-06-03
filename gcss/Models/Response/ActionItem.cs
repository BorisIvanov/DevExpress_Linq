namespace gcss.Models.Response
{

	public class ActionItem
	{
		public ActionItem(string controller)
		{
			Controller = controller;
		}
		public ActionItem(string controller, string action)
		{
			Controller = controller;
			Action = action;
		}
		public string Controller { get; set; }
		public string Action { get; set; }
	
	}

}