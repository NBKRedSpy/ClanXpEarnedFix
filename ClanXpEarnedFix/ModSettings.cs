using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanXpEarnedFix
{
    public class ModSettings
    {
        /// <summary>
        /// If true, will log debug data.
        /// </summary>
        public bool Debug { get; set; }


        /// <summary>
        /// If true, changes clan missions' XP to a 5 skull equivalent.
        /// </summary>
        public bool ChangePayout { get; set; } = true;

        /// <summary>
        /// If true, changes clan missions' payout to a 5 skull equivalent.
        /// </summary>
        public bool ChangeXp { get; set; } = true;

    }
}
