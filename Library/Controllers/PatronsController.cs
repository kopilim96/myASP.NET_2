using Library.Models.Patrons;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
    public class PatronsController : Controller
    {
        private readonly PatronsInterface _patrons;
        public PatronsController(PatronsInterface patrons) {
            _patrons = patrons;
        }
        public IActionResult Index()
        {
            //Getting the Collection of the Patrons
            var allPatron = _patrons.getAllPatron();

            //This method create a model from the proteties
            //of the class PatronsDetailModel and save it 
            //in a List
            var patronModel = allPatron.Select(
                p => new PatronsDetailModel { 
                    id = p.patronId,
                    firstName = p.firstName,
                    lastName = p.lastName,
                    libraryCardId = p.libraryCard.id,
                    libraryBranchName = p.LibraryBranch.name,
                    overdueFee = p.libraryCard.fees,
                }).ToList();

            //Then this method will save the list to the 
            //class PatronsIndexModel which has IEnumerable method
            //of keeping the List collection
            var model = new PatronsIndexModel()
            {
                patrons = patronModel,
            };


            //Finally, return the collection model to the Patrons view
            return View(model);
        }

        public IActionResult PatronDetails(int id) {

            //Getting the properties from Patrons
            var patron = _patrons.getPatron(id);

            var model = new PatronsDetailModel { 
                firstName = patron.firstName,
                lastName = patron.lastName,
                address = patron.address,
                libraryBranchName = patron.LibraryBranch.name,
                memberSince = patron.libraryCard.created,
                overdueFee = patron.libraryCard.fees,
                libraryCardId = patron.libraryCard.id,
                telephone = patron.phone,
                assetCheckout = _patrons.getAllCheckout(id).ToList() ?? new List<Checkouts>(),
                checkoutHistory = _patrons.getAllCheckOutHistory(id),
                hold = _patrons.getAllHold(id),
            };

            return View(model);

        }
    }
}