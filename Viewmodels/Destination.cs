﻿using Microsoft.Maui.Controls.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Planner.Viewmodels
{
    internal class Destination
    {
        public string Name { get; set; }
        public int price { get; set; }
        public string dateString { get; set; }
        public DateTime date { get; set; }
        public Location coordinates { get; set; }
        public Pin pin { get; set; }

    }
}
