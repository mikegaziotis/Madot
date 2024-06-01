using System.Data;

namespace Badop.Infrastructure.Storage
{
    public class QueryOptions
    {
        /// <summary>
        /// Connection Timeout in seconds.
        /// </summary>
        public int ConnectionTimeout { get; set; }

        /// <summary>
        /// Defines the isolation level of the transaction: Read Uncommitted, Committed...
        /// By default, set to 'ReadUncommited'.
        /// </summary>
        public IsolationLevel IsolationLevel { get; set; }

        /// <summary>
        /// Specify if the data access call is made in a transaction.
        /// By default, set to 'true'.
        /// </summary>
        public bool IsTransactional { get; set; }

        /// <summary>
        /// The type of SqlCommand: Stored procedure, SQL...
        /// By default, set to StoredProcedure.
        /// </summary>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// For a single result query, result might be nullable.
        /// </summary>
        public bool SingleResultCanBeNull { get; set; }

        /// <summary>
        /// Create a QueryOptions. Set each option to their defaults.
        /// </summary>
        public QueryOptions()
        {
            this.ConnectionTimeout = 300;
            this.IsolationLevel = IsolationLevel.ReadUncommitted;
            this.IsTransactional = false;
            this.CommandType = CommandType.StoredProcedure;
            this.SingleResultCanBeNull = false;
        }
    }
}
