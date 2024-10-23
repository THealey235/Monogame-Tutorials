﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerAttributes.Models
{
    public class Attributes
    {
        public int HealthPoint {  get; set; }
        public int Speed { get; set; }

        public static Attributes operator +(Attributes a, Attributes b)
        {
            return new Attributes() 
            {  
                HealthPoint = a.HealthPoint + b.HealthPoint,
                Speed = a.Speed + b.Speed
            };
        }

        public static Attributes operator -(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                HealthPoint = a.HealthPoint - b.HealthPoint,
                Speed = a.Speed - b.Speed
            };
        }
    }
}
