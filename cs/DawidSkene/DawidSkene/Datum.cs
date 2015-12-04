using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DawidSkene
{
    public class Datum
    {
        public string WorkerLabel { get; protected set; }
        public string WorkerId { get; protected set; }
        public string TaskId { get; protected set; }

		public Datum(string AnnotatorId, string ObjectId, string Label)
        {
			this.WorkerId = AnnotatorId;
			this.TaskId = ObjectId;
			this.WorkerLabel = Label;
        }
    }
}
