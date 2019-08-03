using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class UserAction
    {
        public UserAction(string method, string entity)
        {
            Method = method;
            Entity = entity;
        }

        public string Method { get; set; }
        public string Entity { get; set; }
    }
}