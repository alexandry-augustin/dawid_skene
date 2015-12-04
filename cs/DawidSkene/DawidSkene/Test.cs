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
/*			int iterations = 100;
			string[] lines_input = Test.ReadFile("data/dawid_skene.csv", true);
			List<Datum> labelings = DawidSkene.LoadLabels(lines_input, ',');
			DawidSkene ds = new DawidSkene(labelings);

			// Save the majority vote before the D&S estimation
			//HashMap<String,String> prior_voting = ds.getMajorityVote();
			//Utils.writeFile( ds.printVote(), "./pre-majority-vote.txt");

			ds.Estimate(iterations);

			Console.WriteLine ("I={0} J={1} K={2}", ds.I, ds.J, ds.K);
			Console.WriteLine(ds.PrintPriors());
			Console.WriteLine(ds.PrintConfusionMatrices());*/
		}
		private static string[] ReadFile(string filename, bool skip_header)
		{
			StreamReader sr = new StreamReader(filename);
			List<string> lines = new List<string>();
			string line = null;
			while ((line = sr.ReadLine()) != null) {
				if (skip_header)
				{
					skip_header = false;
					continue;
				}
				lines.Add(line);
			}
			sr.Close();
			return lines.ToArray();
		}
	}
}

