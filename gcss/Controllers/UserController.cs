using gcss.Models.Data;
using gcss.Models.Response;


namespace gcss.Controllers
{

	public class UserController : GridController<UserGridModel, NCT_USER>
  {
		protected override void UpdateItemModel(NCT_USER item)
		{
			UpdateModel(item);
		}

	}

}