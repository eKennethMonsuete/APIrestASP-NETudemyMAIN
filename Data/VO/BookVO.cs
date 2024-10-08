﻿using APIrestASP_NETudemy.Hypermedia;
using APIrestASP_NETudemy.Hypermedia.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIrestASP_NETudemy.Data.VO
{
    public class BookVO : ISupportHyperMedia
    {

        public long Id
        {
            get; set;
        }

        public string Author
        {
            get; set;
        }


        public decimal Price
        {
            get; set;
        }


        public DateTime LaunchDate
        {
            get; set;
        }


        public string Title
        {
            get; set;
        }

        public List<HyperMediaLink> Links
        {
            get;
            set;
        } = new List<HyperMediaLink>();
    }
}
