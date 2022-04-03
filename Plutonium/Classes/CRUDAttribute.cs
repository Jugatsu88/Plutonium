using System;

namespace Plutonium.Classes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CRUDAttribute : Attribute
    {
        //private string name;
        //public double version;
        //public int Ordering { get; set; }
        //public bool IsVisible { get; set; } = true;
        //public int Ordering;
        //public bool IsVisible = true;
        public int Ordering { get; set; } 
        public bool IsVisible { get; set; } = true;

        //public CRUDAttribute(int Ordering, bool IsVisible)
        //{
        //    this.Ordering = Ordering;
        //    this.IsVisible = IsVisible;
        //}
        //public CRUDAttribute(int Ordering)
        //{
        //    this.Ordering = Ordering;
        //}
        //public CRUDAttribute( bool IsVisible)
        //{

        //    this.IsVisible = IsVisible;
        //}
    }
}
