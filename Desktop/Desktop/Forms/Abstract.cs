using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Forms
{
  internal interface Populatable
  {
    DateTime LastFetched { get; set; }
  }
}
