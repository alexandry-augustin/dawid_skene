using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DawidSkene
{
    public class DawidSkene
    {
        // Maps annotators, object, and classes to integers
        Dictionary<string, int> annotators;
        Dictionary<string, int> contributions;
        Dictionary<string, int> objects;
        Dictionary<string, int> classes;

        // Given the ID's retrieve the names
        Dictionary<int, string> annotators_names;
        Dictionary<int, string> objects_names;
        Dictionary<int, string> classes_names;

        /// <summary>
        /// We have N tasks
        /// </summary>
		public int N { get; protected set; }
        /// <summary>
        /// We have J possible labels per object
        /// </summary>
		public int J { get; protected set; }
        /// <summary>
        /// We have K annotators
        /// </summary>
		public int K { get; protected set; }
        /// <summary>
        /// The labels given to each object from each annotator.
        /// label[k][i] is the label assigned by annotator k to object i.
        /// We assign -1 if it has not been labeled!!!!
        /// </summary>
        int[,] label;
        /// <summary>
        /// Error rate (confusion matrix) for each annotator.
        /// pi[k][j][l] is the probability that annotator k, classifies an item from category j to category l
        /// </summary>
		public double[, ,] pi { get; protected set; }
        /// <summary>
        /// Priors for the different classes.
        /// </summary>
        double[] prior;
        /// <summary>
        /// The probabilities of different labels for each object.
        /// T[oid][l] is the probability that the object oid belong to class l.
        /// </summary>
        double[,] T;

        public DawidSkene(List<Datum> labels)
        {
 
        }

		public void Run(int max_iter)
		{
		}

		public void responsesToCounts()
		{
		}

		public void Initialize()
		{
		}

		private void MStep()
		{
		}

		private void EStep()
		{
		}
    }
}