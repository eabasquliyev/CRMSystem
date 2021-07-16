﻿using System;

namespace CrmSystem.Domain.Models
{
    public abstract class User:DomainObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + ' ' + LastName;
        public string Email { get; set; }
        public string ImageLink { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? CreationDate { get; set; }
        public AddressInformation AddressInfo { get; set; }
        public Company Company { get; set; }
    }
}