using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp
{
    class DataM
    {
        public string Error { get; set; }
        public IList<Logs> Logs { get; set; }
        private IList<Logs> Sort(IList<Logs> logs, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
                return logs;
            var pivotIndex = Split(logs, minIndex, maxIndex);
            Sort(logs, minIndex, pivotIndex - 1);
            Sort(logs, pivotIndex + 1, maxIndex);

            return logs;
        }
        private int Split(IList<Logs> logs, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (logs[i].Created_at.Ticks < logs[maxIndex].Created_at.Ticks)
                {
                    pivot++;
                    Swap(pivot, i);
                }
            }
            pivot++;
            Swap(pivot, maxIndex);
            return pivot;
        }
        private void Swap(int x, int y)
        {
            var temp = this.Logs[x];
            this.Logs[x] = this.Logs[y];
            this.Logs[y] = temp;
        }
        public void Sort()
        {
            this.Logs = Sort(this.Logs, 0, this.Logs.Count - 1);
        }
    }
    public class Logs
    {
        public DateTime Created_at { get; set; }
        public string First_name { get; set; }
        public string Message { get; set; }
        public string Second_name { get; set; }
        public string User_id { get; set; }
    }
}
