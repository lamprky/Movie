using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interations
{
    public class Movie
    {
        public static MovieViewModel Create()
        {
            Console.Clear();
            Console.WriteLine("Let's handle a Movie! \n");

            MovieViewModel model = new MovieViewModel();

            Console.WriteLine("All we need for creating a Movie is to give us some details!");
            MovieOptions(ref model, true);

            return model;
        }

        public static MovieViewModel Update()
        {
            Console.Clear();
            Console.WriteLine("Let's handle a Movie! \n");

            MovieViewModel model = Common.GetRecordForUpdate<MovieViewModel>("M");
            if (model == null)
                return null;

            Console.WriteLine("Okey, we are ready!");
            MovieOptions(ref model, false);

            return model;
        }

        private static void MovieOptions(ref MovieViewModel model, bool isNew)
        {
            bool edit = false;

            model.Details = Common.ControlTranslations(model.Details, isNew, ref edit);
            model.Genres = Common.ControlRelationships(model.Genres, "Genre", ref edit);
            model.Contributors = Common.ControlRelationships(model.Contributors, "Contributor", ref edit);

            if (!edit)
            {
                model = null;
                Console.WriteLine("There is nothing else to modify in this entity. You may probably decide to make some other action!");
            }
        }

        public static void Preview(List<MovieViewModel> Movies)
        {
            Console.WriteLine("\nLet's see how api responsed back!");

            int i = 1;
            foreach (var c in Movies)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Movie: ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(String.Format("{0,-10} | {1,-10}", i + ". ", c.ID));
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Translation.Preview(c.Details);

                Common.Preview(c.Genres, "Genres");
                Common.Preview(c.Contributors, "Contributors");

                Console.WriteLine("");
                i++;
            }
        }
    }
}