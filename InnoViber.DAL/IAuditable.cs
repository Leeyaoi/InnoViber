using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoViber.DAL;
internal interface IAuditable
{
    public DateTime createdAt {  get; set; }
    public DateTime updatedAt { get; set; }
}
