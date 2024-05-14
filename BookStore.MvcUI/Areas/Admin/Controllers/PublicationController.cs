using AutoMapper;
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
        private readonly IMapper _mapper;

        public PublicationController(IPublicationServices publicationServices, IMapper mapper)
        {
            _publicationServices = publicationServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublicationViewModel>>> List(CancellationToken cancellationToken)
        {
            var publications = await _publicationServices.GetAll(cancellationToken);
            var publicationsViewModel = _mapper.Map<List<PublicationViewModel>>(publications);

            return View(publicationsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(CancellationToken cancellationToken, ProductActionType productActionType, int? Id = null)
        {
            if (productActionType == ProductActionType.Create && Id.HasValue)
            {
                productActionType = ProductActionType.Update;
            }

            if (productActionType == ProductActionType.Create)
            {
                return View(new UpdatePublicationViewModel() { ProductActionType = productActionType });
            }
            else if (productActionType == ProductActionType.Update)
            {
                var publication = await _publicationServices.Get(Id.GetValueOrDefault(), cancellationToken);

                if (publication is null)
                {
                    return RedirectToAction("Error", "AdminHome", new { area = "Admin" });
                }

                var updatePublicationViewModel = _mapper.Map<UpdatePublicationViewModel>(publication);
                updatePublicationViewModel.ProductActionType = productActionType;

                return View(updatePublicationViewModel);
            }
            else
            {
                return RedirectToAction("Error", "AdminHome", new { area = "Admin" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePublicationViewModel updatePublicationViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(updatePublicationViewModel);
            }

            if (updatePublicationViewModel.ProductActionType == ProductActionType.Create)
            {
                var newPublication = _mapper.Map<Publication>(updatePublicationViewModel);

                var publication = await _publicationServices.Add(newPublication, cancellationToken);

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
                var publication = _mapper.Map<Publication>(updatePublicationViewModel);

                bool result = await _publicationServices.Edit(publication, cancellationToken);

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
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var result = await _publicationServices.Delete(Id, cancellationToken);

            return Json(result);
        }
    }
}
