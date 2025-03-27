using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VDCA.Ask
{
    public interface IAskView
    {
        void SetupAskView();
        void SetIsVisible(bool isVisible);
    }
}
