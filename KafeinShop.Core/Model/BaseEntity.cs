﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KafeinShop.Core.Model
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
