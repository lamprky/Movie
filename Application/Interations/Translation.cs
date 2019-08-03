using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interations
{
    public static class Translation
    {

        private static Dictionary<int, string> OrderedLanguages = new Dictionary<int, string>
        {
            { 1, "English" },
            { 2, "Greek" },
            { 3, "Italian" },
            { 4, "Spanish" },
        };

        private static Dictionary<string, Guid> Languages = new Dictionary<string, Guid>
        {
            { "English", Guid.Parse("D4423635-10ED-416E-81EE-D5D7E6746090") },
            { "Greek", Guid.Parse("E621BA86-B5CF-4AAE-B867-56969E71851A") },
            { "Italian", Guid.Parse("3828E370-B755-47E8-8CCC-12EBCB1CEBA4") },
            { "Spanish", Guid.Parse("B6F6270D-F435-4BD4-8723-94B7659B0505") },
        };

        public static void CreateOrUpdateTranslations(IGeneralViewModel model)
        {
            Console.Clear();

            var input = "";
            Console.WriteLine("Let's handle our translations! \n");
            Console.WriteLine("We need at least 3 translations, but feel free to add as many as you wish!");

            if (model.Details.Count > 0)
            {
                Console.WriteLine("The good news is that it's seems that we have already some translations! Less job for you!");

                input = Common.GetYesOrNoAnswer("You may want to preview them, isn't it?");
                if (input == Common.Yes)
                    PreviewTranslations(model);

                input = Common.GetYesOrNoAnswer("It is something make you feel unhappy with? Do you want to apply changes?");
                if (input == Common.Yes)
                    UpdateTranslations(model);
            }

            if (model.Details.Count < Languages.Count)
            {
                input = Common.GetYesOrNoAnswer("Do you want to continue by adding new translations?");
                if (input == Common.Yes)
                    CreateTranslations(model);
            }
        }

        private static void CreateTranslations(IGeneralViewModel model)
        {
            Console.WriteLine("\nOkey, let's go!");
            Console.WriteLine("A translation need to be a combination of a title, a name and a description, separated by the comma character!");

            string input = "";
            while (model.Details.Count() <= 2 || input == "Y")
            {
                if (model.Details.Count() > 0)
                    Console.WriteLine("Remember: We need a title, a name and a description. Comma separated! Order is important!");

                CreateTranslation(model);

                if (model.Details.Count() == 3)
                    Console.WriteLine("Wow! The minimum requirement just fullfilled!");

                if (model.Details.Count() >= 3 && model.Details.Count < Languages.Count())
                    input = Common.GetYesOrNoAnswer("Do you want to continue adding?");
                    
                if (model.Details.Count == Languages.Count())
                    input = "N";
            }

            Console.WriteLine("\nOkey, these are the translations!");
            PreviewTranslations(model);
        }

        private static void CreateTranslation(IGeneralViewModel model)
        {
            var translationParts = GetTranslationParts();
            int language = GetLanguage(model);
            AddTranslation(model, translationParts, language);
        }

        private static void AddTranslation(IGeneralViewModel model, string[] translationParts, int language)
        {
            var translation = new DetailsViewModel
            {
                ID = null,
                Title = translationParts[0],
                Name = translationParts[1],
                Description = translationParts[2],
                LanguageId = Languages[OrderedLanguages[language]]
            };
            model.Details.Add(translation);

            Common.ShowSuccesInputMessage("\nCool! Your translation saved temporarily!");
        }

        private static string[] GetTranslationParts()
        {
            Console.WriteLine("We are listening!");
            string[] translationParts = new string[] { };
            bool isValid = false;

            while (!isValid)
            {
                string input = Common.GetUserInput();
                translationParts = input.Split(",");

                if (translationParts.Count() != 3)
                    Common.ShowWrongInputMessage("");
                else
                    isValid = true;
            }

            return translationParts;
        }

        private static void UpdateTranslations(IGeneralViewModel model)
        {
            //todo
        }

        public static void PreviewTranslations(IGeneralViewModel model)
        {
            int i = 1;
            foreach (var tr in model.Details)
            {
                Console.WriteLine(String.Format("{0,-10} | {1,-10} | {2,-10}| {3,-10} | {4,-10} | {5,-10}",
                    i + ". ", tr.ID, tr.Title, tr.Name, tr.Description, Languages.FirstOrDefault(x => x.Value == tr.LanguageId).Key));
                i++;
            }
        }

        private static int GetLanguage(IGeneralViewModel model)
        {
            var existingTranslations = model.Details.Select(x => x.LanguageId).ToList();
            var languagesToShow = Languages.Where(x => !existingTranslations.Contains(x.Value)).ToList()
                .SelectMany(x => OrderedLanguages.Where(y => y.Value == x.Key).ToList()).ToList();

            Console.WriteLine("\nHm, ok but in which language?");

            string input = "";
            bool isValidLanguage = false;
            int language = 0;

            while (!isValidLanguage)
            {
                Console.WriteLine("Peak a number from the list bellow!");
                foreach (var lang in languagesToShow)
                {
                    Console.Write("\t" + lang.Key + ". " + lang.Value);
                }
                Console.WriteLine("");
                input = Common.GetUserInput();

                if (!int.TryParse(input, out language) || languagesToShow.Where(x => x.Key == language).Count() == 0)
                    Common.ShowWrongInputMessage("");
                else
                    isValidLanguage = true;
            }

            return language;
        }
    }
}
