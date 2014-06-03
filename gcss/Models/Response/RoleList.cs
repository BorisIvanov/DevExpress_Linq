using System.Data.Entity;
using DevExpress.Web.Mvc;
using gcss.Models.Data;



namespace gcss.Models.Response
{
	public class RoleList : GridModel<NCT_SYSTEM_ROLE>
	{

		public override string GridName
		{
			get { return "RoleGridView"; }
		}

		protected override object CallbackRouteValues
		{
			get { return new { Controller = "Role", Action = "FilterComboBoxPartial" }; }
		}

		public override DbSet<NCT_SYSTEM_ROLE> Entity
		{
			get { return Entities.NCT_SYSTEM_ROLE; }
		}

		public override void Init(GridViewModel gridViewModel = null)
		{
			bool isNoInit = gridViewModel == null;
			base.Init(gridViewModel);

			Action = new ActionList("Role");

			ColumnAdd("CODE", isNoInit, true);
			ColumnAdd("SRL_NAME", isNoInit, true);
			ColumnAdd("DESCRIPTION", isNoInit, true);

			FilterListInit();
		}
		
	}

}