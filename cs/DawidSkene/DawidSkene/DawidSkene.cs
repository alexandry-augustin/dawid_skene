using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DawidSkene
{
    public class DawidSkene
    {
		public List<Datum> responses;
		public int[, ,] counts;

		public int nPatients { get; protected set; }
		public int nClasses { get; protected set; }
		public int nObservers { get; protected set; }

		public List<string> patients { get; protected set; }
		public List<string> classes { get; protected set; }
		public List<string> observers { get; protected set; }

		public double[, ,] error_rates { get; protected set; }
		double[] class_marginals;
        double[,] patient_classes;

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

			initialize (); //equation (3.1)

			Console.WriteLine ("Iter\tlog-likelihood\tdelta-CM\tdelta-ER");

			while(iter<max_iter)
			{
				iter += 1;
				m_step ();
				e_step ();
			}

			// Print final results
			Console.WriteLine ("Class marginals");
			Console.WriteLine ("{0}", this.class_marginals); //FIXME
			Console.WriteLine ("Error rates");
			Console.WriteLine ("{0}", this.error_rates_str()); //FIXME
			  
			Console.WriteLine ("Patient classes");
			for (int i=0; i<this.nPatients; i++)
				Console.WriteLine ("{0} {1}", i, patient_classes[i, 0]); //FIXME
		}

		private void responses_to_counts()
		{
			// determine the observers and classes
			this.observers=this.responses.Select (n => n.observer).Distinct().ToList();
			this.observers.Sort ();
			this.nObservers=this.observers.Count;

			this.patients=this.responses.Select (n => n.patient).Distinct().ToList();
			this.patients.Sort ();
			this.nPatients=this.patients.Count;

			this.classes = this.responses.Select (n => n.label).Distinct ().ToList ();
			this.classes.Sort ();
			this.nClasses=this.classes.Count;

			// create a 3d array to hold counts
			this.counts = new int[this.nPatients, this.nObservers, this.nClasses];

			// convert responses to counts
			int i=0; int j=0; int k=0;
			foreach (var r in this.responses)
			{
				i = patients.IndexOf(r.patient);
				j = classes.IndexOf(r.label);
				k = observers.IndexOf (r.observer);
				this.counts[i, k, j] += 1;
			}
		}

		private void initialize()
		{
			int[,] response_sums=new int[this.nPatients, this.nClasses];

			// sum over observers
			for(int  i=0; i<this.nPatients; ++i)
				for(int  j=0; j<this.nClasses; ++j)
					for(int  k=0; k<this.nObservers; ++k)
						response_sums[i,j] += this.counts[i,k,j];

			int[] response_sums_=new int[this.nPatients];

			for (int i = 0; i < this.nPatients; ++i)
				for (int j = 0; j < this.nClasses; ++j)
					response_sums_[i] += response_sums[i,j];

			// create an empty array
			this.patient_classes=new double[this.nPatients, this.nClasses];

			for (int i = 0; i < this.nPatients; ++i)
				for (int j = 0; j < this.nClasses; ++j)
					this.patient_classes[i,j] = response_sums[i,j] / (double)response_sums_[i];

		}

		private void m_step()
		{
		}

		private void e_step()
		{
		}

		public string error_rates_str()
		{
			StringBuilder sb = new StringBuilder();
			for (int k = 0; k < this.nObservers; k++)
			{
				sb.AppendLine (string.Format("\nobserver {0}:", k));
				for (int j = 0; j < this.nClasses; j++)
				{
					for (int l = 0; l < this.nClasses; l++)
					{
						sb.Append (string.Format("{0:0.00}\t", this.error_rates [k, j, l]));
					}
					sb.Append ("\n");
				}
			}
			return sb.ToString();
		}
    }
}