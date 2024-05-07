using BookStore.Common.Enums;
using BookStore.Entities.Product;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.MvcUI.Areas.Admin.Controllers
{
    public class PublicationController : BaseMvcController
    {
        private readonly IPublicationServices _publicationServices;

        public PublicationController(IPublicationServices publicationServices)
        {
            _publicationServices = publicationServices;
        }

        [NonAction]
        private async Task<List<PublicationViewModel>> GeneratePublicationViewModelList()
        {
            var publications = await _publicationServices.GetAll();

            List<PublicationViewModel> list = new List<PublicationViewModel>();

            foreach (var publication in publications)
            {
                var publicationViewModel = new PublicationViewModel()
                {
                    Id = publication.Id,
                    Name = publication.Name,
                    Address = publication.Address,
                    WebSiteUrl = publication.WebSiteUrl
                };

                list.Add(publicationViewModel);
            }

            return list;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublicationViewModel>>> List()
        {
            var list = await GeneratePublicationViewModelList();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Update(ProductActionType productActionType, int? Id = null)
        {
            UpdatePublicationViewModel updatePublicationViewModel = new UpdatePublicationViewModel();

            updatePublicationViewModel.ProductActionType = productActionType;

            if (productActionType == ProductActionType.Create && Id.HasValue)
            {
                productActionType = ProductActionType.Update;
                updatePublicationViewModel.ProductActionType = ProductActionType.Update;
            }

            if (productActionType == ProductActionType.Create)
            {
                return View(updatePublicationViewModel);
            }
            else if (productActionType == ProductActionType.Update)
            {
                var publication = await _publicationServices.Get(Id.GetValueOrDefault());

                if (publication is null)
                {
                    return RedirectToAction("Error", "AdminHome", new { area = "Admin" });
                }

                updatePublicationViewModel.Id = publication.Id;
                updatePublicationViewModel.Name = publication.Name;
                updatePublicationViewModel.Address = publication.Address;
                updatePublicationViewModel.WebSiteUrl = publication.WebSiteUrl;
            }

            return View(updatePublicationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePublicationViewModel updatePublicationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(updatePublicationViewModel);
            }

            if (updatePublicationViewModel.ProductActionType == ProductActionType.Create)
            {
                var publication = await _publicationServices.Add(new Publication
                {
                    Name = updatePublicationViewModel.Name,
                    Address = updatePublicationViewModel.Address,
                    WebSiteUrl = updatePublicationViewModel.WebSiteUrl
                });

                if (publication.Id > 0)
                {
                    return RedirectToAction("List", "Publication", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("AddPublicationError", "خطایی در ثبت انتشارات رخ داده است.");

                    return View(updatePublicationViewModel);
                }
            }
            else if (updatePublicationViewModel.ProductActionType == ProductActionType.Update)
            {
                var publication = new Publication()
                {
                    Id = updatePublicationViewModel.Id,
                    Name = updatePublicationViewModel.Name,
                    Address = updatePublicationViewModel.Address,
                    WebSiteUrl = updatePublicationViewModel.WebSiteUrl
                };

                bool result = await _publicationServices.Edit(publication);

                if (result)
                {
                    return RedirectToAction("List", "Publication", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("UpdatePublicationError", "خطایی در ویرایش انتشارات رخ داده است.");

                    return View(updatePublicationViewModel);
                }
            }

            return RedirectToAction("List", "Publication", new { area = "Admin" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _publicationServices.Delete(Id);

            return Json(result);
        }
    }
}
