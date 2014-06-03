using System.Data.Entity;
using System.Linq;
using DevExpress.Web.Mvc;
using gcss.Models.Data;



namespace gcss.Models.Response
{
	public class UserGridModel : GridModel<NCT_USER>
	{

		public override string GridName
		{
			get { return "UserGridView"; }
		}

		protected override object CallbackRouteValues
		{
			get { return new { Controller = "User", Action = "FilterComboBoxPartial" }; }
		}

		public override DbSet<NCT_USER> Entity
		{
			get { return Entities.NCT_USER; }
		}

		public override void Init(GridViewModel gridViewModel = null)
		{
			bool isNoInit = gridViewModel == null;
			base.Init(gridViewModel);

			Action = new ActionList("User");
			
			ColumnAdd("USER_NAME", isNoInit, true);
			ColumnAdd("EMAIL", isNoInit, true);
			ColumnAdd("LOGIN", isNoInit, true);
			ColumnAdd("IS_ACTIVE", isNoInit);

			FilterListInit();
		}

	}

}