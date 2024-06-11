using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoViber.Domain.Providers;

public interface IDateTimeProvider
{
    DateTime GetDate();
}
