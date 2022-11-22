using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP4
{
    public class DescriptionAttribute : Attribute
    {
        private string description;

        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified value cannot be empty.");
                }

                this.description = value ?? throw new ArgumentNullException(nameof(this.Description), "The property cannot be null.");
            }
        }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
