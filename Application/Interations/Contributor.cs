using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interations
{
    public class Contributor
    {
        public static ContributorViewModel Create()
        {
            Console.Clear();
            Console.WriteLine("Let's handle a contributor! \n");

            ContributorViewModel model = new ContributorViewModel();

            Console.WriteLine("All we need for creating a contributor is to give us some details!");
            ContributorOptions(ref model);

            return model;
        }

        public static ContributorViewModel Update()
        {
            Console.Clear();
            Console.WriteLine("Let's handle a contributor! \n");

            ContributorViewModel model = Common.GetRecordForUpdate<ContributorViewModel>("C");
            if (model == null)
                return null;

            Console.WriteLine("Okey, we are ready!");
            ContributorOptions(ref model);

            return model;
        }

        private static void ContributorOptions(ref ContributorViewModel model)
        {
            bool edit = false;
            string input = Common.GetYesOrNoAnswer("Do you like to add or modify translations?");
            if (input == Common.Yes)
            {
                Translation.CreateOrUpdateTranslations(model);
                edit = true;
            }

            input = Common.GetYesOrNoAnswer("Do you like to add or modify contributor types relashionships?");
            if (input == Common.Yes)
            {
                edit = true;
                if (model.ContributorTypes.Count() > 0)
                {
                    Console.WriteLine("There are already a couple of contributor types relashionships");

                    input = Common.GetYesOrNoAnswer("You may want to preview them, isn't it?");
                    if (input == Common.Yes)
                        Common.Preview(model.ContributorTypes, "Contributor Types");
                }

                Common.HandleRelationShips(model.ContributorTypes, "Contributor Types");
            }

            if (!edit)
            {
                model = null;
                Console.WriteLine("There is nothing else to modify in this entity. You may probably decide to make some other action!");
            }
        }

        public static void Preview(List<ContributorViewModel> Contributors)
        {
            Console.WriteLine("\nLet's see how api responsed back!");

            int i = 1;
            foreach (var c in Contributors)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Contributor: ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(String.Format("{0,-10} | {1,-10}", i + ". ", c.ID));
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Translation.Preview(c);

                Common.Preview(c.ContributorTypes, "Contributor Types");
                Console.WriteLine("");
                i++;
            }
        }
    }
}