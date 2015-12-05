using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DawidSkene
{
    public class DawidSkene
    {
		/// <summary>
		/// 
		/// </summary>
		public List<Datum> responses;
		/// <summary>
		/// 
		/// </summary>
		public int[, ,] counts;
        /// <summary>
        /// We have I tasks
        /// </summary>
		public int nPatients { get; protected set; }
        /// <summary>
        /// We have J possible labels per object
        /// </summary>
		public int nClasses { get; protected set; }
        /// <summary>
        /// We have K annotators
        /// </summary>
		public int nObservers { get; protected set; }
        /// <summary>
        /// Error rate (confusion matrix) for each annotator.
		/// errorRates[k][j][l] is the probability that annotator k, classifies an item from category j to category l
        /// </summary>
		public double[, ,] errorRates { get; protected set; }
        /// <summary>
        /// Priors for the different classes.
        /// </summary>
        double[] classMarginals;
        /// <summary>
        /// The probabilities of different labels for each object.
        /// T[oid][l] is the probability that the object oid belong to class l.
        /// </summary>
        double[,] T;

		public DawidSkene(List<Datum> responses)
        {
			this.responses = responses;
        }

		public void run(int max_iter)
		{
			// convert responses to counts
			responses_to_counts ();
			int patients = 0;
			int observers=0;
			int classes=0;
			Console.WriteLine ("num Patients:{0}", patients);
			Console.WriteLine ("Observers:{0}", observers);
			Console.WriteLine ("Classes:{0}", classes);

			// initialize
			int iter = 0;

			/*patient_classes = */initialize (); //equation (3.1)

			Console.WriteLine ("Iter\tlog-likelihood\tdelta-CM\tdelta-ER");

			while(iter<max_iter)
			{
				iter += 1;
				m_step ();
				e_step ();
			}

			// Print final results
			Console.WriteLine ("Class marginals");
			Console.WriteLine ("Error rates");
			  
			Console.WriteLine ("Patient classes");
		}

		public void responses_to_counts()
		{
			//responses
		}

		public void initialize()
		{
		}

		private void m_step()
		{
		}

		private void e_step()
		{
		}
    }
}