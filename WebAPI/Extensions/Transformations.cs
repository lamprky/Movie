using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Database;
using WebAPI.Models.Service;

namespace WebAPI.Extensions
{
    public static class Transformations
    {
        private static Guid entityType_Contributor = Guid.Parse("9D0BD902-C389-4A3E-BFB1-FC43FFDD0DEE");
        private static Guid entityType_Movie = Guid.Parse("4B191233-7411-48BE-851D-FA5E1C891DDB");
        private static Guid entityType_Genre = Guid.Parse("FC9945F4-D513-4818-AD87-B854BCADE8E6");
        private static Guid entityType_ContributorType = Guid.Parse("C242262F-611C-48AF-99CB-9B93496F8687");

        public static ContributorType ToContributorType(this ContributorTypeViewModel model, ContributorType cp = null)
        {
            Guid id = (model.ID == null) ? Guid.NewGuid() : model.ID.Value;

            if (cp == null)
                cp = new ContributorType();

            cp.ID = id;
            cp.Translations = model.Details.Select(x => x.ToTranslation(id, entityType_ContributorType, cp.Translations?.SingleOrDefault(y => y.ID == x.ID))).ToList();

            return cp;
        }

        public static Genre ToGenre(this GenreViewModel model, Genre g = null)
        {
            Guid id = (model.ID == null) ? Guid.NewGuid() : model.ID.Value;

            if (g == null)
                g = new Genre();

            g.ID = id;
            g.Translations = model.Details.Select(x => x.ToTranslation(id, entityType_Genre, g.Translations?.SingleOrDefault(y => y.ID == x.ID))).ToList();

            return g;
        }

        public static Contributor ToContributor(this ContributorViewModel model, Contributor c = null)
        {
            Guid id = (model.ID == null) ? Guid.NewGuid() : model.ID.Value;

            if (c == null)
                c = new Contributor();

            c.ID = id;
            c.Translations = model.Details.Select(x => x.ToTranslation(id, entityType_Contributor, c.Translations?.SingleOrDefault(y => y.ID == x.ID))).ToList();
            c.ContributorContributorTypes = model.ContributorTypes.Distinct().Select(x => x.ToContributorContributorType(id)).ToList();

            return c;
        }

        public static Translation ToTranslation(this DetailsViewModel model, Guid entityId, Guid entityType, Translation tr = null)
        {
            Guid id = (model.ID == null) ? Guid.NewGuid() : model.ID.Value;

            if (tr == null)
                tr = new Translation();

            tr.ID = id;
            tr.LanguageId = model.LanguageId;
            tr.Title = model.Title;
            tr.Name = model.Name;
            tr.Description = model.Description;

            return tr;
        }

        public static Movie ToMovie(this MovieViewModel model, Movie m = null)
        {
            Guid id = (model.ID == null) ? Guid.NewGuid() : model.ID.Value;

            if (m == null)
                m = new Movie();

            m.ID = id;
            m.Translations = model.Details.Select(x => x.ToTranslation(id, entityType_Movie, m.Translations?.SingleOrDefault(y => y.ID == x.ID))).ToList();
            m.MovieContributors = model.Contributors.Distinct().Select(x => x.ToMovieContributors(id)).ToList();
            m.MovieGenres = model.Genres.Distinct().Select(x => x.ToMovieGenres(id)).ToList();

            return m;
        }

        public static ContributorContributorTypes ToContributorContributorType(this Guid contributorTypeId, Guid contributorId, ContributorContributorTypes cct = null)
        {
            if (cct == null)
            {
                cct = new ContributorContributorTypes();
                cct.ID = Guid.NewGuid();
            }

            cct.ContributorID = contributorId;
            cct.ContributorTypeID = contributorTypeId;

            return cct;
        }

        public static MovieContributors ToMovieContributors(this Guid contributorId, Guid movieId, MovieContributors mc = null)
        {
            if (mc == null)
            {
                mc = new MovieContributors();
                mc.ID = Guid.NewGuid();
            }

            mc.ContributorID = contributorId;
            mc.MovieID = movieId;

            return mc;
        }

        public static MovieGenres ToMovieGenres(this Guid genreId, Guid movieId, MovieGenres mg = null)
        {
            if (mg == null)
            {
                mg = new MovieGenres();
                mg.ID = Guid.NewGuid();
            }

            mg.GenreID = genreId;
            mg.MovieID = movieId;

            return mg;
        }

        public static ContributorTypeViewModel ToContributorTypeViewModel(this ContributorType model)
        {
            return new ContributorTypeViewModel
            {
                ID = model.ID,
                Details = model.Translations.Select(z => z.ToDetailsViewModel()).ToList()
            };
        }

        public static GenreViewModel ToGenreViewModel(this Genre model)
        {
            return new GenreViewModel
            {
                ID = model.ID,
                Details = model.Translations.Select(z => z.ToDetailsViewModel()).ToList()
            };
        }

        public static ContributorViewModel ToContributorViewModel(this Contributor model)
        {
            return new ContributorViewModel
            {
                ID = model.ID,
                Details = model.Translations.Select(z => z.ToDetailsViewModel()).ToList(),
                ContributorTypes = model.ContributorContributorTypes.Select(x => x.ContributorTypeID).ToList()
            };
        }

        public static MovieViewModel ToMovieViewModel(this Movie model)
        {
            return new MovieViewModel
            {
                ID = model.ID,
                Details = model.Translations.Select(z => z.ToDetailsViewModel()).ToList(),
                Contributors = model.MovieContributors.Select(x => x.ContributorID).ToList(),
                Genres = model.MovieGenres.Select(x => x.GenreID).ToList(),
            };
        }

        public static DetailsViewModel ToDetailsViewModel(this Translation model)
        {
            return new DetailsViewModel
            {
                ID = model.ID,
                Name = model.Name,
                Title = model.Title,
                Description = model.Description,
                LanguageId = model.LanguageId
            };
        }
    }
}