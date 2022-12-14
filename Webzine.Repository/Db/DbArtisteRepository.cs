using Microsoft.EntityFrameworkCore;
using Webzine.EntitiesContext;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository.Db
{
    public class DbArtisteRepository : IArtisteRepository
    {
        private readonly WebzineDbContext _webzineDbContext;

        public DbArtisteRepository(WebzineDbContext context)
        {
            _webzineDbContext = context;
        }

        public void AddArtiste(Artiste artiste)
        {
            artiste.UrlSite = "";
            _webzineDbContext.Add(artiste);
            _webzineDbContext.SaveChanges();
        }

        public void DeleteArtiste(Artiste artiste)
        {
            _webzineDbContext.Remove(artiste);
            _webzineDbContext.SaveChanges();
        }

        public Artiste Find(int id)
        {
            return _webzineDbContext.Artistes.Include(a => a.Titres).Where(a => a.IdArtiste == id).FirstOrDefault();
        }

        public IEnumerable<Artiste> FindAll()
        {
            return _webzineDbContext.Artistes
                .Include(a => a.Titres)
                .OrderBy(a => a.Nom)
                .ToList();
        }

        public void UpdateArtiste(Artiste artiste)
        {
            _webzineDbContext.Update(artiste);
            _webzineDbContext.SaveChanges();
        }
    }
}
