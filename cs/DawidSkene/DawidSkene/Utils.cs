using System;

namespace DawidSkene
{
	public class Utils
	{
		public static int[] Slice2D(int[,] a, int axe, int index)
		{
			int[] ret=new int[a.GetLength(axe)];

			for(int i=0; i<ret.Length; i++)
			{
				if(axe==0)
					ret[i]=a[i,index];
				if(axe==1)
					ret[i]=a[index,i];
			}

			return ret;
		}

		/*public static double[,] Slice3D(double[,,] a, int axe, int index)
		{
			int[,] ret=new int[a.GetLength(axe)];

			for(int i=0; i<a.GetLength(axe); i++)
			{
				if(axe==0)
					ret[i]=a[i,index];
				if(axe==1)
					ret[i]=a[index,i];
			}

			return ret;
		}*/
	}
}

