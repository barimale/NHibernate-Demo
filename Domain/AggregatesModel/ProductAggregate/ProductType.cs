using System;
using System.Xml.Linq;
using Demo.Domain.Abstraction;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    [Serializable]
    public class ProductType : Entity<int>, IAggregateRoot
    {
        public virtual string Description { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this); ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Description, Id, Version);
        }

        public override bool Equals(object obj)
        {
            if (obj is ProductType other)
            {
                return Description == other.Description && Id == other.Id && Version == other.Version;
            }
            return false;

        }
    }
}
