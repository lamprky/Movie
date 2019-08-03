using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Database;
using WebAPI.Models.Service;

namespace WebAPI.Extensions
{
    public static class Validations
    {
        public static void ValidateTranslations<T>(this List<T> transalations) where T : DetailsViewModel
        {
            if (transalations.Select(x => x.LanguageId).Distinct().Count() < transalations.Count())
                throw new Exception("For each language only one translation is allowed");

            if (transalations.Count() < 3)
                throw new Exception("Translation in at least 3 different languages is required");

            transalations.ForEach(x=> {
                if(string.IsNullOrEmpty(x.Name) && string.IsNullOrEmpty(x.Description) && string.IsNullOrEmpty(x.Title))
                    throw new Exception("The translation details should have at least one filled field");
            });
        }

        public static void ValidateContributorTypes(this List<Guid> reqContributorTypes, List<ContributorType> dbContributorTypes)
        {
            if (reqContributorTypes.Distinct().Count() > dbContributorTypes.Count())
            {
                var notExistingItems = reqContributorTypes.Where(p => !dbContributorTypes.Any(p2 => p2.ID == p));
                var notExistingItemsString = String.Join(", ", notExistingItems.ToArray());

                throw new Exception("Contributor Types {" + notExistingItemsString  + "} are not valid");
            }
        }

        public static void ValidateContributors(this List<Guid> reqContributor, List<Contributor> dbContributors)
        {
            if (reqContributor.Distinct().Count() > dbContributors.Count())
            {
                var notExistingItems = reqContributor.Where(p => !dbContributors.Any(p2 => p2.ID == p));
                var notExistingItemsString = String.Join(", ", notExistingItems.ToArray());

                throw new Exception("Contributors {" + notExistingItemsString + "} are not valid");
            }
        }

        public static void ValidateGenres(this List<Guid> reqGenres, List<Genre> dbGenres)
        {
            if (reqGenres.Distinct().Count() > dbGenres.Count())
            {
                var notExistingItems = reqGenres.Where(p => !dbGenres.Any(p2 => p2.ID == p));
                var notExistingItemsString = String.Join(", ", notExistingItems.ToArray());

                throw new Exception("Genres {" + notExistingItemsString + "} are not valid");
            }
        }
    }
}
