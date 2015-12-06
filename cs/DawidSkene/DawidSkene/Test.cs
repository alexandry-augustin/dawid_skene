using System;
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
			List<Datum> responses = Datum.load_data("../../../../../data/dawid_skene.csv", true, ';');
			DawidSkene ds = new DawidSkene(responses);

			ds.run(max_iter);

			Console.WriteLine ("nPatients={0} nClasses={1} nObservers={2}", ds.nPatients, ds.nClasses, ds.nObservers);
//			Console.WriteLine(ds.PrintPriors());
//			Console.WriteLine(ds.PrintConfusionMatrices());
		}
	}
}

