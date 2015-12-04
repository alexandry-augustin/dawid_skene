using System;
using System.IO;
using System.Collections.Generic;

namespace DawidSkene.Test
{
	public class Test
	{
		public static void Main(string[] args)
		{
			DawidSkene_Test ();
		}

		public static void DawidSkene_Test ()
		{
			int max_iter = 100;
			List<Datum> responses = Test.LoadData("../../../../../data/dawid_skene.csv", true);
			DawidSkene ds = new DawidSkene(responses);

			ds.Run(max_iter);

			Console.WriteLine ("I={0} J={1} K={2}", ds.I, ds.J, ds.K);
//			Console.WriteLine(ds.PrintPriors());
//			Console.WriteLine(ds.PrintConfusionMatrices());
		}
		private static List<Datum> LoadData(string filename, bool skip_header, char sep=';')
		{
			List<Datum> responses=new List<Datum>();
			StreamReader sr = new StreamReader(filename);
			string line = null;
			while ((line = sr.ReadLine()) != null)
			{
				if (skip_header)
				{
					skip_header = false;
					continue;
				}
				string[] entries = line.Split (sep);
				if (entries.Length == 3)
				{
					responses.Add(new Datum (entries[1], entries[0], entries[2]));
				}
			}
			sr.Close();

			return responses;
		}
	}
}

