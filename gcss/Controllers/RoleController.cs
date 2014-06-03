using gcss.Models.Data;
using gcss.Models.Response;


namespace gcss.Controllers
{

	public class RoleController : GridController<RoleList, NCT_SYSTEM_ROLE>
  {
		protected override void UpdateItemModel(NCT_SYSTEM_ROLE item)
		{
			UpdateModel(item);
		}

	}

}