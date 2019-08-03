using Application.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interations
{
    public static class Genre
    {
        public static GenreViewModel Create()
        {
            Console.Clear();
            Console.WriteLine("Let's handle a movie gerne! \n");

            GenreViewModel model = new GenreViewModel();

            Console.WriteLine("All we need for creating a movie gerne is to give us some details! In some languages!");
            GenreOptions(ref model, true);

            return model;
        }

        public static GenreViewModel Update()
        {
            Console.Clear();
            Console.WriteLine("Let's handle a movie genre! \n");

            GenreViewModel model = Common.GetRecordForUpdate<GenreViewModel>("G");
            if (model == null)
                return null;

            Console.WriteLine("Okey, we are ready!");
            GenreOptions(ref model, false);

            return model;
        }

        private static void GenreOptions(ref GenreViewModel model, bool isNew)
        {
            bool edit = false;
            model.Details = Common.ControlTranslations(model.Details, isNew, ref edit);

            if (!edit)
            {
                model = null;
                Console.WriteLine("There is nothing else to modify in this entity. You may probably decide to make some other action!");
            }
        }

        public static void Preview(List<GenreViewModel> Genres)
        {
            Console.WriteLine("\nLet's see how api responsed back!");

            int i = 1;
            foreach (var g in Genres)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Genre: ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(String.Format("{0,-10} | {1,-10}", i + ". ", g.ID));
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Translation.Preview(g.Details);
                Console.WriteLine("");
                i++;
            }
        }
    }
}