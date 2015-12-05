using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
