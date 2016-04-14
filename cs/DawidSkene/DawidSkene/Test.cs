using System;
using System.Collections.Generic;

namespace DawidSkene.Test
{
	public class Test
	{
		public static void Main(string[] args)
		{
			//Utils_Test ();
			OriginalPaperTest ();
			ForwardSamplingTest ();
		}
		/// <summary>
		/// Test with the dataset from the original paper Dawid and Skene (1979)
		/// </summary>
		public static void OriginalPaperTest ()
		{
			List<Datum> responses = Datum.load_data("../../../../../data/dawid_skene.csv", true, ';');
			DawidSkene ds = new DawidSkene(responses);

			ds.run();
		}
		public static void Utils_Test ()
		{
			int[,] a= new int[,] { {1, 0}, {3, 6}, {9, 12} };
			var ret=Utils.Slice2D (a, 1, 1);

			for(int i=0; i<ret.Length; i++)
				Console.Write (string.Format("{0} ", ret[i]));
		}
		/// <summary>
		/// Test using forward sampling
		/// </summary>
		public static void ForwardSamplingTest ()
		{
			int nPatients=45;
			int nClasses=4;
			int nObservers=5;

			//Discrete class_marginals = ;
		}
	}
}

