using System;
using System.Collections.Generic;

namespace DawidSkene.Test
{
	public class Test
	{
		public static void Main(string[] args)
		{
			Utils_Test ();
			//DawidSkene_Test ();
		}

		public static void DawidSkene_Test ()
		{
			int max_iter = 100;
			List<Datum> responses = Datum.load_data("../../../../../data/dawid_skene.csv", true, ';');
			DawidSkene ds = new DawidSkene(responses);

			ds.run(max_iter);

			Console.WriteLine ("nPatients={0} nClasses={1} nObservers={2}", ds.nPatients, ds.nClasses, ds.nObservers);
//			Console.WriteLine(ds.PrintPriors());
//			Console.WriteLine(ds.PrintConfusionMatrices());
		}
		public static void Utils_Test ()
		{
			int[,] a= new int[,] { {1, 0}, {3, 6}, {9, 12} };
			var ret=Utils.Slice2D (a, 1, 1);

			for(int i=0; i<ret.Length; i++)
				Console.Write (string.Format("{0} ", ret[i]));
		}
	}
}

