﻿using System.Collections.Generic;

namespace FlightPlanner.Core.Dto
{
    public class PageResult<T>
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }
    }
}