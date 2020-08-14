using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelcukETicaret.Models.Account
{
    public class ProfilModels
    {
        public DB.Members Members { get; set; }
        public List<DB.Addresses> Addresses { get; set; }
    }
}