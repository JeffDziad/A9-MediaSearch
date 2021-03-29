using System;
using System.Collections.Generic;

namespace A8_MediaSearch.Models
{

    public class Movie : Media
    {
        public override string displayConfirmation()
        {
            return String.Format($"Movie:\nID: {ID}\nTitle: {Title}\nGenres: {String.Join(",", genres)}");
        }
    }
}