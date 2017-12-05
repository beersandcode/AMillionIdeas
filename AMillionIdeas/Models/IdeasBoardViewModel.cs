using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMillionIdeas.Models
{
    public class IdeasBoardViewModel
    {
        public int MaxNumberOfIdeas { get; set; }
        public int NumberOfCreatedIdeas { get; set; }
        public List<IdeaViewModel> ListIdeas { get; set; }
        public int TotalFreeSpaces { get; set; }
        public List<int?> UsedPositions { set; get; }
        
    }
}