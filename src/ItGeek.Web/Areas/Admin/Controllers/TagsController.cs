using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Enitities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    public class TagsController : BaseController
    {

        private readonly UnitOfWork _uow;
        public TagsController(UnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }
    }
}

//public TagsController(UnitOfWork uow, TagRepository tagRepository) : base(uow, tagRepository)
//    {
//}