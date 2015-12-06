using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DawidSkene
{
    /*public class Datum
    {
        public string WorkerLabel { get; protected set; }
        public string WorkerId { get; protected set; }
        public string TaskId { get; protected set; }

		public Datum(string WorkerId, string TaskId, string WorkerLabel)
        {
			this.WorkerId = WorkerId;
			this.TaskId = TaskId;
			this.WorkerLabel = WorkerLabel;
        }
    }*/
	public class Datum
	{
		public string label { get; protected set; }
		public string observer { get; protected set; }
		public string patient { get; protected set; }

		public Datum(string observer, string patient, string label)
		{
			this.observer = observer;
			this.patient = patient;
			this.label = label;
		}

		public static List<Datum> LoadData(string filename, bool skip_header, char sep=';')
		{
			List<Datum> responses=new List<Datum>();
			StreamReader sr = new StreamReader(filename);
			string line = null;
			while ((line = sr.ReadLine()) != null)
			{
				if (skip_header)
				{
					skip_header = false;
					continue;
				}
				string[] entries = line.Split (sep);
				if (entries.Length == 3)
				{
					responses.Add(new Datum (entries[1], entries[0], entries[2]));
				}
			}
			sr.Close();

			return responses;
		}
	}
}
