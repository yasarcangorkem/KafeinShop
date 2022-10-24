﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KafeinShop.Core.DTOs
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
