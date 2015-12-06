using System;

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
	}
}

