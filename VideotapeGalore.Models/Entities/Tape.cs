using System;
using System.Collections.Generic;

namespace VideotapeGalore.Models.Entities
{
    public class Tape
    {
        public enum TapeType
        {
            // ReSharper disable once InconsistentNaming
            VHS,
            Betamax
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public TapeType Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Eidr { get; set; }

        // Foreign keys
        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }
        public virtual IEnumerable<BorrowInfo> BorrowInfos { get; set; }
    }
}