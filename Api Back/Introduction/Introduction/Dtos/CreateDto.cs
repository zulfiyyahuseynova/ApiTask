using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Introduction.Dtos
{
    public class CreateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
