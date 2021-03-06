﻿using Application.Models;
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
            ContributorOptions(ref model, true);

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
            ContributorOptions(ref model, false);

            return model;
        }

        private static void ContributorOptions(ref ContributorViewModel model, bool isNew)
        {
            bool edit = false;

            model.Details = Common.ControlTranslations(model.Details, isNew, ref edit);
            model.ContributorTypes = Common.ControlRelationships(model.ContributorTypes, "Contributor Type", ref edit);

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

                Translation.Preview(c.Details);

                Common.Preview(c.ContributorTypes, "Contributor Types");
                Console.WriteLine("");
                i++;
            }
        }
    }
}