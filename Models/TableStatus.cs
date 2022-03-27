using SBShared.Types;
using Shared;
using System.Collections.Generic;

namespace SBCore.Configurator.Models
{
    public class TableStatus : JsonToString
    {
        private readonly IList<string> details = new List<string>();

        public TableStatus(string tableName, TableStatusReason status, string comment, bool writable, bool userFilled)
        {
            TableName = tableName;
            Status = status;
            Comment = comment;
            Writable = writable;
            UserFilled = userFilled;
        }

        public string TableName { get; set; }
        public TableStatusReason Status { get; set; }
        public string Comment { get; set; }
        public bool Writable { get; set; }
        public bool UserFilled { get; set; }

        public IList<string> Details => details;
    }
}
