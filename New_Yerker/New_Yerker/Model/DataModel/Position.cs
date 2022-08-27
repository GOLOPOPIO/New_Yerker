using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace New_Yerker.Model.DataModel
{
    public class Position
    {
        [PrimaryKey]
        public string Group { get; set; }

        public string Name { get; set; }
        public string ImagaSource { get; set; }
        public int OldPrice { get; set; }
        public int NewPrice { get; set; }
        public List<Size> Sizes { get; set; }

        private DateTime _addTime { get; set; }
        private DateTime _lastUpdate { get; set; }

        public Position()
        {
            _addTime = DateTime.Now;
            _lastUpdate = _addTime;
        }
    }
}
