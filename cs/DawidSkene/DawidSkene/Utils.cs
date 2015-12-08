using System;
using System.Collections.Generic;
using System.Linq;

namespace DawidSkene
{
	public class Utils
	{
		public static T[] Slice2D<T>(T[,] a, int axe, int index)
		{
			T[] ret=new T[a.GetLength(axe)];

			for(int i=0; i<ret.Length; i++)
			{
				if(axe==0)
					ret[i]=a[i,index];
				if(axe==1)
					ret[i]=a[index,i];
			}

			return ret;
		}

		/*public static T[,] Slice3D<T>(T[,,] a, int axe, int index)
		{
			T[,] ret=new int[a.GetLength(axe)];

			for(int i=0; i<a.GetLength(axe); i++)
			{
				if(axe==0)
					ret[i]=a[i,index];
				if(axe==1)
					ret[i]=a[index,i];
			}

			return ret;
		}*/

		/// <summary>
		/// Sort by a list of string intelligently (i.e. "1", "2", "10" instead of "1", "10", "2")
		/// </summary>
		/// <param name="l">List of strings</param>
		public static void SmartSort(List<string> l)
		{
			if(l==null)
				return;
			int m=0;
			if (!int.TryParse (l[0], out m)) //is not integer
				return;

			List<int> temp = l.Select (int.Parse).ToList (); //convert string to int
			temp.Sort ();
			l = temp.Select (n => n.ToString ()).ToList (); //convert int to string
		}
	}
}

