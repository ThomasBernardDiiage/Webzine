using Microsoft.AspNetCore.Mvc;
using Webzine.Entity;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.Areas.Admin.ViewModels;

namespace Webzine.WebApplication.Areas.Admin.Controllers.Artist
{

    [Area("Administration")]
    public class ArtisteController : Controller
    {
        private readonly IArtisteRepository _artisteRepository; // Repo of all artistes

        // Constructeur en injectant un IArtisteRepository
        public ArtisteController(IArtisteRepository artisteRepository)
        {
            _artisteRepository = artisteRepository;
        }

        // Index page
        public IActionResult Index()
        {
            var model = new ArtistViewModel
            {
                Artistes = _artisteRepository.FindAll()
            };

            return this.View("Index", model); // On retourne la vue index avec la liste de tous les artistes
        }

        // Lors du clic sur le bouton create
        // https://localhost:7185/administration/artiste/create
        public IActionResult Create()
        {
            return this.View("Create"); // On retourne la vue create
        }


        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(string nom, string biographie)
        {
           
            if (_artisteRepository.FindAll().Any(artisteRepo => artisteRepo.Nom == nom))
            {
                ModelState.AddModelError(string.Empty, nom + " existe déjà !");
            }
            // Si le model n'est pas valide
            if (!this.ModelState.IsValid)
            {
                return Create();
            }

            // On créé un nouvel artiste avec les données renseignées
            var artiste = new Artiste() { Nom = nom, Biographie = biographie };
            _artisteRepository.AddArtiste(artiste);
            return Index(); // redirect to index page
        }

        /// <summary>
        /// Lors du clic sur éditer
        /// https://localhost:7185/administration/titre/edit/916413
        /// </summary>
        /// <param name="idArtiste">Id de l'artiste à éditer</param>
        /// <returns></returns>
        public IActionResult Edit(int idArtiste)
        {
            var artiste = _artisteRepository.Find(idArtiste); // On récupère l'artiste dans le repo

            if (artiste == null)
            {
                return this.View("Create");
            }
            else
            {
                var model = new ArtistViewModel
                {
                    Artiste = artiste
                };
                return this.View("Create", model);
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(int idArtiste, string nom, string biographie)
        {
            if (!this.ModelState.IsValid)
            {
                return Edit(idArtiste);
            }

            var artiste = new Artiste() { IdArtiste = idArtiste, Nom = nom, Biographie = biographie, UrlSite = "" };
            _artisteRepository.UpdateArtiste(artiste);
            return Index();
        }


        /// <summary>
        /// Lors du clic sur delete
        /// </summary>
        /// <param name="idArtiste">Id de l'artiste</param>
        /// <returns>La vue delete</returns>
        public IActionResult Delete(int idArtiste)
        {
            var artiste = _artisteRepository.Find(idArtiste); // On récupère l'artiste en bdd

            var model = new ArtistViewModel
            {
                Artiste = artiste
            };

            return this.View("Delete", model); // On return la vue artiste avec l'artiste
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int idArtiste)
        {
            _artisteRepository.DeleteArtiste(new Artiste { IdArtiste = idArtiste });
            return Index();
        }
    }
}
