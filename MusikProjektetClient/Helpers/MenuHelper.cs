using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetClient.Helpers
{
	public class MenuHelper
	{
		public int FormatMenu(string whatMenu, string[] options)
		{
			ConsoleKeyInfo keyinfo;
			int optionIndex = 0;
			int optionsLength = options.Length;

			do
			{
				Console.Clear();
				Console.WriteLine(whatMenu);
                Console.WriteLine();

                for (int i = 0; i < optionsLength; i++)
				{
					if (i == optionIndex)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine(options[i]);
						Console.ResetColor();
					}
					else
					{
						Console.WriteLine(options[i]);
					}
				}

				keyinfo = Console.ReadKey();

				if (keyinfo.Key == ConsoleKey.DownArrow && optionIndex < optionsLength - 1)
				{
					optionIndex++;
				}
				else if (keyinfo.Key == ConsoleKey.UpArrow && optionIndex > 0)
				{
					optionIndex--;
				}

			} while (keyinfo.Key != ConsoleKey.Enter);

			return optionIndex+1;
		}

		public static void BackToMenu()
		{
            Console.WriteLine("Press any key to go back to menu");
			Console.ReadKey();
			Console.Clear();
        }

			//public static void FormatList()
			//{
			//	int customerIndex = 0;
			//	ConsoleKeyInfo keyinfo;
			//	do
			//	{
			//		int listItem2 = 0;
			//		keyinfo = Console.ReadKey();

			//		if (keyinfo.Key == ConsoleKey.DownArrow && customerIndex <= emp.Count)
			//		{
			//			Console.Clear();
			//			customerIndex++;
			//			foreach (var customer in emp)
			//			{
			//				if (customerIndex == listItem2)
			//				{
			//					Console.ForegroundColor = ConsoleColor.Green;
			//					Console.WriteLine($"Company name: {customer.CompanyName} Country: {customer.Country}     Region: {customer.Region}     Phone: {customer.Phone}    Orders: {orders.Where(e => e.CustomerId == customer.CustomerId).Count()}");
			//					Console.ResetColor();
			//				}
			//				else
			//				{
			//					Console.WriteLine($"Company name: {customer.CompanyName} Country: {customer.Country}     Region: {customer.Region}     Phone: {customer.Phone}    Orders: {orders.Where(e => e.CustomerId == customer.CustomerId).Count()}");
			//				}

			//				listItem2++;
			//			}
			//			Console.WriteLine("Customer: " + customerIndex);
			//		}
			//		else if (keyinfo.Key == ConsoleKey.UpArrow && customerIndex > 0)
			//		{
			//			Console.Clear();
			//			customerIndex--;
			//			foreach (var customer in emp)
			//			{
			//				if (customerIndex == listItem2)
			//				{
			//					Console.ForegroundColor = ConsoleColor.Green;
			//					Console.WriteLine($"Company name: {customer.CompanyName} Country: {customer.Country}     Region: {customer.Region}     Phone: {customer.Phone}");
			//					Console.ResetColor();
			//				}
			//				else
			//				{
			//					Console.WriteLine($"Company name: {customer.CompanyName} Country: {customer.Country}     Region: {customer.Region}     Phone: {customer.Phone}");
			//				}

			//				listItem2++;
			//			}
			//			Console.WriteLine("Customer: " + customerIndex);
			//		}
			//	} while (Console.ReadKey().Key != ConsoleKey.Enter);
			//}
		}
	
}
