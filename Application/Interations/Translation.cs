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

        public static void CreateOrUpdateTranslations(List<DetailsViewModel> details)
        {
            Console.Clear();

            var input = "";
            Console.WriteLine("Let's handle our translations! \n");
            Console.WriteLine("We need at least 3 translations, but feel free to add as many as you wish!");

            if (details.Count > 0)
            {
                Console.WriteLine("The good news is that it's seems that we have already some translations! Less job for you!");

                input = Common.GetYesOrNoAnswer("You may want to preview them, isn't it?");
                if (input == Common.Yes)
                    Preview(details);

                input = Common.GetYesOrNoAnswer("It is something make you feel unhappy with? Do you want to apply changes?");
                if (input == Common.Yes)
                    UpdateTranslations(details);
            }

            if (details.Count < Languages.Count)
            {
                input = Common.GetYesOrNoAnswer("Do you want to continue by adding new translations?");
                if (input == Common.Yes)
                    CreateTranslations(details);
            }
        }

        #region Create

        private static void CreateTranslations(List<DetailsViewModel> details)
        {
            Console.WriteLine("\nOkey, let's go!");
            Console.WriteLine("A translation need to be a combination of a title, a name and a description, separated by the comma character!");

            string input = Common.Yes;
            while (details.Count() <= 2 || input == Common.Yes)
            {
                if (details.Count() > 0)
                    Console.WriteLine("\nRemember: We need a title, a name and a description. Comma separated! Order is important!");

                CreateTranslation(details);

                if (details.Count() == 3)
                    Console.WriteLine("Wow! The minimum requirement just fullfilled!");

                if (details.Count() >= 3 && details.Count < Languages.Count())
                    input = Common.GetYesOrNoAnswer("Do you want to continue adding?");

                if (details.Count == Languages.Count())
                    input = Common.No;
            }

            Console.WriteLine("\nOkey, these are the translations!");
            Preview(details);
        }

        private static void CreateTranslation(List<DetailsViewModel> details)
        {
            var translationParts = GetTranslationParts();
            int language = GetLanguage(details, null);
            AddTranslation(details, translationParts, language);
        }

        private static void AddTranslation(List<DetailsViewModel> details, string[] translationParts, int language)
        {
            var translation = new DetailsViewModel
            {
                ID = null,
                Title = translationParts[0],
                Name = translationParts[1],
                Description = translationParts[2],
                LanguageId = Languages[OrderedLanguages[language]]
            };
            details.Add(translation);

            Common.ShowSuccesInputMessage("Cool! Your translation saved temporarily!");
        }

        #endregion Create

        #region Update

        private static void UpdateTranslations(List<DetailsViewModel> details)
        {
            var input = Common.Yes;
            while (input == Common.Yes)
            {
                var key = GetKeyToModify(details);
                if (key != null)
                {
                    Console.WriteLine("Ok, to edit a translation you should give us the new content!");
                    Console.WriteLine("A translation need to be a combination of a title, a name and a description, separated by the comma character!");

                    UpdateTranslation(details, key.Value);

                    if (details.Count < Languages.Count())
                        input = Common.GetYesOrNoAnswer("Do you want to continue modifying the existing translations?");

                    if (details.Count == Languages.Count())
                        input = Common.No;
                }
                else
                {
                    Console.WriteLine("We are sorry, but it seems that we get confused :( You may not want to edit the existed translations");
                    return;
                }
            }

            Console.WriteLine("\nOkey, these are the translations!");
            Preview(details);
        }

        private static void UpdateTranslation(List<DetailsViewModel> details, Guid key)
        {
            var translationParts = GetTranslationParts();
            int language = GetLanguage(details, key);
            UpdateTranslation(details, key, translationParts, language);
        }

        private static void UpdateTranslation(List<DetailsViewModel> details, Guid key, string[] translationParts, int language)
        {
            var translation = new DetailsViewModel
            {
                ID = key,
                Title = translationParts[0],
                Name = translationParts[1],
                Description = translationParts[2],
                LanguageId = Languages[OrderedLanguages[language]]
            };

            var pos = details.IndexOf(details.First(x => x.ID == key));
            details[pos] = translation;

            Common.ShowSuccesInputMessage("Cool! Your translation saved temporarily!");
        }

        #endregion Update

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

        private static Guid? GetKeyToModify(List<DetailsViewModel> details)
        {
            Guid? key = Guid.Empty;
            bool isValid = false;
            while (!isValid)
            {
                key = Common.GetKey();
                if (key != null)
                {
                    if (details.Select(x => x.ID).Contains(key))
                        isValid = true;
                    else
                        Common.ShowWrongInputMessage("");
                }
                else
                {
                    var input = Common.GetYesOrNoAnswer("You may want to preview them, isn't it?");
                    if (input == Common.Yes)
                        Preview(details);
                    else
                        break;
                }
            }

            return key;
        }

        public static void Preview(List<DetailsViewModel> details)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Translations: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            int i = 1;
            foreach (var tr in details)
            {
                Console.WriteLine(String.Format("{0,-10} | {1,-10} | {2,-10}| {3,-10} | {4,-10} | {5,-10}",
                    i + ". ", tr.ID, tr.Title, tr.Name, tr.Description, Languages.FirstOrDefault(x => x.Value == tr.LanguageId).Key));
                i++;
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        private static int GetLanguage(List<DetailsViewModel> details, Guid? key)
        {
            var currentLanguage = (key == null) ? null : details.SingleOrDefault(x => x.ID == key)?.LanguageId;
            var existingTranslations = details.Select(x => x.LanguageId).ToList();
            var languagesToShow = Languages.Where(x => !existingTranslations.Contains(x.Value) || (currentLanguage != null && x.Value == currentLanguage)).ToList()
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