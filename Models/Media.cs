using System;
using System.Collections.Generic;

namespace A8_MediaSearch.Models
{

    public abstract class Media
    {

        public int ID {get; set;}
        public string Title {get; set;}
        public string[] genres {get; set;}

        public abstract string displayConfirmation();
    }
}