using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfGenesys.Common
{
    public sealed class CharacteristicValidationRule : IntegerRule
    {
        public override int Min { get; set; } = 1;
        public override int Max { get; set; } = 5;
    }
}
