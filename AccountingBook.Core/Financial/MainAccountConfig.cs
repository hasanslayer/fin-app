using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingBook.Core.Financial
{
    public class MainAccountConfig
    {
        public Guid Id { get; set; }

        //public Account MainAccount { get; set; }
        //public long MainAccountId { get; set; }

        /// <summary>
        /// 4..6
        /// </summary>
        public string RegexPattern { get; set; } = "^[1-9]{1}..[1-9]{1}";

        public string PatternValue { get; set; }

        public ICollection<AccountConfig> AccountConfigs { get; set; }
    }
}
