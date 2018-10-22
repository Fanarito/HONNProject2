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
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }

        public virtual IEnumerable<BorrowInfo> BorrowInfos { get; set; }
    }
}