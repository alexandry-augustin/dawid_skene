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
		public int[,,] counts;

		public int nPatients { get; protected set; }
		public int nClasses { get; protected set; }
		public int nObservers { get; protected set; }

		public List<string> patients { get; protected set; }
		public List<string> classes { get; protected set; }
		public List<string> observers { get; protected set; }

		public double[,,] error_rates { get; protected set; }
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
			Console.WriteLine ("{0}", this.class_marginals_str());
			Console.WriteLine ("Error rates");
			Console.WriteLine ("{0}", this.error_rates_str());
			  
			Console.WriteLine ("Patient classes");
			Console.WriteLine ("{0}", this.patient_classes_str());
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
				for(int j=0; j<this.nClasses; ++j)
					for(int k=0; k<this.nObservers; ++k)
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

			this.class_marginals=new double[this.nClasses];
			this.error_rates=new double[this.nObservers, this.nClasses, this.nClasses];
		}

		private void m_step()
		{
			// compute class marginals
			double[] class_sums=new double[this.nClasses];

			for (int j = 0; j < this.nClasses; ++j)
				for (int i = 0; i < this.nPatients; ++i)
					class_sums[j] += this.patient_classes[i,j];

			for (int j = 0; j < this.nClasses; ++j)
				this.class_marginals[j] = class_sums[j] / (double)this.nPatients;
				
			// compute error rates
			for(int  k=0; k<this.nObservers; ++k)
				for (int j = 0; j < this.nClasses; ++j)
					for (int l = 0; l < this.nClasses; ++l)
						this.error_rates[k,j,l] = 0;

			double[] patient_classes_slice=new double[this.nPatients];
			int[] counts_slice=new int[this.nPatients];
			double[] error_rates_slice=new double[this.nClasses];
			double sum_over_responses=0;
			for(int  k=0; k<this.nObservers; ++k)
			{
				for (int j = 0; j < this.nClasses; ++j)
				{
					for (int l = 0; l < this.nClasses; ++l)
					{
						//compute slices
						for (int i = 0; i < this.nPatients; ++i)
						{
							patient_classes_slice[i]=this.patient_classes[i,j];
							counts_slice[i]=this.counts[i,k,l];
						}

						this.error_rates [k, j, l] = patient_classes_slice.Zip (counts_slice, (d1, d2) => d1 * d2).Sum (); //dot product
					}
					// normalize by summing over all observation classes
					for (int l = 0; l < this.nClasses; ++l)
						error_rates_slice[l]=error_rates[k,j,l];

					sum_over_responses=error_rates_slice.Sum();
					if(sum_over_responses>0)
						for (int l = 0; l < this.nClasses; ++l)
							error_rates[k,j,l]=error_rates[k,j,l]/(double)sum_over_responses;
				}
			}
		}

		private void e_step()
		{
			for (int i = 0; i < this.nPatients; ++i)
				for (int j = 0; j < this.nClasses; ++j)
					this.patient_classes[i,j] = 0;

			int[,] counts_slice=new int[this.nObservers, this.nClasses];	//TODO remove
			double[,] error_rates_slice=new double[this.nObservers, this.nClasses];	//TODO remove
			double[,] error_rates_pow=new double[this.nObservers, this.nClasses]; //TODO remove
			double[] patient_classes_slice=new double[this.nClasses];
			double patient_sum = 0;
			double estimate = 0;
			for (int i = 0; i < this.nPatients; ++i)
			{
				for (int j = 0; j < this.nClasses; ++j)
				{
					estimate=this.class_marginals[j];

					// compute slices
					for(int  k=0; k<this.nObservers; ++k)
						for (int l = 0; l < this.nClasses; ++l)
						{
							counts_slice[k,l]=this.counts[i,k,l];	//TODO remove
							error_rates_slice[k,l]=this.error_rates[k,j,l];	//TODO remove
							error_rates_pow[k,l]=Math.Pow(error_rates_slice[k,l], counts_slice[k,l]); //TODO remove
							estimate *= error_rates_pow [k, l];
						}
							
					this.patient_classes [i, j] = estimate;
				}
				// normalize error rates by dividing by the sum over all observation classes
				// compute slices
				for (int j = 0; j < this.nClasses; ++j)
					patient_classes_slice[j]=this.patient_classes[i,j];

				patient_sum = patient_classes_slice.Sum ();
				if (patient_sum > 0)
					for (int l = 0; l < this.nClasses; ++l)
						patient_classes [i, l] = patient_classes [i, l] / (double)patient_sum;
			}
		}

		public string error_rates_str()
		{
			StringBuilder sb = new StringBuilder();
			for (int k = 0; k < this.nObservers; k++)
			{
				sb.AppendLine (string.Format("observer {0}:", k));
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

		public string patient_classes_str()
		{
			StringBuilder sb = new StringBuilder ();
			for (int i = 0; i < this.nPatients; i++)
			{
				sb.Append (string.Format("{0} [", i));
				for (int j = 0; j < this.nClasses; ++j)
					sb.Append (string.Format("{0:0.000} ", this.patient_classes [i, j]));
				sb.Append ("]\n");
			}
			return sb.ToString ();
		}

		public string class_marginals_str()
		{
			StringBuilder sb = new StringBuilder ();

			sb.Append ("[");
			for(int j=0; j<this.nClasses; ++j)
				sb.Append(string.Format("{0:0.00} ", this.class_marginals[j]));
			sb.Append ("]\n");

			return sb.ToString ();
		}
    }
}