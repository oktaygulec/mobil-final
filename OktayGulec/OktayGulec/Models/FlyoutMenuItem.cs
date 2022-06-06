using System;
using System.Collections.Generic;
using System.Text;

namespace OktayGulec.Models
{
    public class FlyoutMenuItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Type TargetType { get; set; }
    }
}
