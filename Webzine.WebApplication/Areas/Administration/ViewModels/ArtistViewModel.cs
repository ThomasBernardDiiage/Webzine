using Webzine.Entity;

namespace Webzine.WebApplication.Areas.Admin.ViewModels
{
    public class ArtistViewModel
    {
        public IEnumerable<Artiste> Artistes { get; set; }
        public Artiste Artiste { get; set; }
    }
}

