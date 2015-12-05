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

		public double[, ,] errorRates { get; protected set; }
        double[] classMarginals;
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
			/*foreach(var patient in this.patients)
			{
				i = patients.IndexOf(patient);
				foreach(var observer in this.responses[patient].keys())
				{
					k = observers.IndexOf(observer)
					foreach(var response in this.responses[patient][observer])
					{
							j = classes.IndexOf(response);
							this.counts[i, k, j] += 1;
					}
				}
			}*/
			foreach (var r in this.responses)
			{
				i = patients.IndexOf(r.patient);
				j = classes.IndexOf(r.label);
				k = observers.IndexOf (r.observer);
				this.counts[i, k, j] += 1;
			}
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