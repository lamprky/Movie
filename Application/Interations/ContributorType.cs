﻿using Application.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interations
{
    public static class ContributorType
    {
        public static ContributorTypeViewModel Create()
        {
            Console.Clear();
            Console.WriteLine("Let's handle a contributor type! \n");

            ContributorTypeViewModel model = new ContributorTypeViewModel();

            Console.WriteLine("All we need for creating a contributor type is to give us some details! In some languages!");
            ContributorTypeOptions(ref model, true);

            return model;
        }

        public static ContributorTypeViewModel Update()
        {
            Console.Clear();
            Console.WriteLine("Let's handle a contributor type! \n");

            ContributorTypeViewModel model = Common.GetRecordForUpdate<ContributorTypeViewModel>("CT");
            if (model == null)
                return null;

            Console.WriteLine("Okey, we are ready!");
            ContributorTypeOptions(ref model, false);

            return model;
        }

        private static void ContributorTypeOptions(ref ContributorTypeViewModel model, bool isNew)
        {
            bool edit = false;
            model.Details = Common.ControlTranslations(model.Details, isNew, ref edit);

            if (!edit)
            {
                model = null;
                Console.WriteLine("There is nothing else to modify in this entity. You may probably decide to make some other action!");
            }
        }

        public static void Preview(List<ContributorTypeViewModel> contributorTypes)
        {
            Console.WriteLine("\nLet's see how api responsed back!");

            int i = 1;
            foreach (var ct in contributorTypes)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Contributor Type: ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(String.Format("{0,-10} | {1,-10}", i + ". ", ct.ID));
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Translation.Preview(ct.Details);
                Console.WriteLine("");
                i++;
            }
        }
    }
}